using Catalog.API.Data.interfaces;
using Catalog.API.Entities;
using Catalog.API.Settngs;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Product { get; }

        public CatalogContext(ICatalogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            
            var database = client.GetDatabase(settings.DatabaseName);
            
            Product = database.GetCollection<Product>(settings.CollectionName);

            SeedingDb.Seeding(Product);
        }
    }
}
