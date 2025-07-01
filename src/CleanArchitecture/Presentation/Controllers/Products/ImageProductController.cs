using Common.Dto.Products;
using Common.Interfaces.Products.Controller;
using Common.Interfaces.Products.Gateway;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Products;

public class ImageProductController : IImageProductController
{
    public ImageProductController(IImageProductGateway imageProductGateway)
    {
        // Instantiate use cases
    }

    public async Task<List<ImageProductDto>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<ImageProductDto?> GetProductImageByIdAsync(int productId, int imageId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<ImageProductDto?> CreateImageProductAsync(int productId, ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<ImageProductDto?> UpdateImageProductAsync(int productId, int imageId,
        ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteImageProductAsync(int productId, int imageId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}