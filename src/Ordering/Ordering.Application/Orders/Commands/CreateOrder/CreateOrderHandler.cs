using EshopMicro.Common.CQRS;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{

    public async Task<CreateOrderResult> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        //Create Order Entity from command object
        //Save to database
        //Return result
        throw new NotImplementedException();
    }
}