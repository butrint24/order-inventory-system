using System.Text.Json.Serialization;

namespace Order.Rest.Contracts.Purchase.Common;

public class ItemInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
}
