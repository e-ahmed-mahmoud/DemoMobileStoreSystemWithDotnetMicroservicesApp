using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UI_Layer.ApiCollection.Interfaces;
using UI_Layer.Models;

namespace AspnetRunBasics
{
    public class CheckOutModel : PageModel
    {
        private readonly ICatalogAPI _catalogApi;
        private readonly IBasketAPI _basketAPI;


        public CheckOutModel(ICatalogAPI catalog, IBasketAPI basketAPI)
        {
            _catalogApi = catalog;
            _basketAPI = basketAPI;
        }


        [BindProperty]
        public BasketCheckoutModel CheckOut { get; set; }

        public BasketModel Basket { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Basket = await _basketAPI.GetBasket(userName:"sw");
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            Basket = await _basketAPI.GetBasket("sw");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            CheckOut.UserName="sw";

            CheckOut.TotalPrice = Basket.TotalPrice;

            var basket = await _basketAPI.CheckoutModel(CheckOut);
            
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }       
    }
}