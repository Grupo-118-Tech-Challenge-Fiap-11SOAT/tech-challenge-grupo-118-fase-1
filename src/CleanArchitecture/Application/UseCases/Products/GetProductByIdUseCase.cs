using Common.Interfaces.Products.Gateway;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products;

/// <summary>
/// Use case for retrieving a product by its ID.
/// This use case is typically used in scenarios where a specific product needs to be fetched, such as in product detail views or when performing operations that require knowledge of a specific product.
/// </summary>
public class GetProductByIdUseCase
{
    private readonly IProductGateway _productGateway;

    public GetProductByIdUseCase(IProductGateway productGateway)
    {
        _productGateway = productGateway;
    }

    public static GetProductByIdUseCase Create(IProductGateway productGateway)
    {
        return new GetProductByIdUseCase(productGateway);
    }

    public async Task<Product?> ExecuteAsync(int id, bool includeImage, CancellationToken cancellationToken = default)
    {
        return await _productGateway.GetProductByIdAsync(id, includeImage, cancellationToken);
    }
}