using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace ShoppingCartApi.Models
{
    public class Coupon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("MinAmountForDiscount")]
        public int MinAmountForDiscount { get; set; }
        
        [BsonElement("AmountOrRate")]
        public double AmountOrRate { get; set; }

        [BsonElement("DiscountType")]
        public int DiscountType { get; set; }
    }
}