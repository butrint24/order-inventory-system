using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.Purchase.GetPurchases;

public class UserInfo
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }
}
