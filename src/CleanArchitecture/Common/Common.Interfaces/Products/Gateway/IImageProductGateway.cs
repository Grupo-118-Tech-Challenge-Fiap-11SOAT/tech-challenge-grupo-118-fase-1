using ImageProductDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities.ImageProduct;

namespace Common.Interfaces.Products.Gateway;

public interface IImageProductGateway
{
/// <summary>
/// Retrieves a paginated list of images for a given product.
/// </summary>
/// <param name="productId">The ID of the product.</param>
/// <param name="skip">The number of images to skip for pagination.</param>
/// <param name="take">The number of images to take for pagination.</param>
/// <param name="cancellationToken">A token to cancel the operation.</param>
/// <returns>A list of <see cref="ImageProductDomain"/> or null if not found.</returns>
Task<List<ImageProductDomain>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10,
    CancellationToken cancellationToken = default);

/// <summary>
/// Retrieves a specific image by its ID for a given product.
/// </summary>
/// <param name="productId">The ID of the product.</param>
/// <param name="id">The ID of the image.</param>
/// <param name="cancellationToken">A token to cancel the operation.</param>
/// <returns>The <see cref="ImageProductDomain"/> or null if not found.</returns>
Task<ImageProductDomain?> GetImageByIdAsync(int productId, int id, CancellationToken cancellationToken = default);

/// <summary>
/// Updates an existing image for a given product.
/// </summary>
/// <param name="productId">The ID of the product.</param>
/// <param name="imageId">The ID of the image to update.</param>
/// <param name="imageProduct">The updated image entity.</param>
/// <param name="cancellationToken">A token to cancel the operation.</param>
/// <returns>The updated <see cref="ImageProductDomain"/> or null if not found.</returns>
Task<ImageProductDomain?> UpdateImageAsync(int productId, int imageId, ImageProductDomain imageProduct,
    CancellationToken cancellationToken = default);

/// <summary>
/// Deletes an image from a given product.
/// </summary>
/// <param name="productId">The ID of the product.</param>
/// <param name="imageId">The ID of the image to delete.</param>
/// <param name="cancellationToken">A token to cancel the operation.</param>
/// <returns>The number of records deleted.</returns>
Task<int> DeleteImageAsync(int productId, int imageId, CancellationToken cancellationToken = default);

/// <summary>
/// Creates a new image for a given product.
/// </summary>
/// <param name="productId">The ID of the product.</param>
/// <param name="imageProduct">The image entity to create.</param>
/// <param name="cancellationToken">A token to cancel the operation.</param>
/// <returns>The created <see cref="ImageProductDomain"/> or null if creation failed.</returns>
Task<ImageProductDomain?> CreateImageAsync(int productId, ImageProductDomain imageProduct,
    CancellationToken cancellationToken = default);
}