using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PartsService.Models;

public class Part
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Name { get; set; }
    public string? SerialNumber { get; set; }
    public decimal Price { get; set; }
    public string? Note { get; set; }
}