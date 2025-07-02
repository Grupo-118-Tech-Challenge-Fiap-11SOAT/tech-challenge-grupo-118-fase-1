using Common.Dto.Products.Database;
using Common.Interfaces.Products.Gateway;
using Common.Interfaces.Products.Repositories;
using ProductDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities.Product;
using ProductEntity = Common.Dto.Products.Database.Product;

namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Products;

public class ProductGateway : IProductGateway
{
    private readonly IProductRepository _productRepository;

    public ProductGateway(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductDomain>?> GetProductsAsync(int skip = 0, int take = 10,
        bool searchActiveProducts = false,
        CancellationToken cancellationToken = default)
    {
        var persistedProducts =
            await _productRepository.GetProductsAsync(skip, take, searchActiveProducts, cancellationToken);

        if (persistedProducts == null)
            return null;

        var productsDto = new List<ProductDomain>();

        persistedProducts.ForEach(productEntity =>
        {
            productsDto.Add(
                new ProductDomain(
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

    public async Task<ProductDomain> CreateProductAsync(ProductDomain product, CancellationToken cancellationToken = default)
    {
        var productEntity = new ProductEntity(
            product.Name,
            product.Description,
            product.Category,
            product.Price,
            product.IsActive
        );

        var persistedProduct = await _productRepository.CreateProductAsync(productEntity, cancellationToken);

        product.Id = persistedProduct.Id;
        
        return product;
    }
}