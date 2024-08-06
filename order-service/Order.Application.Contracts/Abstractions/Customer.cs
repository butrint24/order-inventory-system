namespace Order.Application.Contracts.Abstractions;

public class Customer
{
    public Guid CustomerId { get; set; }

    public string GivenName { get; set; }

    public string FamilyName { get; set; }

    public string Address { get; set; }

    public List<Order> Orders { get; set; } = [];
}
