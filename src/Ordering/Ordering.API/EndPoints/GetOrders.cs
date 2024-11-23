using EshopMicro.Common.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.EndPoints;

public record GetOrdersResponse(PaginationResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request ,ISender sender) =>
        {
            var result = await sender.Send(new GetOrderQuery(request));

            var response = result.Adapt<GetOrdersResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrders")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get orders")
        .WithDescription("Get orders in the system.");
    }
}
