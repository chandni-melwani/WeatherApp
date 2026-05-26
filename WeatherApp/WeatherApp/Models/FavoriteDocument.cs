using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherApp.Models;

public class FavoriteDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("userId")]
    public string UserId { get; set; } = "";

    [BsonElement("city")]
    public string City { get; set; } = "";

    [BsonElement("savedAt")]
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;
}