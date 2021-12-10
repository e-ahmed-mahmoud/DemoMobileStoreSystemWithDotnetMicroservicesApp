using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UI_Layer.ApiCollection.Interfaces;
using UI_Layer.Models;

namespace AspnetRunBasics
{
    public class OrderModel : PageModel
    {
        private readonly IOrderAPI _orderApi;

        public OrderModel(IOrderAPI orderApi)
        {
            _orderApi = orderApi;
        }

        public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            Orders = await _orderApi.GetOrderResponseByUserName("sw");

            return Page();
        }       
    }
}