using Duende.IdentityModel;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Store.IdentityServer.Data;
using System.Security.Claims;

namespace Store.IdentityServer.Services
{
    public class ProfileAppService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

        public ProfileAppService(UserManager<ApplicationUser> userManager, 
                                 RoleManager<IdentityRole> roleManager, 
                                 IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context, CancellationToken ct)
        {
            string id = context.Subject.GetSubjectId();

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claimns = userClaims.Claims.ToList();
            claimns.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            claimns.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

            if(_userManager.SupportsUserRole)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);

                foreach(string role in roles)
                {
                    claimns.Add(new Claim(JwtClaimTypes.Role, role));

                    if(_roleManager.SupportsRoleClaims)
                    {
                        IdentityRole identityRole = await _roleManager.FindByNameAsync(role);

                        if (identityRole is not null)
                            claimns.AddRange(await _roleManager.GetClaimsAsync(identityRole));
                    }
                }

                context.IssuedClaims = claimns;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context, CancellationToken ct)
        {
            string userid = context.Subject.GetSubjectId();

            ApplicationUser user = await _userManager.FindByIdAsync(userid);

            context.IsActive = user is not null;
        }
    }
}