using System.Text.Json.Serialization;
using UserInfo = Order.Rest.Contracts.Abstractions.User;

namespace Order.Rest.Contracts.User.GetUsers;

public class GetUsersResponse 
{
    [JsonPropertyName("users")]
    public IList<UserInfo> Users { get; set; }
}
