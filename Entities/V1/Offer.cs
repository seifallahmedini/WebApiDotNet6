using WebApi.Attributes.V1;

namespace WebApi.Entities.V1
{
    [BsonCollection("Offers")]
    public class Offer : Document
    {
        public string Title { get; set; }
        public string  Description { get; set; }
    }
}
