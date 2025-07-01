using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace Common.Interfaces.Products.Gateway;

public interface IProductGateway
{
    Task<List<Product>?> GetProductsAsync(int skip = 0, int take = 10,
        bool searchActiveProducts = false,
        CancellationToken cancellationToken = default);
}