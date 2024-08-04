using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.User;

public class CreateUserResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}