using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingCartApi.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }
        
        [BsonElement("Price")]
        public double Price { get; set; }

        [BsonElement("CategoryId")]
        public string CategoryId { get; set; }


    }
}