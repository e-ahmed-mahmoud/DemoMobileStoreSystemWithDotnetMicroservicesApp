using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI_Layer.Models;

namespace UI_Layer.ApiCollection.Interfaces
{
    public interface ICatalogAPI
    {
        Task<IEnumerable<CatalogModel>> GetCatalogs();

        Task<IEnumerable<CatalogModel>> GetCatalogByCatogery(string catagery);

        Task<CatalogModel> GetCatalog (string Id);

        Task<CatalogModel> CreateCatalog (CatalogModel model);
        
    }

}

