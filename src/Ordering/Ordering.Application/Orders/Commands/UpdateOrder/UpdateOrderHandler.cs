namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler
    (IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        //Update Order Entity from command object
        var orderId= OrderId.Of(command.order.Id);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.order.Id);
        }
        //Save to database

        UpdateOrderWithNewValues(order, command.order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        order.Update(
            OrderName.Of(orderDto.OrderName),
            Address.Of(
                orderDto.ShippingAddress.FirstName,
                orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress,
                orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.ZipCode),
            Address.Of(
                orderDto.BillingAddress.FirstName,
                orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress,
                orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.State,
                orderDto.BillingAddress.Country,
                orderDto.BillingAddress.ZipCode),
            Payment.Of(
                orderDto.Payment.CardName,
                orderDto.Payment.CardNumber,
                orderDto.Payment.Expiration,
                orderDto.Payment.Cvv,
                orderDto.Payment.PaymentMethod)
        );
    }
}
