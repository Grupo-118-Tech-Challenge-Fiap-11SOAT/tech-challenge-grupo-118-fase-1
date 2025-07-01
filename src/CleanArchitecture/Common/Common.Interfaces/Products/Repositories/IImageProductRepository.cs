namespace Common.Interfaces.Products.Repositories;

public interface IImageProductRepository
{

    /// <summary>
    /// Adds a new image associated with a specific product.
    /// </summary>
    /// <param name="imageProduct">The image product entity containing the image details to be created.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The unique identifier of the created image product.</returns>
    Task<object> CreateImageProductAsync(object imageProduct,
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
    Task<object?> UpdateImageProductAsync(int productId, int imageId, object imageProduct,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an image product associated with a specific product by its identifier.
    /// </summary>
    /// <param name="productId">The identifier of the product associated with the image.</param>
    /// <param name="imageId">The identifier of the image product to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The requested image product if found, otherwise null.</returns>
    Task<object?> GetImageProductByIdAsync(int productId, int imageId,
        CancellationToken cancellationToken = default);    
}