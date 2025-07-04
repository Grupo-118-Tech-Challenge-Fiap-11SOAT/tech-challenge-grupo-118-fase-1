using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;
using Common.Interfaces.Products.Repositories;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products;

public class GetProductsUseCase
{
    private readonly IProductGateway _productGateway;

    public GetProductsUseCase(IProductGateway productGateway)
    {
        _productGateway = productGateway;
    }

    public static GetProductsUseCase Create(IProductGateway productGateway)
    {
        return new GetProductsUseCase(productGateway);
    }

    public async Task<List<Product>?> ExecuteAsync(int skip = 0, int take = 10,
        bool searchActiveProducts = false,
        CancellationToken cancellationToken = default)
    {
        return await _productGateway.GetProductsAsync(skip, take, searchActiveProducts, cancellationToken);
    }
}