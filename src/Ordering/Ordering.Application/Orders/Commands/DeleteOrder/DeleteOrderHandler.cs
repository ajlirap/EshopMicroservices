
namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler (IApplicationDbContext dbContext)
    :ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        //Delete Order Entity from command object

        var orderId = OrderId.Of(request.OrderId);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(request.OrderId);
        }

        //Save to database
        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        //Return result
        return new DeleteOrderResult(true); 
    }
}
