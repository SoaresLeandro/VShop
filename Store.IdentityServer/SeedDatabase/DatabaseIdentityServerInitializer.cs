using Duende.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Store.IdentityServer.Configuration;
using Store.IdentityServer.Data;
using System.Security.Claims;

namespace Store.IdentityServer.SeedDatabase
{
    public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, 
                                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void InitializeSeedRoles()
        {
            //Se o perfil Admin não existir, então cria o perfil
            if(!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
            {
                IdentityRole roleAdmin = new IdentityRole();
                roleAdmin.Name = IdentityConfiguration.Admin;
                roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
                _roleManager.CreateAsync(roleAdmin).Wait();
            }

            //Se o perfil Client não existir, então cria o perfil
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
            {
                IdentityRole roleClient = new IdentityRole();
                roleClient.Name = IdentityConfiguration.Client;
                roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
                _roleManager.CreateAsync(roleClient).Wait();
            }
        }

        public void InitializeSeedUsers()
        {
            //Se o usuário Admin não existir, cria o usuário, define a senha e atribui ao perfil
            if(_userManager.FindByEmailAsync("admin1@com.br").Result == null)
            {
                //Define os dados do usuário Admin
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin1@com.br",
                    NormalizedEmail = "ADMIN1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (67) 12345-9999",
                    FirstName = "Usuario",
                    LastName = "Admin1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //Cria o usuário admim e atribui a senha
                IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Senha123*").Result;
                if (resultAdmin.Succeeded)
                {
                    _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                    var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                        new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                    }).Result;
                }
            }

            //Se o usuário Client não existir, cria o usuário, define a senha e atribui ao perfil
            if (_userManager.FindByEmailAsync("client1@com.br").Result == null)
            {
                //Define os dados do usuário Client
                ApplicationUser client = new ApplicationUser()
                {
                    UserName = "client1",
                    NormalizedUserName = "CLIENT1",
                    Email = "client1@com.br",
                    NormalizedEmail = "CLIENT1@COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (67) 12345-9999",
                    FirstName = "Usuario",
                    LastName = "Client1",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //Cria o usuário client e atribui a senha
                IdentityResult resultClient = _userManager.CreateAsync(client, "Senha123*").Result;
                if (resultClient.Succeeded)
                {
                    _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                    var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, client.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, client.LastName),
                        new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                    }).Result;
                }
            }
        }
    }
}