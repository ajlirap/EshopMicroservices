namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler (
    IApplicationDbContext dbContext
    )
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{

    public async Task<CreateOrderResult> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        //Create Order Entity from command object
        var order = CreateNewOrder(command.order);

        //Save to database
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        //Return result

        return new CreateOrderResult(order.Id.Value); 
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName,
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.ZipCode);

        var billingAddress = Address.Of(
            orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.ZipCode);

        var newOrder = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(orderDto.CustomerId),
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: shippingAddress,
            billingAddress: billingAddress,
            payment: Payment.Of(orderDto.Payment.CardName,
                orderDto.Payment.CardNumber,
                orderDto.Payment.Expiration,
                orderDto.Payment.Cvv,
                orderDto.Payment.PaymentMethod)
            );

        foreach (var item in orderDto.OrderItems)
        {
            newOrder.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
        }

        return newOrder;
    }
}