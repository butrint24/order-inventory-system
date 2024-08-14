namespace Inventory.Data.Contracts;

public interface IDataRepository
{
    Task<Product> CreateProductAsync(Product product);

    Task<List<Product>> GetAllProducts();

    Task<Product> GetProductById(Guid id);

    Task<Product?> UpdateProductAsync(Product product);
    
    Task DeleteProductAsync(Product product);
}
