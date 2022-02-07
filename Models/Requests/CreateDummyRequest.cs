using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class CreateDummyRequest
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
