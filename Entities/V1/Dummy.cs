using WebApi.Attributes.V1;

namespace WebApi.Entities.V1
{
    [BsonCollection("Dummies")]
    public class Dummy : Document
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
