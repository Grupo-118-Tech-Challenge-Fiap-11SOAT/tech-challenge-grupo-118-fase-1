using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products.ImageProduct;

public class GetProductImagesUseCase
{
    private readonly IImageProductGateway _imageProductGateway;

    private GetProductImagesUseCase(IImageProductGateway imageProductGateway)
    {
        _imageProductGateway = imageProductGateway;
    }

    public static GetProductImagesUseCase Create(IImageProductGateway imageProductGateway)
    {
        return new GetProductImagesUseCase(imageProductGateway);
    }

    public async Task<List<Domain.Entities.Products.Entities.ImageProduct>?> ExecuteAsync(int productId, int skip = 0,
        int take = 10,
        CancellationToken cancellationToken = default)
    {
        var images = await _imageProductGateway.GetProductImagesAsync(productId, skip, take, cancellationToken);

        return images;
    }
}