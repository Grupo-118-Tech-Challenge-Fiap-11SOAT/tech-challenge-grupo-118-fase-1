using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products.Exceptions;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products;

/// <summary>
/// Use case for retrieving active products by their IDs. Used on Order scenario
/// </summary>
public class GetActiveProductsByIdsUseCase
{
    private readonly IProductGateway _productGateway;

    public GetActiveProductsByIdsUseCase(IProductGateway productGateway)
    {
        _productGateway = productGateway;
    }

    public static GetActiveProductsByIdsUseCase Create(IProductGateway productGateway)
    {
        return new GetActiveProductsByIdsUseCase(productGateway);
    }

    /// <summary>
    /// Executes the use case to retrieve active products by their IDs.
    /// This method checks if the products exist and are active.
    /// If any product is not found or is deactivated, it throws an exception.
    /// </summary>
    /// <param name="productIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidProductException"></exception>
    /// <exception cref="DeactivatedProductException"></exception>
    public async Task<List<Product?>> ExecuteAsync(int[] productIds, CancellationToken cancellationToken = default)
    {
        if (productIds == null || productIds.Length == 0)
            return new List<Product?>();

        var products = await _productGateway.GetProductsByIdsAsync(productIds, cancellationToken);

        if (products is null || products.Count == 0 || products.Count < productIds.Length)
            throw new InvalidProductException();

        if (products.Any(p => !p.IsActive))
            throw new DeactivatedProductException();

        return products;
    }
}