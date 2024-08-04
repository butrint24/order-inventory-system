using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.User;

public class CreateUserRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("adress")]
    public string Address { get; set; }
}