using AutoMapper;
using FluentResults;
using FluentValidation;
using MediatR;
using Order.Application.Contracts;
using Order.Application.Contracts.Order.DeleteOrder;
using Order.Data.Contracts;
using Order.Messaging.Contracts;
using static FluentResults.Result;

namespace Order.Application.Order;

public class DeleteOrderHandler : IRequestHandler<DeleteOrder, Result<DeleteOrderResponse>>
{
    private readonly IValidator<DeleteOrder> _validator;
    private readonly IDataRepository _repository;
    private readonly IMessageBusClient _messageBusClient;
    private readonly IMapper _mapper;

    public DeleteOrderHandler(
        IValidator<DeleteOrder> validator,
        IDataRepository repository,
        IMessageBusClient messageBusClient,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _messageBusClient = messageBusClient;
        _mapper = mapper;
    }

    public async Task<Result<DeleteOrderResponse>> Handle(DeleteOrder request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);

        if (!validator.IsValid)
            return Fail(validator.Errors.FirstOrDefault()!.ErrorCode);
        
        var entity = _mapper.Map<Data.Contracts.Order>(request);

        var getOrder = await _repository.GetOrderById(entity.OrderId);

        if (getOrder == null)
            return new Result().WithError(ErrorCodes.ORDER_NOT_FOUND.ToString());

        await _repository.DeleteOrderAsync(getOrder);

        await PublishOrder(getOrder);

        var result = _mapper.Map<DeleteOrderResponse>(getOrder);
        
        return Ok(result);
    }
    
    private async Task PublishOrder(Data.Contracts.Order order)
    {
        try
        {
            var publishMessage = _mapper.Map<PublishOrder>(order);

            publishMessage.Event = Event.Order_Deleted.ToString();
            
            _messageBusClient.PublishOrder(publishMessage);
        }
        catch (Exception ex)
        {
            new Result().WithError(ErrorCodes.RABBITMQ_FAILED_SENDING_MESSAGE.ToString());
        }
    }
}
