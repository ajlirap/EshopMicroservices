namespace Ordering.Domain.Events;

public record OrderUpdatedEvent(OrderItem Order) : IDomainEvent;