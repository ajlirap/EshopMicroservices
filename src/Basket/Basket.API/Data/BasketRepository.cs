
using Basket.API.Exceptions;
using Marten;

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session)
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellation = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancellation);
            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellation = default)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancellation);
            return true;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellation = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellation);
            return basket;
        }  
    }
}
