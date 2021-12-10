using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Data
{
    public interface IBasketDbContext
    {

        public IDatabase Redis { get; }

    }
}
