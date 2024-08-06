using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.User.CreateUser;

public class CreateUserResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}
