using System.Text.Json.Serialization;

namespace Inventory.Rest.Contracts.CreateItem;

public class CreateItemResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}
