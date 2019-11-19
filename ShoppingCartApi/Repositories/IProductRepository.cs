using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> Get();

        Task<Product> Get(string id);

        Product Create(Product product);

        Task<bool> Update(string id, Product product);

        Task<bool> Remove(Product product);

        Task<bool> Remove(string id);

        Task<bool> Remove();

        Task<string> CreateIndex();

    }
}