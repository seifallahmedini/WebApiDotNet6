namespace WebApi.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApi.Attributes.V1;
using WebApi.Entities.V1;

[BsonCollection("Users")]
public class User : Document
{
    [Required(ErrorMessage = "The firstname is required")]
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string ImageUrl { get; set; }
    public string Description { get; set; }

    [Required(ErrorMessage = "The email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "The password hash is required")]
    public string PasswordHash { get; set; }

    public bool AcceptTerms { get; set; }

    public string Role { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public bool Enabled { get; set; }

    public DateTime LastConnectionDate { get; set; }

    public string Gender { get; set; }

    public string Language { get; set; }

    public DateTime? BirthdayDate { get; set; }

    public string Civility { get; set; }

    public string Profession { get; set; }

    public string StudyLevel { get; set; }

    public string Situation { get; set; }

    public string VerificationToken { get; set; }

    public DateTime? Verified { get; set; }

    public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;

    public string ResetToken { get; set; }

    public DateTime? ResetTokenExpires { get; set; }

    public DateTime? PasswordReset { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; }

    [NotMapped]
    public string FullName
    {
        get
        {
            return string.Concat(FirstName, " ", LastName);
        }
    }

    public bool OwnsToken(string token)
    {
        return this.RefreshTokens?.Find(x => x.Token == token) != null;
    }
}