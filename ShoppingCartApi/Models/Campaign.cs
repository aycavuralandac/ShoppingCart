using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingCartApi.Models
{
    public class Campaign
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CategoryId")]
        public string CategoryId { get; set; }

        [BsonElement("QutityForDiscount")]
        public int QutityForDiscount { get; set; }
        
        [BsonElement("AmountOrRate")]
        public double AmountOrRate { get; set; }

        [BsonElement("DiscountType")]
        public int DiscountType { get; set; }
        [BsonElement("Status")]
        public bool Status {get;set;}
    }
}