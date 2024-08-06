namespace Order.Rest.Contracts.Abstractions;

public class OrderDetail
{
    public Guid Id { get; set; }

    public string? Details { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; }

    public decimal Price { get; set; }
}
