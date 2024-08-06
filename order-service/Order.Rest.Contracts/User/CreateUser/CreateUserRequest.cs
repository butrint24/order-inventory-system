using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.User.CreateUser;

public class CreateUserRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }
}
