using Common.Dto.Products;
using Common.Interfaces.Products.Gateway;
using Common.Interfaces.Products.Repositories;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Products;

public class ProductGateway : IProductGateway
{
    private readonly IProductRepository _productRepository;

    public ProductGateway(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>?> GetProductsAsync(int skip = 0, int take = 10,
        bool searchActiveProducts = false,
        CancellationToken cancellationToken = default)
    {
        var persistedProducts =
            await _productRepository.GetProductsAsync(skip, take, searchActiveProducts, cancellationToken);

        if (persistedProducts == null)
            return null;

        var productsDto = new List<Product>();

        persistedProducts.ForEach(productEntity =>
        {
            productsDto.Add(
                new Product(
                    productEntity.Name,
                    productEntity.Description,
                    productEntity.Category,
                    productEntity.Price,
                    productEntity.IsActive,
                    productEntity.Id
                ));
        });

        return productsDto;
    }
}