using Common.Interfaces.Products.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products.ImageProduct;

public class DeleteImageProductUseCase
{
    private readonly IImageProductGateway _imageProductGateway;

    private DeleteImageProductUseCase(IImageProductGateway imageProductGateway)
    {
        _imageProductGateway = imageProductGateway;
    }

    public static DeleteImageProductUseCase Create(IImageProductGateway imageProductGateway)
    {
        return new DeleteImageProductUseCase(imageProductGateway);
    }

    public async Task<int> ExecuteAsync(int productId, int imageId, CancellationToken cancellationToken = default)
    {
        return await _imageProductGateway.DeleteImageAsync(productId, imageId, cancellationToken);
    }
}