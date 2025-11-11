using DomainLayer.Contracts;
using DomainLayer.Models.ProductModels;
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
    public class DataSeeding(StoreDbContext _storeDbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            if (_storeDbContext.Database.GetPendingMigrations().Any())
            {
                _storeDbContext.Database.Migrate();
            }
            if (!_storeDbContext.Brands.Any())
            {
                var brandData = File.ReadAllText(@"..\Infrastructure\PersistenceLayer\Data\DataSeed\brands.json");
                var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if(brand != null && brand.Any())
                {
                    _storeDbContext.Brands.AddRange(brand);
                    _storeDbContext.SaveChanges();
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

           
        }
    }
}
