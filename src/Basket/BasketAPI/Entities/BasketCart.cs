using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Entities
{
    public class BasketCart
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();

        public BasketCart()
        {

        }
        public BasketCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice { 
            get
            {
                decimal price = 0;
                Items.ForEach(item => price += (item.Price * item.Quantity));
                return price;
            } 
        }
    }
}
