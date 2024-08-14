using AutoMapper;
using FluentResults;
using FluentValidation;
using Inventory.Application.Contracts;
using Inventory.Application.Contracts.GetProductById;
using Inventory.Data.Contracts;
using MediatR;
using static FluentResults.Result;

namespace Inventory.Application.Product;

public class GetProductByIdHandler : IRequestHandler<GetProductById, Result<GetProductByIdResponse>>
{
    private readonly IValidator<GetProductById> _validator;
    private readonly IDataRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(
        IValidator<GetProductById> validator,
        IDataRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<GetProductByIdResponse>> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);

        if (!validator.IsValid)
            return Fail(validator.Errors.FirstOrDefault()!.ErrorCode);
        
        var entity = _mapper.Map<Data.Contracts.Product>(request);

        var product = await _repository.GetProductById(entity.ProductId);

        if (product == null)
            return new Result().WithError(ErrorCodes.PRODUCT_NOT_FOUND.ToString());
        
        var result = _mapper.Map<GetProductByIdResponse>(product);

        return result == null 
            ? new Result().WithError(ErrorCodes.PRODUCT_NOT_FOUND.ToString()) 
            : Ok(result);
    }
}
