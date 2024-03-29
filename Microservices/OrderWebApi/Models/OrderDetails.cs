﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OrderWebApi.Models
{
    public class OrderDetails
    {
        [BsonElement("product_id"), BsonRepresentation(BsonType.Int32)]
        public int ProductId { get; set;}
        [BsonElement("quantity"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Quantity{ get; set; }
        [BsonElement("unit_price,"), BsonRepresentation(BsonType.Decimal128)]
        public decimal UnitPrice{ get; set; }
    }
}
