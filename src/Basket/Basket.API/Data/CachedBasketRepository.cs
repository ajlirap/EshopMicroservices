using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

public class CachedBasketRepository (IBasketRepository repository, IDistributedCache cache)
    : IBasketRepository
{

    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellation = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellation);
        if (!string.IsNullOrEmpty(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }

        var basket = await repository.GetBasketAsync(userName, cancellation);

        await cache.SetStringAsync(
            userName, 
            JsonSerializer.Serialize(basket),
            cancellation);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellation = default)
    {
        await repository.StoreBasketAsync(basket, cancellation);

        await cache.SetStringAsync(
            basket.UserName,
            JsonSerializer.Serialize(basket),
            cancellation);

        return basket;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellation = default)
    {
        await repository.DeleteBasketAsync(userName, cancellation);

        await cache.RemoveAsync(userName, cancellation);

        return true;
    }

}
