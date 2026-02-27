using DomainLayer.Contracts;
using DomainLayer.Models.BasketModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;

namespace PersistenceLayer.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection ) : IBasketRepository
    {
        private readonly IDatabase _database=connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreateOrUpdate= await _database.StringSetAsync(basket.Id, jsonBasket,timeToLive?? TimeSpan.FromDays(30));
            if (isCreateOrUpdate) return basket;
            else return null;
        }

        public async Task<bool> DeleteBasketAsync(string key)
        => await _database.KeyDeleteAsync(key);

        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            var basket = await _database.StringGetAsync(key);
            if (basket.IsNullOrEmpty) return null;
            else return JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }
    }
}
