using EshopMicro.Common.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrderQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrderQuery query, CancellationToken cancellationToken)
        {
            //get orders with pagination

            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orders = await dbContext.Orders
                .Include(x => x.OrderItems)
                .OrderBy(o=> o.OrderName.Value)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(
                new PaginationResult<OrderDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    orders.ToOrderDtoList()));
        }
    }
}
