using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UI_Layer.ApiCollection.Interfaces;
using UI_Layer.Models;

namespace AspnetRunBasics.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogAPI _catalogApi;
        private readonly IBasketAPI _basketAPI;

        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

        public IndexModel(ICatalogAPI catalog, IBasketAPI basketAPI)
        {
            _catalogApi = catalog;
            _basketAPI = basketAPI;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _catalogApi.GetCatalogs();
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });
            var product = await _catalogApi.GetCatalog(productId);

            var basket = await _basketAPI.GetBasket(userName: "sw");

            basket.Items.Add(new BasketCartItem
            {
                ProductId = productId,
                Color = "white",
                Price = product.Price,
                ProductName = product.Name,
                Quantity = 1
            });
            
            await _basketAPI.UpdateBasket(basket);
            
            return RedirectToPage("Cart");
        }
    }
}
