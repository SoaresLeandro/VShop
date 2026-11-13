using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Mappings;
using Store.Application.Services;
using Store.Application.Services.Interfaces;
using Store.Domain.Interfaces.Repositories;
using Store.Infrastructure.Context;
using Store.Infrastructure.Repositories;

namespace Store.CrossCutting.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Context
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, MySqlServerVersion.AutoDetect(connectionString)));

            //Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            //Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            //Mappings
            services.AddScoped(typeof(CategoryProfile));
            services.AddScoped(typeof(ProductProfile));

            return services;
        }
    }
}