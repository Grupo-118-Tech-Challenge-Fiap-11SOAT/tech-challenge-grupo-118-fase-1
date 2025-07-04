using Common.Interfaces.Products.Gateway;
using Common.Interfaces.Products.Repositories;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Products;

public class ImageProductGateway : IImageProductGateway
{
    private readonly IImageProductRepository _imageProductRepository;

    public ImageProductGateway(IImageProductRepository imageProductRepository)
    {
        _imageProductRepository = imageProductRepository;
    }

    public async Task<List<ImageProduct>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}