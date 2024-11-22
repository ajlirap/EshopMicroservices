using EshopMicro.Common.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrderQuery(PaginationRequest PaginationRequest) :
    IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginationResult<OrderDto> Orders);
