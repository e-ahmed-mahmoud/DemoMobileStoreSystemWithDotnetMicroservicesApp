using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UI_Layer.ApiCollection.Interfaces;
using UI_Layer.Models;

namespace AspnetRunBasics
{
    public class ProductDetailModel : PageModel
    {
        private readonly ICatalogAPI _catalogApi;
        private readonly IBasketAPI _basketAPI;

        public ProductDetailModel(ICatalogAPI catalog, IBasketAPI basketAPI)
        {
            _catalogApi = catalog;
            _basketAPI = basketAPI;
        }


        public CatalogModel Product { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(string productId)
        {
            if (String.IsNullOrEmpty(productId))
            {
                return NotFound();
            }

            Product = await _catalogApi.GetCatalog(productId);

            if (Product == null)
            {
                return NotFound();
            }
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
                Color = Color,
                Price = product.Price,
                ProductName = product.Name,
                Quantity = Quantity
            });

            await _basketAPI.UpdateBasket(basket);

            return RedirectToPage("Cart");
        }
    }
}