using WebApi.Attributes.V1;

namespace WebApi.Entities.V1
{
    [BsonCollection("Interviews")]
    public class Interview : Document
    {
        public DateTime InterviewDate { get; set; }
        public string Message { get; set; }
        public string Place { get; set; }
    }
}
