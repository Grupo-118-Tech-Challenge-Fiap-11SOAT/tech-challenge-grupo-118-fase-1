using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products.ImageProduct;

public class CreateImageProductUseCase
{
    private readonly IImageProductGateway _imageProductGateway;

    private CreateImageProductUseCase(IImageProductGateway imageProductGateway)
    {
        _imageProductGateway = imageProductGateway;
    }

    public static CreateImageProductUseCase Create(IImageProductGateway imageProductGateway)
    {
        return new CreateImageProductUseCase(imageProductGateway);
    }

    public async Task<Domain.Entities.Products.Entities.ImageProduct?> ExecuteAsync(
        Product product,
        ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        var imageProduct = new Domain.Entities.Products.Entities.ImageProduct(
            product.Id,
            imageProductDto.Position,
            imageProductDto.Url);

        product.AddImage(imageProduct);

        var createdImage = await _imageProductGateway.CreateImageAsync(product.Id, imageProduct, cancellationToken);

        return createdImage;
    }
}