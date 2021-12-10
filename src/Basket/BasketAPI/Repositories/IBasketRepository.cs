using BasketAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Repositories
{
    public interface IBasketRepository
    {

        Task<BasketCart> GetBasket(string userName);

        Task<BasketCart> UpdateBasket(BasketCart basketCart);

        Task<bool> DeleteBasket(string userName);

    }
}
