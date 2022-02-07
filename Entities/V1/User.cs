namespace WebApi.Entities;

using System.Text.Json.Serialization;
using WebApi.Attributes.V1;

[BsonCollection("Users")]
public class User : Document
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
}