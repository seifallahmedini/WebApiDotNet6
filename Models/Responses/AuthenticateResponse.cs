namespace WebApi.Models;

using System.Text.Json.Serialization;
using WebApi.Entities;


/// <summary>
/// Defines the <see cref="AuthenticateResponse" />.
/// </summary>
public class AuthenticateResponse
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the FirstName.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the LastName.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the Role.
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// Gets or sets the Created.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the Updated.
    /// </summary>
    public DateTime? Updated { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether IsVerified.
    /// </summary>
    public bool IsVerified { get; set; }

    /// <summary>
    /// Gets or sets the JwtToken.
    /// </summary>
    public string JwtToken { get; set; }

    /// <summary>
    /// Gets or sets the RefreshToken.
    /// </summary>
    [JsonIgnore] // refresh token is returned in http only cookie
    public string RefreshToken { get; set; }
}