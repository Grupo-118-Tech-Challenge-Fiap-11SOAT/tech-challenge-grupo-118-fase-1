using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products.ImageProduct;

public class GetProductImageByIdUseCase
{
    private readonly IImageProductGateway _imageProductGateway;

    private GetProductImageByIdUseCase(IImageProductGateway imageProductGateway)
    {
        _imageProductGateway = imageProductGateway;
    }

    public static GetProductImageByIdUseCase Create(IImageProductGateway imageProductGateway)
    {
        return new GetProductImageByIdUseCase(imageProductGateway);
    }

    public async Task<Domain.Entities.Products.Entities.ImageProduct?> ExecuteAsync(int productId, int imageId,
        CancellationToken cancellationToken = default)
    {
        var image = await _imageProductGateway.GetImageByIdAsync(productId, imageId, cancellationToken);
        return image;
    }
}