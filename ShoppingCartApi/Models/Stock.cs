using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingCartApi.Models
{
    public class Stock
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ProductId")]
        public string ProductId { get; set; }
        
        [BsonElement("Quantity")]
        public int Quantity { get; set; }


    }
}