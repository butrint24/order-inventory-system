using AutoMapper;
using FluentResults;
using Inventory.Application.Contracts.GetProducts;
using Inventory.Data.Contracts;
using MediatR;
using static FluentResults.Result;

namespace Inventory.Application.Product;

public class GetProductsHandler : IRequestHandler<GetProducts, Result<IList<Contracts.Abstractions.Product>>>
{
    private readonly IDataRepository _repository;
    private readonly IMapper _mapper;

    public GetProductsHandler(
        IDataRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IList<Contracts.Abstractions.Product>>> Handle(GetProducts request, CancellationToken cancellationToken)
    {
        var getProducts = await _repository.GetAllProducts();
        
        var result = _mapper.Map<IList<Contracts.Abstractions.Product>>(getProducts);

        return Ok(result);
    }
}
