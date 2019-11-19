using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Data
{
    public class StoreContext
    {
        private readonly IMongoDatabase _database = null;

        public StoreContext(IOptions<DBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Stock> Stocks
        {
            get
            {
                return _database.GetCollection<Stock>("Stock");
            }
        }

        public IMongoCollection<Product> Products
        {
            get
            {
                return _database.GetCollection<Product>("Product");
            }
        }
        public IMongoCollection<Campaign> Campaigns
        {
            get
            {
                return _database.GetCollection<Campaign>("Campaign");
            }
        }
        public IMongoCollection<Coupon> Coupons
        {
            get
            {
                return _database.GetCollection<Coupon>("Coupon");
            }
        }
    }
}