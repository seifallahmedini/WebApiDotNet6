using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
