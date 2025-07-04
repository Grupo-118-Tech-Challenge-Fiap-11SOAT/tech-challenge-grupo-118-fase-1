using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products;

public class UpdateProductUseCase
{
    private readonly IProductGateway _productGateway;

    public UpdateProductUseCase(IProductGateway productGateway)
    {
        _productGateway = productGateway;
    }

    public static UpdateProductUseCase Create(IProductGateway productGateway)
    {
        return new UpdateProductUseCase(productGateway);
    }

    public async Task<Product?> ExecuteAsync(int productId, ProductDto productDto, CancellationToken cancellationToken)
    {
        var productToUpdate = new Product(
            productDto.Name,
            productDto.Description,
            productDto.Category,
            productDto.Price,
            productDto.IsActive,
            productId
        );

        var updatedProduct = await _productGateway.UpdateProductAsync(productId, productToUpdate, cancellationToken);

        return updatedProduct;
    }
}