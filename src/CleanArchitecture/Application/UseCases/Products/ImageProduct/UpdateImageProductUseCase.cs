using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products.ImageProduct;

public class UpdateImageProductUseCase
{
    private readonly IImageProductGateway _imageProductGateway;

    private UpdateImageProductUseCase(IImageProductGateway imageProductGateway)
    {
        _imageProductGateway = imageProductGateway;
    }

    public static UpdateImageProductUseCase Create(IImageProductGateway imageProductGateway)
    {
        return new UpdateImageProductUseCase(imageProductGateway);
    }

    public async Task<Domain.Entities.Products.Entities.ImageProduct?> ExecuteAsync(
        Product product,
        int imageId,
        ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        var imageProduct = new Domain.Entities.Products.Entities.ImageProduct(
            product.Id,
            imageProductDto.Position,
            imageProductDto.Url);

        product.ChangeImage(imageProduct);

        var updatedImage =
            await _imageProductGateway.UpdateImageAsync(product.Id, imageId, imageProduct, cancellationToken);

        return updatedImage;
    }
}