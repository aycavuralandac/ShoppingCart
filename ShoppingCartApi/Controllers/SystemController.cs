using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using ShoppingCartApi.Models;
using ShoppingCartApi.Infrastructure;
using ShoppingCartApi.Repositories;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    public class SystemController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;

        public SystemController(IProductRepository productRepository, IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }

        // Call an initialization - api/system/init
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (setting == "init")
            {
                _productRepository.Remove();
                _stockRepository.Remove();
                var title = _productRepository.CreateIndex();

                Product product = new Product();

                product = _productRepository.Create(new Product()
                {
                    Title = "Toshiba L312",
                    Price = 3800,
                    CategoryId = "5dcb0919d1e4079b69a897d0"
                });

                int quantity = 0;
                if (_stockRepository.Get(product.Id).Result != null)
                {
                    quantity = _stockRepository.Get(product.Id).Result.Quantity;
                }
                
                _stockRepository.Create(new Stock()
                {
                    ProductId = product.Id,
                    Quantity = quantity + 1
                });

                product = _productRepository.Create(new Product()
                {
                    Title = "Nike Air",
                    Price = 650,
                    CategoryId = "5dcb09abd1e4079b69a897d2"
                });

                quantity = 0;
                if (_stockRepository.Get(product.Id).Result != null)
                {
                    quantity = _stockRepository.Get(product.Id).Result.Quantity;
                }

                _stockRepository.Create(new Stock()
                {
                    ProductId = product.Id,
                    Quantity = quantity + 1
                });

                product = _productRepository.Create(new Product()
                {
                    Title = "Kulaklik",
                    Price = 150,
                    CategoryId = "5dcb08e2d1e4079b69a897cf"
                });

                quantity = 0;
                if (_stockRepository.Get(product.Id).Result != null)
                {
                    quantity = _stockRepository.Get(product.Id).Result.Quantity;
                }

                _stockRepository.Create(new Stock()
                {
                    ProductId = product.Id,
                    Quantity = quantity + 1
                });

                product = _productRepository.Create(new Product()
                {
                    Title = "Macbook Pro",
                    Price = 17000,
                    CategoryId = "5dcb0919d1e4079b69a897d0"
                });

                quantity = 0;
                if (_stockRepository.Get(product.Id).Result != null)
                {
                    quantity = _stockRepository.Get(product.Id).Result.Quantity;
                }

                _stockRepository.Create(new Stock()
                {
                    ProductId = product.Id,
                    Quantity = quantity + 1
                });

                return "Database ProductsDb was created, and collection 'Product' was filled with 4 sample items";
            }

            return "Unknown";
        }
    }
}
