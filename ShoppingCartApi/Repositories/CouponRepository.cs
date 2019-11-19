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
    public class CouponRepository : ICouponRepository
    {
        private readonly StoreContext _context = null;
        private readonly ILogger<CouponRepository> _logger;

        public CouponRepository(IOptions<DBSettings> settings, ILogger<CouponRepository> logger)
        {
            _context = new StoreContext(settings);
            _logger = logger;
        }

        public async Task<IEnumerable<Coupon>> Get()
        {
            try
            {
                return await _context.Coupons.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public async Task<Coupon> Get(string id)
        {
            try
            {
                return await _context.Coupons
                                .Find(coupon => coupon.Id == id )
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public Coupon Create(Coupon coupon)
        {
            try
            {
                _context.Coupons.InsertOneAsync(coupon);
                return coupon;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        

        public async Task<bool> Update(string id, Coupon coupon)
        {
            var filter = Builders<Coupon>.Filter.Eq(p => p.Id, id);
            var update = Builders<Coupon>.Update
                            .Set(s => s.MinAmountForDiscount, coupon.MinAmountForDiscount)
                            .Set(s => s.AmountOrRate, coupon.AmountOrRate)
                            .Set(s => s.DiscountType, coupon.DiscountType);
            try
            {
                UpdateResult actionResult = await _context.Coupons.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }


        public async Task<bool> Remove(Coupon coupon)
        {
            try
            {
                DeleteResult actionResult = await _context.Coupons.DeleteOneAsync(
                     Builders<Coupon>.Filter.Eq("Id", coupon.Id));

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
                DeleteResult actionResult = await _context.Coupons.DeleteOneAsync(
                     Builders<Coupon>.Filter.Eq("Id", id));

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
                DeleteResult actionResult = await _context.Coupons.DeleteManyAsync(new BsonDocument());

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
                IndexKeysDefinition <Coupon> keys = Builders<Coupon>
                                                    .IndexKeys
                                                    .Ascending(item => item.Id);

                return await _context.Coupons
                                .Indexes.CreateOneAsync(new CreateIndexModel<Coupon>(keys));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}