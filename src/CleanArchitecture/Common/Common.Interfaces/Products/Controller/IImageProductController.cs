using Common.Dto.Products;

namespace Common.Interfaces.Products.Controller;

public interface IImageProductController
{
    /// <summary>
    /// Retrieves a paginated list of images for a specific product.
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="skip">The number of images to skip for pagination.</param>
    /// <param name="take">The number of images to take for pagination.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A list of <see cref="ImageProductDto"/> objects, or null if not found.</returns>
    Task<List<ImageProductDto>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a specific image by its ID for a given product.
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="imageId">The ID of the image.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An <see cref="ImageProductDto"/> object, or null if not found.</returns>
    Task<ImageProductDto?> GetProductImageByIdAsync(int productId, int imageId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new image for a specific product.
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="imageProductDto">The image data to create.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The created <see cref="ImageProductDto"/> object, or null if creation failed.</returns>
    Task<ImageProductDto?> CreateImageProductAsync(int productId, ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing image for a specific product.
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="imageId">The ID of the image to update.</param>
    /// <param name="imageProductDto">The updated image data.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The updated <see cref="ImageProductDto"/> object, or null if update failed.</returns>
    Task<ImageProductDto?> UpdateImageProductAsync(int productId, int imageId, ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an image from a specific product.
    /// </summary>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="imageId">The ID of the image to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The number of records deleted.</returns>
    Task<int> DeleteImageProductAsync(int productId, int imageId, CancellationToken cancellationToken = default);
}