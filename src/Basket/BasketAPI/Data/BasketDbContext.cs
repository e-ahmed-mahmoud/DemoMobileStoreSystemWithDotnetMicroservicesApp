using BasketAPI.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Data
{
    public class BasketDbContext : IBasketDbContext
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public BasketDbContext( ConnectionMultiplexer connection)
        {
            _redisConnection = connection;

            Redis = connection.GetDatabase();

        }
        public IDatabase Redis { get; }
    }
}
