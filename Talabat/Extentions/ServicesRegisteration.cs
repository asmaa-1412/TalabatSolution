using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Factories;

namespace Talabat.Extentions
{
    public static class ServicesRegisteration
    {
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiVaLidationErrorResponse;
            });
            services.ConfigureJWT(configuration);
            return services;
        }

        public static void ConfigureJWT(this IServiceCollection services,IConfiguration configuration)
        {
            var jwt = configuration.GetSection("JwtOptions").Get<JwtBearerOptions>();
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey))
                };
            });
        }
    }
}
