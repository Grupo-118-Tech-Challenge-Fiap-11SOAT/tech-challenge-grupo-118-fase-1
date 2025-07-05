using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products.ImageProduct;

public class CreateImageProductUseCase
{
    private readonly IImageProductGateway _imageProductGateway;
    private readonly GetProductByIdUseCase _getProductByIdUseCase;

    private CreateImageProductUseCase(IImageProductGateway imageProductGateway, IProductGateway productGateway)
    {
        _imageProductGateway = imageProductGateway;
        _getProductByIdUseCase = GetProductByIdUseCase.Create(productGateway);
    }

    public static CreateImageProductUseCase Create(IImageProductGateway imageProductGateway,
        IProductGateway productGateway)
    {
        return new CreateImageProductUseCase(imageProductGateway, productGateway);
    }

    public async Task<Domain.Entities.Products.Entities.ImageProduct?> ExecuteAsync(int productId,
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

        product.AddImage(imageProduct);

        var createdImage = await _imageProductGateway.CreateImageAsync(productId, imageProduct, cancellationToken);

        return createdImage;
    }
}