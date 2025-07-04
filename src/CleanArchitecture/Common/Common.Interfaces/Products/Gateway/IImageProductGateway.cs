using Common.Dto.Products;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace Common.Interfaces.Products.Gateway;

public interface IImageProductGateway
{
    // Task<List<Product>?> GetProductsAsync(int skip = 0, int take = 10,
    //     bool searchActiveProducts = false,
    //     CancellationToken cancellationToken = default);
    Task<List<ImageProduct>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default);
}