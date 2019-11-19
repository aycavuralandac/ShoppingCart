using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingCartApi.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("MainCategoryId")]
        public string MainCategoryId { get; set; }


    }
}