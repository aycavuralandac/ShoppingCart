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
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context = null;
        private readonly ILogger<StockRepository> _logger;

        public ProductRepository(IOptions<DBSettings> settings, ILogger<StockRepository> logger)
        {
            _context = new StoreContext(settings);
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            try
            {
                return await _context.Products.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public async Task<Product> Get(string id)
        {
            try
            {
                return await _context.Products
                                .Find(product => product.Id == id )
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public Product Create(Product product)
        {
            try
            {
                _context.Products.InsertOneAsync(product);
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        

        public async Task<bool> Update(string id, Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var update = Builders<Product>.Update
                            .Set(s => s.Title, product.Title)
                            .Set(s => s.Price, product.Price);
            try
            {
                UpdateResult actionResult = await _context.Products.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }


        public async Task<bool> Remove(Product product)
        {
            try
            {
                DeleteResult actionResult = await _context.Products.DeleteOneAsync(
                     Builders<Product>.Filter.Eq("Id", product.Id));

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
                DeleteResult actionResult = await _context.Products.DeleteOneAsync(
                     Builders<Product>.Filter.Eq("Id", id));

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
                DeleteResult actionResult = await _context.Products.DeleteManyAsync(new BsonDocument());

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
                IndexKeysDefinition <Product> keys = Builders<Product>
                                                    .IndexKeys
                                                    .Ascending(item => item.Price)
                                                    .Ascending(item => item.Title);

                return await _context.Products
                                .Indexes.CreateOneAsync(new CreateIndexModel<Product>(keys));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}