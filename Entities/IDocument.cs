using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi.Entities
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        string Id { get; set; }
        DateTime? CreatedAt { get; }
        DateTime? UpdatedAt { get; set; }
    }
}
