using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI_Layer.ApiCollection.Infrastracture;
using UI_Layer.ApiCollection.Interfaces;
using UI_Layer.AppSettings;
using UI_Layer.Models;

namespace AspnetRunBasics.ApiCollection
{
    public class BasketAPI : BaseHttpClientWithFactory, IBasketAPI
    {
        private readonly IAppSettings _appSettings;

        public BasketAPI( IHttpClientFactory factory, IAppSettings appSettings): base(factory)
        {
            _appSettings = appSettings;
        }

        public async Task<BasketModel> GetBasket(string userName)
        {
            var message = new HttpRequestBuilder(_appSettings.BaseAddress).SetPath(_appSettings.BasketPath)
                                    .AddToPath(userName).HttpMethod(HttpMethod.Get).GetHttpMessage();

            return await SendRequest<BasketModel>(message);
        }

        public async Task<BasketModel> UpdateBasket(BasketModel model)
        {
            var message = new HttpRequestBuilder(_appSettings.BaseAddress).SetPath(_appSettings.BasketPath).HttpMethod(HttpMethod.Post).GetHttpMessage();
            message.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            return await SendRequest<BasketModel>(message);
        }

        public async Task<BasketCheckoutModel> CheckoutModel(BasketCheckoutModel model)
        {
            var message = new HttpRequestBuilder(_appSettings.BaseAddress)
                                .SetPath(_appSettings.BasketPath)
                                .AddToPath("Checkout")
                                .HttpMethod(HttpMethod.Post)
                                .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await SendRequest<BasketCheckoutModel>(message); 
        }

    }
}
