using MongoDB.Bson;

namespace WebApi.Entities
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
