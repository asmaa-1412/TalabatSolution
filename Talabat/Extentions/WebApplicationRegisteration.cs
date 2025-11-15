using DomainLayer.Contracts;

namespace Talabat.Extentions
{
    public static class WebApplicationRegisteration
    {
        public static async Task SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var seedobj = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await seedobj.DataSeedAsync();
            await seedobj.IdentityDataSeedAsync();
        }
    }
}
