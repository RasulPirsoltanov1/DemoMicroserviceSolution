using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderWebApi.Models
{
    [BsonIgnoreExtraElements]
    public class Order
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; } // ObjectId is typically represented as string in C#
        [BsonElement("customer_id")]
        [BsonRepresentation(BsonType.Int32)]
        public int CustomerId { get; set; }
        [BsonElement("created_on")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? CreatedOn { get; set; }
        [BsonElement("order_details")]
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}
