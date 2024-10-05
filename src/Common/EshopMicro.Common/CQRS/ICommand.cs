using MediatR;

namespace EshopMicro.Common.CQRS;

public interface ICommand: ICommand<Unit>
{
}
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
