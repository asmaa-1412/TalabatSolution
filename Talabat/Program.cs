
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens.Experimental;
using PersistenceLayer;
using PersistenceLayer.Data;
using PersistenceLayer.Repositories;
using ServicesAbstractionLayer;
using ServicesLayer;
using ServicesLayer.MappingProfiles;
using Shared.ErrorModel;
using Swashbuckle.AspNetCore.SwaggerUI;
using Talabat.CustomMiddleWares;
using Talabat.Extentions;
using Talabat.Factories;

namespace Talabat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            //ApplicationServicesRegisteration.AddApplicationServices(builder.Services);

            builder.Services.AddWebApplicationServices();

            #endregion

            var app = builder.Build();

            #region DataSeed
            app.SeedDatabase();
            #endregion

            #region Configure the HTTP request pipeline.
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
    
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.ConfigObject = new ConfigObject()
                    {
                        DisplayRequestDuration= true
                    };
                    options.DocumentTitle = "Talabat Ecommerce App";
                    options.DocExpansion(DocExpansion.None);
                    options.EnableFilter();
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
