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

        var result = _mapper.Map<IList<Contracts.Abstractions.Customer>>(getCustomers);
        
        return Ok(result);
    }
}
