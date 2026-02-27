using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModel;
using DomainLayer.Models.OrderModels;
using DomainLayer.Models.ProductModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace PersistenceLayer
{
    public class DataSeeding(StoreDbContext _storeDbContext
        ,UserManager<ApplicationUser> _userManager
        ,RoleManager<IdentityRole> _roleManager) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            if (_storeDbContext.Database.GetPendingMigrations().Any())
            {
                await _storeDbContext.Database.MigrateAsync();
            }
            if (!_storeDbContext.Brands.Any())
            {
                var brandData = File.ReadAllText(@"..\Infrastructure\PersistenceLayer\Data\DataSeed\brands.json");
                var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if(brand != null && brand.Any())
                {
                    await _storeDbContext.Brands.AddRangeAsync(brand);
                    await _storeDbContext.SaveChangesAsync();
                }
            }
            

            if (!_storeDbContext.Types.Any())
            {
                var typeData = File.ReadAllText(@"..\Infrastructure\PersistenceLayer\Data\DataSeed\types.json");
                var type = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                if (type != null && type.Any())
                {
                    _storeDbContext.Types.AddRange(type);
                    _storeDbContext.SaveChanges();
                }
            }
            

            if (!_storeDbContext.Products.Any())
            {
                var productData = File.ReadAllText(@"..\Infrastructure\PersistenceLayer\Data\DataSeed\products.json");
                var product = JsonSerializer.Deserialize<List<Product>>(productData);
                if (product != null && product.Any())
                {
                    _storeDbContext.Products.AddRange(product);
                    _storeDbContext.SaveChanges();
                }
            }
            if (!_storeDbContext.Set<DeliveryMethod>().Any())
            {
                var deliveryMethod = File.ReadAllText(@"..\Infrastructure\PersistenceLayer\Data\DataSeed\delivery.json");
                var deliveryMethodOnjs = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethod);
                if (deliveryMethodOnjs != null && deliveryMethodOnjs.Any())
                {
                    await _storeDbContext.AddRangeAsync(deliveryMethodOnjs);
                    _storeDbContext.SaveChanges();
                }
            }

        }

        public async Task IdentityDataSeedAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!_userManager.Users.Any())
            {
                var user01 = new ApplicationUser()
                {
                    Email = "salma@gmail.com",
                    DisplayName= "Salma",
                    UserName ="salma17",
                    PhoneNumber="01094697720"
                };
                var user02 = new ApplicationUser()
                {
                    Email = "magda@gmail.com",
                    DisplayName = "Magda",
                    UserName = "magda20",
                    PhoneNumber = "01094697720"
                };
                await _userManager.CreateAsync(user01, "password01");
                await _userManager.CreateAsync(user02, "password02");

                await _userManager.AddToRoleAsync(user01, "Admin");
                await _userManager.AddToRoleAsync(user02, "SuperAdmin");
            }
        }
    }
}
