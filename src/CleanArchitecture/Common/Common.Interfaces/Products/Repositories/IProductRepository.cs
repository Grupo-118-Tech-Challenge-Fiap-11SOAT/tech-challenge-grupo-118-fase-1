using Common.Dto.Products;
using Common.Dto.Products.Database;

namespace Common.Interfaces.Products.Repositories;

public interface IProductRepository
{
    /// <summary>
    /// Retrieves a list of products based on pagination parameters.
    /// </summary>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to retrieve.</param>
    /// <param name="searchActiveProducts">A flag indicating whether to include only active products in the result.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of products.</returns>
    Task<List<Product>?> GetProductsAsync(int skip = 0, int take = 10, bool searchActiveProducts = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of products filtered by the specified product type, with optional pagination.
    /// </summary>
    /// <param name="type">The type of the products to retrieve.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token to observe.</param>
    /// <returns>A list of products filtered by the specified type, or null if no products match the criteria.</returns>
    Task<List<Product>?> GetProductsByTypeAsync(ProductType type, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves a list of active products based on their IDs.
    /// </summary>
    /// <param name="ids">An array of product IDs to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing an asynchronous operation that returns a list of active Product objects, or null if no products are found.</returns>
    Task<List<Product>?> GetProductsByIdsAsync(int[] ids,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <param name="includeImages">A flag indicating whether to include product images in the result.</param>
    /// <param name="skip">The number of image product items to skip.</param>
    /// <param name="take">The number of image product items to retrieve.</param>   
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The product matching the specified identifier, or null if no such product exists.</returns>
    Task<Product?> GetProductByIdAsync(int id, bool includeImages = false, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new product in the system.
    /// </summary>
    /// <param name="product">The product entity containing the details of the product to be created.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The unique identifier of the created product.</returns>
    Task<Product> CreateProductAsync(Product product,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product with the provided details.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to be updated.</param>
    /// <param name="product">The updated product data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated Product entity</returns>
    Task<Product?> UpdateProductAsync(int productId, Product product,
        CancellationToken cancellationToken = default);
}