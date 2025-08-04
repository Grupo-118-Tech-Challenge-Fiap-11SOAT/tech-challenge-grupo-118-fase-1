using Common.Enums;
using Common.Interfaces.Products.Gateway;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products.Exceptions;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products;

/// <summary>
/// Use case for retrieving products by type.
/// </summary>
public class GetProductByTypeUseCase
{
    private readonly IProductGateway _productGateway;

    public GetProductByTypeUseCase(IProductGateway productGateway)
    {
        _productGateway = productGateway;
    }

    public static GetProductByTypeUseCase Create(IProductGateway productGateway)
    {
        return new GetProductByTypeUseCase(productGateway);
    }

    /// <summary>
    /// Executes the use case to retrieve products by type.
    /// </summary>
    /// <param name="type">The product type as a string.</param>
    /// <param name="skip">The number of items to skip (for pagination).</param>
    /// <param name="take">The number of items to take (for pagination).</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>A list of products of the specified type, or null.</returns>
    /// <exception cref="InvalidProductCategoryException">Thrown if the product type is invalid.</exception>
    public async Task<List<Product>?> ExecuteAsync(string type, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse(type, ignoreCase: true, out ProductType productType))
            throw new InvalidProductCategoryException();

        return await _productGateway.GetProductsByTypeAsync(productType, skip, take, cancellationToken);
    }
}