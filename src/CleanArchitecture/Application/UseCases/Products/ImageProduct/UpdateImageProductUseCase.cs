using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products.ImageProduct;

public class UpdateImageProductUseCase
{
    private readonly IImageProductGateway _imageProductGateway;
    private readonly GetProductByIdUseCase _getProductByIdUseCase;

    private UpdateImageProductUseCase(IImageProductGateway imageProductGateway, IProductGateway productGateway)
    {
        _imageProductGateway = imageProductGateway;
        _getProductByIdUseCase = GetProductByIdUseCase.Create(productGateway);
    }

    public static UpdateImageProductUseCase Create(IImageProductGateway imageProductGateway,
        IProductGateway productGateway)
    {
        return new UpdateImageProductUseCase(imageProductGateway, productGateway);
    }

    public async Task<Domain.Entities.Products.Entities.ImageProduct?> ExecuteAsync(int productId, int imageId,
        ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        var product = await _getProductByIdUseCase.ExecuteAsync(productId, true, cancellationToken);

        if (product is null)
            return null;

        var imageProduct = new Domain.Entities.Products.Entities.ImageProduct(
            productId,
            imageProductDto.Position,
            imageProductDto.Url);

        product.ChangeImage(imageProduct);

        var updatedImage =
            await _imageProductGateway.UpdateImageAsync(productId, imageId, imageProduct, cancellationToken);

        return updatedImage;
    }
}