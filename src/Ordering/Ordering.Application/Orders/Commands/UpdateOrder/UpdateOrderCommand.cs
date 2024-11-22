using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto order)
    : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.order.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.order.OrderName)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.order.CustomerId)
            .NotNull().WithMessage("CustomerId is required");
    }
}
