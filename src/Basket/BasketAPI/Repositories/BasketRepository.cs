using BasketAPI.Data;
using BasketAPI.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketDbContext _context;

        public BasketRepository(IBasketDbContext context)
        {
            _context = context;
        }

        public async Task<BasketCart> GetBasket(string userName)
        {
            var basket = await _context.Redis.StringGetAsync(userName);

            if (basket.IsNullOrEmpty)
                return null;

            return JsonConvert.DeserializeObject<BasketCart>(basket);
        }
        public async Task<BasketCart> UpdateBasket(BasketCart basketCart)
        {
            var updateObj = await _context.Redis.StringSetAsync(basketCart.UserName, JsonConvert.SerializeObject(basketCart));

            if (!updateObj)
                return null;
            return await GetBasket(basketCart.UserName);
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            var res = await _context.Redis.KeyDeleteAsync(userName);
            return res;
        }


    }
}
