using AutoMapper;
using FluentResults;
using FluentValidation;
using Inventory.Application.Contracts;
using Inventory.Application.Contracts.DeleteProduct;
using Inventory.Data.Contracts;
using MediatR;
using static FluentResults.Result;

namespace Inventory.Application.Product;

public class DeleteProductHandler : IRequestHandler<DeleteProduct, Result<DeleteProductResponse>>
{
    private readonly IValidator<DeleteProduct> _validator;
    private readonly IDataRepository _repository;
    private readonly IMapper _mapper;

    public DeleteProductHandler(
        IValidator<DeleteProduct> validator,
        IDataRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<DeleteProductResponse>> Handle(DeleteProduct request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);

        if (!validator.IsValid)
            return Fail(validator.Errors.FirstOrDefault()!.ErrorCode);
        
        var entity = _mapper.Map<Data.Contracts.Product>(request);

        var getProduct = await _repository.GetProductById(entity.ProductId);

        if (getProduct == null)
            return new Result().WithError(ErrorCodes.PRODUCT_NOT_FOUND.ToString());

        await _repository.DeleteProductAsync(getProduct);

        var result = _mapper.Map<DeleteProductResponse>(getProduct);
        
        return Ok(result);
    }
}
