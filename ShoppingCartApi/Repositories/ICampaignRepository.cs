using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Repositories
{
    public interface ICampaignRepository
    {
        Task<IEnumerable<Campaign>> Get();

        Task<Campaign> Get(string id);

        Task<Campaign> GetCampaignByCategory(string categoryId, int discountType);

        Campaign Create(Campaign campaign);

        Task<bool> Update(string id, Campaign campaign);

        Task<bool> Remove(Campaign campaign);

        Task<bool> Remove(string id);

        Task<bool> Remove();

        Task<string> CreateIndex();

    }
}