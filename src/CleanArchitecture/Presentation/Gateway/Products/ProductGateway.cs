using Common.Dto.Products.Database;
using Common.Enums;
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

    public static IProductGateway Create(IProductRepository productRepository)
    {
        return new ProductGateway(productRepository);
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
                    productEntity.Id,
                    createdAt: productEntity.CreatedAt,
                    updatedAt: productEntity.UpdatedAt
                ));
        });

        return productsDto;
    }

    public async Task<ProductDomain?> GetProductByIdAsync(int productId, bool includeImage = false,
        CancellationToken cancellationToken = default)
    {
        var persistedProduct =
            await _productRepository.GetProductByIdAsync(productId, includeImage, cancellationToken: cancellationToken);

        if (persistedProduct == null)
            return null;

        return new ProductDomain(
            persistedProduct.Name,
            persistedProduct.Description,
            persistedProduct.Category,
            persistedProduct.Price,
            persistedProduct.IsActive,
            persistedProduct.Id,
            createdAt: persistedProduct.CreatedAt,
            updatedAt: persistedProduct.UpdatedAt
        );
    }

    public async Task<ProductDomain> CreateProductAsync(ProductDomain product,
        CancellationToken cancellationToken = default)
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

    public async Task<ProductDomain?> UpdateProductAsync(ProductDomain product,
        CancellationToken cancellationToken = default)
    {
        var productEntity = new ProductEntity(
            product.Name,
            product.Description,
            product.Category,
            product.Price,
            product.IsActive,
            product.Id
        );

        productEntity.UpdatedAt = DateTimeOffset.UtcNow;
        productEntity.CreatedAt = product.CreatedAt;

        await _productRepository.UpdateProductAsync(productEntity, cancellationToken);

        return product;
    }

    public async Task<List<ProductDomain>?> GetProductsByTypeAsync(ProductType productType, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        var persistedProducts =
            await _productRepository.GetProductsByTypeAsync(productType, skip, take, cancellationToken);

        if (persistedProducts == null)
            return null;

        return ConvertDatabaseProductsToDomain(persistedProducts);
    }

    public async Task<List<ProductDomain>?> GetProductsByIdsAsync(int[] productIds,
        CancellationToken cancellationToken = default)
    {
        var persistedProducts =
            await _productRepository.GetProductsByIdsAsync(productIds, cancellationToken);

        if (persistedProducts == null)
            return null;

        return ConvertDatabaseProductsToDomain(persistedProducts);
    }

    private List<ProductDomain>? ConvertDatabaseProductsToDomain(List<ProductEntity> productEntities)
    {
        var productsDto = new List<ProductDomain>();

        productEntities.ForEach(productEntity =>
        {
            productsDto.Add(
                new ProductDomain(
                    productEntity.Name,
                    productEntity.Description,
                    productEntity.Category,
                    productEntity.Price,
                    productEntity.IsActive,
                    productEntity.Id,
                    createdAt: productEntity.CreatedAt,
                    updatedAt: productEntity.UpdatedAt
                    
                ));
        });

        return productsDto;
    }
}