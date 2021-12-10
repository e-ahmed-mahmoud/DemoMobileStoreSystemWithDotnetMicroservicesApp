using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UI_Layer.ApiCollection.Interfaces;
using UI_Layer.Models;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketAPI _basketApi;

        public CartModel(IBasketAPI basket)
        {
            _basketApi = basket;
        }

        public BasketModel Basket { get; set; }        

        public async Task<IActionResult> OnGetAsync()
        {
            Basket = await _basketApi.GetBasket(userName:"sw");            

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var basketDb = await _basketApi.GetBasket("sw");
            
            var item = basketDb.Items.Single(i => i.ProductId == productId);
            
            basketDb.Items.Remove(item);

            await _basketApi.UpdateBasket(basketDb);

            return RedirectToPage();
        }
    }
}