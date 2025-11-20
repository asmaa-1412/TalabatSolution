using Microsoft.AspNetCore.Mvc;
using Talabat.Factories;

namespace Talabat.Extentions
{
    public static class ServicesRegisteration
    {
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiVaLidationErrorResponse;
            });
            return services;
        }
    }
}
