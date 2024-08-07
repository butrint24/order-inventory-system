using AutoMapper;
using FluentResults;
using MediatR;
using Order.Application.Contracts.Customer.GetCustomers;
using Order.Data.Contracts;
using static FluentResults.Result;

namespace Order.Application.Customer;

public class GetCustomersHandler : IRequestHandler<GetCustomers, Result<IList<Contracts.Abstractions.Customer>>>
{
    private readonly IDataRepository _repository;
    private readonly IMapper _mapper;

    public GetCustomersHandler(
        IDataRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IList<Contracts.Abstractions.Customer>>> Handle(GetCustomers request, CancellationToken cancellationToken)
    {
        var getCustomers = await _repository.GetAllCustomers();

        var customerOrders = new Dictionary<Guid, List<Contracts.Abstractions.Order>>();
        
        foreach (var customer in getCustomers)
        {
            var order = await _repository.GetCustomerOrders(customer.CustomerId);
            
            var mappedOrders = _mapper.Map<IEnumerable<Contracts.Abstractions.Order>>(order);
            
            customerOrders[customer.CustomerId] = mappedOrders.ToList();
        }
        
        var result = _mapper.Map<IList<Contracts.Abstractions.Customer>>(getCustomers);
        
        foreach (var customer in result)
        {
            if (customerOrders.TryGetValue(customer.CustomerId, out var orders))
            {
                customer.Orders = orders;
            }
        }
        
        return Ok(result);
    }
}
