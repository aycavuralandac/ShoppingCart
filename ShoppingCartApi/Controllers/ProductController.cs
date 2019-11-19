using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ShoppingCartApi.Models;
using ShoppingCartApi.Repositories;
using ShoppingCartApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
    
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET api/product
        // example: http://localhost:5000/api/product
        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productRepository.Get();
        }

        // GET api/product/id
        // example: http://localhost:5000/api/product/49902812
        [HttpGet("{id}")]
        public async Task<Product> Get(string id)
        {
            return await _productRepository.Get(id) ?? new Product();
        }

        // POST api/product
        // example: http://localhost:5000/api/product
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            _productRepository.Create(product);
        }

        // PUT api/product
        // example: http://localhost:5000/api/product
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] Product productIn)
        {
            var product = _productRepository.Get(id);

            if (product == null)
            {
                return NotFound();
            } 

            _productRepository.Update(id, productIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = _productRepository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productRepository.Remove(id);

            return NoContent();
        }

    }
}