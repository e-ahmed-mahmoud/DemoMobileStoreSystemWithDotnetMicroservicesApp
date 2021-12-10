using Catalog.API.Data.interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Product.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context.Product.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCatagery(string catogery)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, catogery);
            return await _context.Product.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await _context.Product.Find(filter).ToListAsync();
        }
        public async Task Create(Product product)
        {
            await _context.Product.InsertOneAsync(product);
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);

            var res = await _context.Product.ReplaceOneAsync(filter, product);
            return res.IsAcknowledged && res.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var res = await _context.Product.DeleteOneAsync(filter);
            return res.IsAcknowledged && res.DeletedCount > 0;
        }

    }
}
