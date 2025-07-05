using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products;

public class UpdateProductUseCase
{
    private readonly IProductGateway _productGateway;
    private readonly GetProductByIdUseCase _getProductByIdUseCase;

    public UpdateProductUseCase(IProductGateway productGateway)
    {
        _productGateway = productGateway;
        _getProductByIdUseCase = GetProductByIdUseCase.Create(productGateway);
    }

    public static UpdateProductUseCase Create(IProductGateway productGateway)
    {
        return new UpdateProductUseCase(productGateway);
    }

    public async Task<Product?> ExecuteAsync(int productId, ProductDto productDto, CancellationToken cancellationToken)
    {
        var product = await _getProductByIdUseCase.ExecuteAsync(productId, false, cancellationToken);

        if (product is null)
            return null;

        product.UpdateProduct(
            productDto.Name,
            productDto.Description,
            productDto.Category,
            productDto.Price,
            productDto.IsActive);

        var updatedProduct = await _productGateway.UpdateProductAsync(product, cancellationToken);

        return updatedProduct;
    }
}