using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> Get();

        Task<Stock> Get(string id);

        Task Create(Stock stock);

        Task<bool> Update(string id, Stock stock);

        Task<bool> Remove(Stock stock);

        Task<bool> Remove(string id);

        Task<bool> Remove();

        Task<string> CreateIndex();

    }
}