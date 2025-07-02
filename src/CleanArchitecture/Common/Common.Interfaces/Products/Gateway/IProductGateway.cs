

using ProductDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities.Product;
using ProductEntity = Common.Dto.Products.Database.Product;

namespace Common.Interfaces.Products.Gateway;

public interface IProductGateway
{
    Task<List<ProductDomain>?> GetProductsAsync(int skip = 0, int take = 10,
        bool searchActiveProducts = false,
        CancellationToken cancellationToken = default);

    Task<ProductDomain> CreateProductAsync(ProductDomain product, CancellationToken cancellationToken = default);
}