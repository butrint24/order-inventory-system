namespace Order.Data.Contracts;

public class Customer
{
    public Guid CustomerId { get; set; }

    public string GivenName { get; set; }

    public string FamilyName { get; set; }

    public string Address { get; set; }

    public IList<Order>? Orders { get; set; }
}