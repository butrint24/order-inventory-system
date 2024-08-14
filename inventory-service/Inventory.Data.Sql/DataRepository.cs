using Inventory.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data.Sql;

public class DataRepository : IDataRepository
{
    private readonly ApplicationDbContext _dataContext;

    public DataRepository(ApplicationDbContext dataContext) => _dataContext = dataContext;
    
    public async Task<Product> CreateProductAsync(Product product)
    {
        var result = await _dataContext.Products.AddAsync(product);
       
        await _dataContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await _dataContext.Products.ToListAsync();
    }

    public async Task<Product> GetProductById(Guid id)
    {
        return await _dataContext.Products.FindAsync(id);
    }
    
    public async Task<Product?> UpdateProductAsync(Product product)
    {
        var existingProduct = await _dataContext.Products.FindAsync(product.ProductId);
    
        if (existingProduct == null)
            return null;

        _dataContext.Entry(existingProduct).CurrentValues.SetValues(product);
        
        await _dataContext.SaveChangesAsync();

        return existingProduct;
    }

    public async Task DeleteProductAsync(Product product)
    {
        _dataContext.Products.Remove(product);
        
        await _dataContext.SaveChangesAsync();
    }
}
