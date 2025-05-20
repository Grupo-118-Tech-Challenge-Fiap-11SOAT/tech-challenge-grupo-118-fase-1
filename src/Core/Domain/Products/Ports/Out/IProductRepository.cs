using Domain.Products.Entities;

namespace Domain.Products.Ports.Out;

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
    Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product with the provided details.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to be updated.</param>
    /// <param name="product">The updated product data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated Product entity</returns>
    Task<Product?> UpdateProductAsync(int productId, Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new image associated with a specific product.
    /// </summary>
    /// <param name="imageProduct">The image product entity containing the image details to be created.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The unique identifier of the created image product.</returns>
    Task<ImageProduct> CreateImageProductAsync(ImageProduct imageProduct,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an image associated with a specific product.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to which the image belongs.</param>
    /// <param name="imageId">The unique identifier of the image to be deleted.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing an asynchronous operation that returns the number of records affected.</returns>
    Task<int> DeleteImageProductAsync(int productId, int imageId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product image with the provided details.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to which the image belongs.</param>
    /// <param name="imageId">The unique identifier of the image to be updated.</param>
    /// <param name="imageProduct">The updated details of the image, including position and URL.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated Image Product entity</returns>
    Task<ImageProduct?> UpdateImageProductAsync(int productId, int imageId, ImageProduct imageProduct,
        CancellationToken cancellationToken = default);
}