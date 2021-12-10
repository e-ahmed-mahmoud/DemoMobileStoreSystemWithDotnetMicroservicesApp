using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI_Layer.Models;

namespace UI_Layer.ApiCollection.Interfaces
{
    public interface IOrderAPI
    {
        Task<IEnumerable<OrderResponseModel>> GetOrderResponseByUserName (string userName);
    }
}