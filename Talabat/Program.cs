
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer;
using PersistenceLayer.Data;
using PersistenceLayer.Repositories;
using ServicesAbstractionLayer;
using ServicesLayer;
using ServicesLayer.MappingProfiles;
using Talabat.CustomMiddleWares;

namespace Talabat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfwork, UnitOfwork>();
            builder.Services.AddAutoMapper(p => p.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IServiceManger, ServiceManger>();

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var seedobj = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            seedobj.DataSeed();
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
