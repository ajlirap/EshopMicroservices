using EshopMicro.Common.Exceptions;

namespace EshopMicro.CatalogApi.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id)
        : base("Product", Id)
    {
    }
}
