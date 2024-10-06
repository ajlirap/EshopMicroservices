﻿namespace EshopMicro.CatalogApi.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResults>;
public record GetProductsResults(IEnumerable<Product> Products);
internal class GetProductsQueryHandler(
    IDocumentSession session,
    ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResults>
{
    public async Task<GetProductsResults> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

        var products = await session.Query<Product>()
            .ToListAsync(cancellationToken);

        return new GetProductsResults(products);
    }
}