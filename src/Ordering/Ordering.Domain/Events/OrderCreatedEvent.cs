namespace Ordering.Domain.Events;

public record OrderCreatedEvent (OrderItem order) : IDomainEvent;