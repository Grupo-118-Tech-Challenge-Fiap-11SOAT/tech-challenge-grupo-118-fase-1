using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Products;

public class CreateProductUseCase
{
    private readonly IProductGateway _productGateway;

    public CreateProductUseCase(IProductGateway productGateway)
    {
        _productGateway = productGateway;
    }

    public static CreateProductUseCase Create(IProductGateway productGateway)
    {
        return new CreateProductUseCase(productGateway);
    }

    public async Task<Product> ExecuteAsync(ProductDto productDto, CancellationToken cancellationToken)
    {
        var product = new Product(productDto.Name, productDto.Description, productDto.Category, productDto.Price,
            productDto.IsActive);

        var persistedProduct = await _productGateway.CreateProductAsync(product, cancellationToken);

        return persistedProduct;
    }
}