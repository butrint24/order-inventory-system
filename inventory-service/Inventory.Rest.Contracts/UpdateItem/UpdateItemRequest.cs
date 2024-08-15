using System.Text.Json.Serialization;
using Inventory.Rest.Contracts.Abstractions;

namespace Inventory.Rest.Contracts.UpdateItem;

public class UpdateItemRequest : Item
{
    [JsonIgnore]
    public new Guid? Id { get; set; }
}
