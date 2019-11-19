using System;
using MongoDB.Bson;
using MongoDB.Driver;
using ShoppingCartApi.Models;
using ShoppingCartApi.Data;
using ShoppingCartApi.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using MongoDB.Driver.Linq;

namespace ShoppingCartApi.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StoreContext _context = null;
        private readonly ILogger<StockRepository> _logger;

        public StockRepository(IOptions<DBSettings> settings, ILogger<StockRepository> logger)
        {
            _context = new StoreContext(settings);
            _logger = logger;
        }

        public async Task<IEnumerable<Stock>> Get()
        {
            try
            {
                return await _context.Stocks.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public async Task<Stock> Get(string id)
        {
            try
            {
                return await _context.Stocks
                                .Find(stock => stock.ProductId == id )                                
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public async Task Create(Stock stock)
        {
            try
            {
                await _context.Stocks.InsertOneAsync(stock);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        

        public async Task<bool> Update(string id, Stock stock)
        {
            var filter = Builders<Stock>.Filter.Eq(p => p.Id, id);
            var update = Builders<Stock>.Update
                            .Set(s => s.ProductId, stock.ProductId)
                            .Set(s => s.Quantity, stock.Quantity);
            try
            {
                UpdateResult actionResult = await _context.Stocks.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }


        public async Task<bool> Remove(Stock stock)
        {
            try
            {
                DeleteResult actionResult = await _context.Stocks.DeleteOneAsync(
                     Builders<Stock>.Filter.Eq("Id", stock.Id));

                return actionResult.IsAcknowledged 
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public async Task<bool> Remove(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.Stocks.DeleteOneAsync(
                     Builders<Stock>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged 
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public async Task<bool> Remove()
        {
            try
            {
                DeleteResult actionResult = await _context.Stocks.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged 
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public async Task<string> CreateIndex()
        {
            try
            {
                IndexKeysDefinition <Stock> keys = Builders<Stock>
                                                    .IndexKeys
                                                    .Ascending(item => item.ProductId)
                                                    .Ascending(item => item.Quantity);

                return await _context.Stocks
                                .Indexes.CreateOneAsync(new CreateIndexModel<Stock>(keys));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }
        
    }
}