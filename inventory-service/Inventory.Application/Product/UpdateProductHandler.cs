using AutoMapper;
using FluentResults;
using FluentValidation;
using Inventory.Application.Contracts;
using Inventory.Application.Contracts.CreateProduct;
using Inventory.Application.Contracts.UpdateProduct;
using Inventory.Data.Contracts;
using MediatR;
using static FluentResults.Result;

namespace Inventory.Application.Product;

public class UpdateProductHandler : IRequestHandler<UpdateProduct, Result<UpdateProductResponse>>
{
    private readonly IValidator<UpdateProduct> _validator;
    private readonly IDataRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProductHandler(
        IValidator<UpdateProduct> validator,
        IDataRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<UpdateProductResponse>> Handle(UpdateProduct request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);

        if (!validator.IsValid)
            return Fail(validator.Errors.FirstOrDefault()!.ErrorCode);
        
        var mappedProduct = _mapper.Map<Data.Contracts.Product>(request);
        
        var updatedProduct = await _repository.UpdateProductAsync(mappedProduct);

        if (updatedProduct.ToResult().IsFailed || updatedProduct == null)
            return new Result().WithError(ErrorCodes.UPDATE_PRODUCT_FAILED.ToString());

        var result = _mapper.Map<UpdateProductResponse>(updatedProduct);

        return Ok(result);
    }
}
