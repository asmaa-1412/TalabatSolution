using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersistenceLayer.Data;
using PersistenceLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer
{
    public static class InfrastructureServicesRegisteration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration _configuration)
        {
            services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IDataSeeding, DataSeeding>();
            services.AddScoped<IUnitOfwork, UnitOfwork>();
            return services;
        }
    }
}
