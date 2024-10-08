﻿namespace Order.Data.Contracts;

public interface IDataRepository
{
    #region Order

    Task<Order> CreateOrderAsync(Order order);

    Task<List<Order>> GetAllOrders();

    Task<Order> GetOrderById(Guid id);
    
    Task DeleteOrderAsync(Order order);
    
    Task<Customer> GetCustomerDetails(Guid id);

    #endregion

    #region Customer

    Task<Customer> CreateCustomerAsync(Customer customer);

    Task<List<Customer>> GetAllCustomers();

    Task<Customer> GetCustomerById(Guid id);

    Task DeleteCustomerAsync(Customer customer);

    Task<List<Order>> GetCustomerOrders(Guid id);

    #endregion

}
