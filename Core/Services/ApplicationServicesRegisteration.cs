using Microsoft.Extensions.DependencyInjection;
using ServicesAbstractionLayer;
using ServicesLayer.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(p => p.AddProfile(new ProductMappingProfile()));
            services.AddScoped<IServiceManger, ServiceManger>();
            return services;
        }
    }
}
