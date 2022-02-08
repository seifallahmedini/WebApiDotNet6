using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class VerifyResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
