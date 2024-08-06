using Microsoft.EntityFrameworkCore;
using Order.Data.Contracts;

namespace Order.Data.Pgsql;

public class DataRepository : IDataRepository
{
    private readonly ApplicationDbContext _dataContext;

    public DataRepository(ApplicationDbContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    #region Order

    public async Task<Contracts.Order> CreateOrderAsync(Contracts.Order order)
    {
       var result = await _dataContext.Orders.AddAsync(order);
       
       await _dataContext.SaveChangesAsync();

       return result.Entity;
    }
    
    public async Task<List<Contracts.Order>> GetAllOrders()
    {
        return await _dataContext.Orders.ToListAsync();
    }

    public async Task<Contracts.Order> GetOrderById(Guid id)
    {
        return await _dataContext.Orders.FindAsync(id);
    }

    #endregion

    #region Customer
    
    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        var result = await _dataContext.Customers.AddAsync(customer);
        
        await _dataContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        return await _dataContext.Customers.ToListAsync();
    }

    public async Task<Customer> GetCustomerById(Guid id)
    {
        return await _dataContext.Customers.FindAsync(id);
    }

    public async Task DeleteCustomerAsync(Customer customer)
    {
        _dataContext.Customers.Remove(customer);
        
        await _dataContext.SaveChangesAsync();
    }
    
    #endregion
}
