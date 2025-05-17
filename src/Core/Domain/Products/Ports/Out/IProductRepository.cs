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
    /// <returns>A list of products.</returns>
    Task<List<Product>?> GetProductsAsync(int skip = 0, int take = 10, bool searchActiveProducts = false);

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <returns>The product matching the specified identifier, or null if no such product exists.</returns>
    Task<Product?> GetProductByIdAsync(int id);

    /// <summary>
    /// Creates a new product in the system.
    /// </summary>
    /// <param name="product">The product entity containing the details of the product to be created.</param>
    /// <returns>The unique identifier of the created product.</returns>
    Task<int> CreateProductAsync(Product product);

    /// <summary>
    /// Updates an existing product with the provided details.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to be updated.</param>
    /// <param name="product">The updated product data.</param>
    /// <returns>The number of affected rows in the database.</returns>
    Task<int> UpdateProductAsync(int productId, Product product);

    /// <summary>
    /// Retrieves a list of product images based on the product identifier and pagination parameters.
    /// </summary>
    /// <param name="productId">The unique identifier of the product whose images are to be retrieved.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to retrieve.</param>
    /// <returns>A list of product images associated with the specified product.</returns>
    Task<List<ImageProduct>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10);

    /// <summary>
    /// Adds a new image associated with a specific product.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to which the image belongs.</param>
    /// <param name="imageProduct">The image product entity containing the image details to be created.</param>
    /// <returns>The unique identifier of the created image product.</returns>
    Task<int> CreateImageProductAsync(ImageProduct imageProduct);

    /// <summary>
    /// Deletes an image associated with a specific product.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to which the image belongs.</param>
    /// <param name="imageId">The unique identifier of the image to be deleted.</param>
    /// <returns>A task representing an asynchronous operation that returns the number of records affected.</returns>
    Task<int> DeleteImageProductAsync(int productId, int imageId);

    /// <summary>
    /// Updates an existing product image with the provided details.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to which the image belongs.</param>
    /// <param name="imageId">The unique identifier of the image to be updated.</param>
    /// <param name="imageProduct">The updated details of the image, including position and URL.</param>
    /// <returns>The number of rows affected by the update operation.</returns>
    Task<int> UpdateImageProductAsync(int productId, int imageId, ImageProduct imageProduct);
}