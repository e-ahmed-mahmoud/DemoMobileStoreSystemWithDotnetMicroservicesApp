using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UI_Layer.ApiCollection.Interfaces;
using UI_Layer.Models;

namespace AspnetRunBasics
{
    public class ProductModel : PageModel
    {
        private readonly ICatalogAPI _catalogApi;
        private readonly IBasketAPI _basketAPI;
        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; }

        [BindProperty(SupportsGet = true)]
        public CatalogModel Product { get; set; }
        
        public ProductModel(ICatalogAPI catalog, IBasketAPI basketAPI)
        {
            _catalogApi = catalog;
            _basketAPI = basketAPI;
        }


        public async Task<IActionResult> OnGetAsync(string prodcutId)
        {
            prodcutId = String.IsNullOrEmpty(prodcutId)? "602d2149e773f2a3990b47f5" : prodcutId;
            if (String.IsNullOrEmpty(prodcutId))
                return NotFound();
            
            Product = await _catalogApi.GetCatalog(prodcutId);
            
            if (Product is null)
                return NotFound();

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