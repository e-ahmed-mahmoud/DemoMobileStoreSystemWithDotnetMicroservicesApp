using System.Collections.Generic;

namespace UI_Layer.Models
{
    public class BasketModel
    {

        public string UserName { get; set; }

        public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();

        public decimal TotalPrice{get;set;}
    }
}