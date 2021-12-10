using Catalog.API.Entities;
using Catalog.API.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;

        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProduct()
        {
            var products = await _repository.GetAllProducts();
            return Ok(products);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            return Ok(await _repository.GetProductById(id));
        }

        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable< Product>>> GetProductByName(string name)
        {
            var products = await _repository.GetProductsByName(name);
            return Ok(products);
        }

        [Route("[action]/{catogery}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByCatogery(string catogery)
        {
            var products = await _repository.GetProductsByCatagery(catogery);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (product != null)
            {
                await _repository.Create(product);
                return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
            }
            else
                return NotFound();
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            if (product != null)
            {
                return Ok(await _repository.UpdateProduct(product));
            }
            else
               { return NotFound(); }
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteProduct (string id)
        {
            try
            {
                return Ok(await _repository.DeleteProduct(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
     
    
    }
}
