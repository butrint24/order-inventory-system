using AutoMapper;
using FluentResults;
using FluentValidation;
using Inventory.Application.Contracts;
using Inventory.Application.Contracts.CreateProduct;
using Inventory.Data.Contracts;
using MediatR;
using static FluentResults.Result;

namespace Inventory.Application.Product;

public class CreateProductHandler : IRequestHandler<CreateProduct, Result<CreateProductResponse>>
{
    private readonly IValidator<CreateProduct> _validator;
    private readonly IDataRepository _repository;
    private readonly IMapper _mapper;

    public CreateProductHandler(
        IValidator<CreateProduct> validator,
        IDataRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);

        if (!validator.IsValid)
            return Fail(validator.Errors.FirstOrDefault()!.ErrorCode);
        
        var mappedProduct = _mapper.Map<Data.Contracts.Product>(request);
        
        var product = await _repository.CreateProductAsync(mappedProduct);

        if (product.ToResult().IsFailed)
            return new Result().WithError(ErrorCodes.CREATE_PRODUCT_FAILED.ToString());

        var result = _mapper.Map<CreateProductResponse>(product);

        return Ok(result);
    }
}
