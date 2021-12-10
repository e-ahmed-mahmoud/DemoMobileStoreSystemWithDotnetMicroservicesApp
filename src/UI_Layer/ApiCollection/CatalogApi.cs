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
    public class CatalogApi : BaseHttpClientWithFactory, ICatalogAPI
    {
        private readonly IAppSettings _appSettings;

        //IHttpClientFactory will inject by DI from API.client package and pass to constractor of BaseHttpClientWithFactory
        public CatalogApi(IHttpClientFactory factory, IAppSettings appSettings) : base(factory)
        {
            _appSettings = appSettings;
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogs()
        {
            //build message, BaseAddress from settings, SetPath is Microservice direction : http://BaseAddress/CatalogPath
            var message = new HttpRequestBuilder(_appSettings.BaseAddress).SetPath(_appSettings.CatalogPath).HttpMethod(HttpMethod.Get).GetHttpMessage();

            //SendRequest<Return Type> (message)
            return await SendRequest<IEnumerable<CatalogModel>>(message);
        }

        public async Task<CatalogModel> GetCatalog(string Id)
        {
            //url: Base/path/Id
            var message = new HttpRequestBuilder(_appSettings.BaseAddress).SetPath(_appSettings.CatalogPath)
                                                    .AddToPath(Id).HttpMethod(HttpMethod.Get).GetHttpMessage();
            return await SendRequest<CatalogModel>(message);
        }
        public async Task<IEnumerable<CatalogModel>> GetCatalogByCatogery(string catagery)
        {
            var message = new HttpRequestBuilder(_appSettings.BaseAddress).SetPath(_appSettings.CatalogPath)
                                            .AddToPath(catagery).HttpMethod(HttpMethod.Get).GetHttpMessage();
            return await SendRequest<IEnumerable<CatalogModel>>(message);
        }

        public async Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            //convert and serialize object model as Json
            var jsonMessage =  JsonConvert.SerializeObject(model);
            
            //and encoding it as UTF8 then 
            var messageContent = new StringContent( jsonMessage , Encoding.UTF8,"application/json");

            var message = new HttpRequestBuilder(_appSettings.BaseAddress).SetPath(_appSettings.CatalogPath)
                .Content(messageContent).HttpMethod(HttpMethod.Post).GetHttpMessage();

            return await SendRequest<CatalogModel>(message);
        }

    }
}
