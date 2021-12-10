using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public class OrderDbSeeding
    {
        public static async Task SeedOrderingDb(OrderingDbContext context, ILoggerFactory logger,int? retry)
        {
            context.Database.Migrate();

            try
            {
                if (!context.Orders.Any())
                {
                    await context.AddRangeAsync(AddIntialData());
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                if ( retry.HasValue &&retry.GetValueOrDefault() > 2)
                {
                    await SeedOrderingDb(context, logger, ++retry);
                }
                else
                {
                    var loggerMsg = logger.CreateLogger<OrderDbSeeding>();

                    loggerMsg.LogError(ex.Message);
                }
            }
        }

        private static IEnumerable<Order> AddIntialData()
        {
            return new List<Order>() { new Order { FirstName="ahmed" , AddressLine="sad" , CardName="af24", CardNumber="3224", Country="egy",
                CVV="asd", EmailAddress="asd.com", Expiration="5/11", LastName="asdaw", PaymentMethod=1,State="va", TotalPrice=65, 
                UserName="sw",ZipCode="45748"
            } 
            
            };
        }
    }
}
