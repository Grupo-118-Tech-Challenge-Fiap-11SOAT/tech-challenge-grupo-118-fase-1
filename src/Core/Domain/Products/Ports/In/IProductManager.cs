using Domain.Products.Dtos;

namespace Domain.Products.Ports.In;

public interface IProductManager
{
    /// <summary>
    /// Retrieves a list of products with pagination.
    /// </summary>
    /// <param name="skip">The number of products to skip from the beginning of the list.</param>
    /// <param name="take">The maximum number of products to include in the result.</param>
    /// <param name="searchActiveProducts">A flag indicating whether to include only active products in the result.</param>
    /// <returns>A task representing an asynchronous operation that returns a list of ProductDto objects.</returns>
    Task<List<ProductDto>?> GetProductsAsync(int skip = 0, int take = 10, bool searchActiveProducts = false);

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to retrieve.</param>
    /// <returns>A task representing an asynchronous operation that returns a ProductDto object if found, or null if the product does not exist.</returns>
    Task<ProductDto?> GetProductByIdAsync(int id);

    /// <summary>
    /// Creates a new product using the provided product details.
    /// </summary>
    /// <param name="productDto">The details of the product to create, encapsulated in a ProductDto object.</param>
    /// <returns>A task representing an asynchronous operation that returns the unique identifier of the created product.</returns>
    Task<int> CreateProductAsync(ProductDto productDto);

    /// <summary>
    /// Updates an existing product with the specified details.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to update.</param>
    /// <param name="productDto">The data transfer object containing updated product details.</param>
    /// <returns>A task representing an asynchronous operation that returns the number of rows affected by the update.</returns>
    Task<int> UpdateProductAsync(int productId, ProductDto productDto);


    /// <summary>
    /// Retrieves a list of product images associated with a specific product, with pagination.
    /// </summary>
    /// <param name="productId">The unique identifier of the product for which to retrieve images.</param>
    /// <param name="skip">The number of images to skip from the beginning of the list.</param>
    /// <param name="take">The maximum number of images to include in the result.</param>
    /// <returns>A task representing an asynchronous operation that returns a list of ImageProductDto objects.</returns>
    Task<List<ImageProductDto>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10);

    /// <summary>
    /// Creates a new image for a specific product using the provided details.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to associate the image with.</param>
    /// <param name="imageProductDto">The details of the image to create, encapsulated in an ImageProductDto object.</param>
    /// <returns>A task representing an asynchronous operation that returns the unique identifier of the created product image.</returns>
    Task<int> CreateImageProductAsync(int productId, ImageProductDto imageProductDto);

    /// <summary>
    /// Deletes an image associated with a specific product.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to which the image belongs.</param>
    /// <param name="imageId">The unique identifier of the image to be deleted.</param>
    /// <returns>A task representing an asynchronous operation that returns the number of records affected.</returns>
    Task<int> DeleteImageProductAsync(int productId, int imageId);

    /// <summary>
    /// Updates the details of an existing product image.
    /// </summary>
    /// <param name="productId">The unique identifier of the product the image belongs to.</param>
    /// <param name="imageId">The unique identifier of the image to update.</param>
    /// <param name="imageProductDto">The updated details of the product image, encapsulated in an ImageProductDto object.</param>
    /// <returns>A task representing an asynchronous operation that returns the unique identifier of the updated image.</returns>
    Task<int> UpdateImageProductAsync(int productId, int imageId, ImageProductDto imageProductDto);
}