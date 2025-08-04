using Common.Enums;
using ProductDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities.Product;
using ProductEntity = Common.Dto.Products.Database.Product;

namespace Common.Interfaces.Products.Gateway;

public interface IProductGateway
{
    /// <summary>
    /// Retrieves a list of products with optional pagination and filtering for active products.
    /// </summary>
    /// <param name="skip">The number of items to skip (for pagination).</param>
    /// <param name="take">The number of items to take (for pagination).</param>
    /// <param name="searchActiveProducts">Indicates whether to filter only active products.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>A list of products or null if no products are found.</returns>
    Task<List<ProductDomain>?> GetProductsAsync(int skip = 0, int take = 10,
        bool searchActiveProducts = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of products filtered by their type with optional pagination.
    /// </summary>
    /// <param name="productType">The type of the products to retrieve.</param>
    /// <param name="skip">The number of items to skip (for pagination).</param>
    /// <param name="take">The number of items to take (for pagination).</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>A list of products of the specified type or null if no products are found.</returns>
    Task<List<ProductDomain>?> GetProductsByTypeAsync(ProductType productType, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of products by their IDs.
    /// </summary>
    /// <param name="productIds">An array of product IDs to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>A list of products matching the specified IDs or null if no products are found.</returns>
    Task<List<ProductDomain>?> GetProductsByIdsAsync(int[] productIds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a single product by its ID with an option to include its image.
    /// </summary>
    /// <param name="productId">The ID of the product to retrieve.</param>
    /// <param name="includeImage">Indicates whether to include the product's image.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>The product matching the specified ID or null if not found.</returns>
    Task<ProductDomain?> GetProductByIdAsync(int productId, bool includeImage = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="product">The product entity to create.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>The created product entity.</returns>
    Task<ProductDomain> CreateProductAsync(ProductDomain product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product by its ID.
    /// </summary>
    /// <param name="product">The updated product entity.</param>
    /// <param name="cancellationToken">The cancellation token for async operations.</param>
    /// <returns>The updated product entity or null if the product is not found.</returns>
    Task<ProductDomain?> UpdateProductAsync(ProductDomain product, CancellationToken cancellationToken = default);
}