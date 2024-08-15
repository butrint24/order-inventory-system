using System.Text.Json.Serialization;
using Inventory.Rest.Contracts.Abstractions;

namespace Inventory.Rest.Contracts.GetItems;

public class GetItemsResponse
{
    [JsonPropertyName("items")]
    public IList<Item> Items { get; set; }
}
