using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI_Layer.Models;


namespace UI_Layer.ApiCollection.Interfaces
{
    public interface IBasketAPI
    {
        Task<BasketModel> GetBasket(string userName);

        Task<BasketModel> UpdateBasket(BasketModel model);

        Task <BasketCheckoutModel> CheckoutModel(BasketCheckoutModel model);
    }
}