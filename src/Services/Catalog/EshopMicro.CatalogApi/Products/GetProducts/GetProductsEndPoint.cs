﻿namespace EshopMicro.CatalogApi.Products.GetProducts;

public record GetProductsResponse (IEnumerable<Product> Products);
public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var query = new GetProductsQuery();
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get all products")
        .WithDescription("Get all products in the catalog");
    }
}