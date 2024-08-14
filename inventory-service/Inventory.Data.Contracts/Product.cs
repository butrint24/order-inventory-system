namespace Inventory.Data.Contracts;

public class Product
{
    public Guid ProductId { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }
    
    public int Stock { get; set; }

    public string? Details { get; set; }
}
