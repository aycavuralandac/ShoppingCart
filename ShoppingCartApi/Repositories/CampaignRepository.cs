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
    public class CampaignRepository : ICampaignRepository
    {
        private readonly StoreContext _context = null;
        private readonly ILogger<CampaignRepository> _logger;

        public CampaignRepository(IOptions<DBSettings> settings, ILogger<CampaignRepository> logger)
        {
            _context = new StoreContext(settings);
            _logger = logger;
        }

        public async Task<IEnumerable<Campaign>> Get()
        {
            try
            {
                return await _context.Campaigns.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public async Task<Campaign> Get(string id)
        {
            try
            {
                return await _context.Campaigns
                                .Find(campaign => campaign.Id == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public async Task<Campaign> GetCampaignByCategory(string categoryId, int discountType)
        {
            try
            {
                return await _context.Campaigns
                                .Find(campaign => campaign.CategoryId == categoryId && campaign.Status == true && campaign.DiscountType == discountType).SortByDescending(campaign => campaign.AmountOrRate)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public Campaign Create(Campaign campaign)
        {
            try
            {
                _context.Campaigns.InsertOneAsync(campaign);
                return campaign;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }



        public async Task<bool> Update(string id, Campaign campaign)
        {
            var filter = Builders<Campaign>.Filter.Eq(p => p.Id, id);
            var update = Builders<Campaign>.Update
                            .Set(s => s.CategoryId, campaign.CategoryId)
                            .Set(s => s.QutityForDiscount, campaign.QutityForDiscount)
                            .Set(s => s.AmountOrRate, campaign.AmountOrRate)
                            .Set(s => s.DiscountType, campaign.DiscountType);
            try
            {
                UpdateResult actionResult = await _context.Campaigns.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }


        public async Task<bool> Remove(Campaign campaign)
        {
            try
            {
                DeleteResult actionResult = await _context.Campaigns.DeleteOneAsync(
                     Builders<Campaign>.Filter.Eq("Id", campaign.Id));

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
                DeleteResult actionResult = await _context.Campaigns.DeleteOneAsync(
                     Builders<Campaign>.Filter.Eq("Id", id));

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
                DeleteResult actionResult = await _context.Campaigns.DeleteManyAsync(new BsonDocument());

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
                IndexKeysDefinition<Campaign> keys = Builders<Campaign>
                                                    .IndexKeys
                                                    .Ascending(item => item.Id);

                return await _context.Campaigns
                                .Indexes.CreateOneAsync(new CreateIndexModel<Campaign>(keys));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}