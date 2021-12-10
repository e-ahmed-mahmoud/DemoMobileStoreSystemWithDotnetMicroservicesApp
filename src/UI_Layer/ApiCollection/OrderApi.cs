using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UI_Layer.ApiCollection.Infrastracture;
using UI_Layer.ApiCollection.Interfaces;
using UI_Layer.AppSettings;
using UI_Layer.Models;

namespace AspnetRunBasics.ApiCollection
{
    public class OrderApi : BaseHttpClientWithFactory, IOrderAPI
    {
        private readonly IAppSettings _appSettings;

        public OrderApi( IHttpClientFactory clientFactory, IAppSettings appSettings ) : base(clientFactory)
        {
            _appSettings = appSettings;
        }
        //public override HttpRequestBuilder GetHttpRequestBuilder(string path)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IEnumerable<OrderResponseModel>> GetOrderResponseByUserName(string userName)
        {
            var message = new HttpRequestBuilder(_appSettings.BaseAddress).SetPath(_appSettings.OrderPath)
                .AddToPath(userName).HttpMethod(HttpMethod.Get).GetHttpMessage();

            return await SendRequest<IEnumerable<OrderResponseModel>>(message);
        }
    }
}
