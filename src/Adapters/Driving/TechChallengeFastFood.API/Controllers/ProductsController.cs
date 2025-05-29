using Domain;
using Domain.Products.Dtos;
using Domain.Products.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.API.Controllers;

/// <summary>
/// Endpoint to Manage Products
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductManager _productManager;

    /// <summary>
    /// Product constructor
    /// </summary>
    /// <param name="productManager"></param>
    public ProductsController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    /// <summary>
    /// Retrieves a paginated list of products.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="skip">The number of products to skip from the start of the list.</param>
    /// <param name="take">The maximum number of products to retrieve.</param>
    /// <param name="searchActiveProducts">A flag indicating whether to include only active products in the result.</param>
    /// <returns>A task representing an asynchronous operation that returns an IActionResult containing a list of products if available, or a no-content response if no products are found.</returns>
    [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken, int skip = 0, int take = 10,
        bool searchActiveProducts = false)
    {
        var products =
            await _productManager.GetProductsAsync(skip, take, searchActiveProducts,
                cancellationToken: cancellationToken);

        if (products is null || products.Count == 0)
            return NotFound();

        return Ok(products);
    }

    /// <summary>
    /// Retrieves a list of products by their category (type).
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="category">The category/type of the products to retrieve.</param>
    /// <param name="skip">The number of products to skip from the start of the list.</param>
    /// <param name="take">The maximum number of products to retrieve.</param>
    /// <returns>A list of products that match the specified category.</returns>
    [HttpGet("type/{category}")]
    [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByTypeAsync(CancellationToken cancellationToken, string category, int skip = 0, int take = 10)
    {
        var products = await _productManager.GetProductsByTypeAsync(category, skip, take, cancellationToken);

        if (products is null || products.Count == 0)
            return NotFound();

        return Ok(products);
    }


    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing an asynchronous operation that returns an IActionResult containing the product details if found, or a no-content response if not found.</returns>
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{productId}"), ActionName("GetDetailedProduct")]
    public async Task<IActionResult> GetAsync(int productId, CancellationToken cancellationToken)
    {
        var product = await _productManager.GetProductByIdAsync(productId, cancellationToken: cancellationToken);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="productDto">The product data transfer object containing information about the product to create.</param>
    /// <returns>The created Product.</returns>
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> PostAsync(CancellationToken cancellationToken, [FromBody] ProductDto productDto)
    {
        var product = await _productManager.CreateProductAsync(productDto, cancellationToken: cancellationToken);
        return CreatedAtAction("GetDetailedProduct", new { productId = product.Id }, product);
    }

    /// <summary>
    /// Updates an existing product with the specified product details.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="productId">The unique identifier of the product to be updated.</param>
    /// <param name="productDto">The object containing the updated product information.</param>
    /// <returns>Returns an HTTP result indicating the outcome of the update operation with the modified entity.</returns>
    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> PutAsync(CancellationToken cancellationToken, int productId,
        [FromBody] ProductDto productDto)
    {
        var product =
            await _productManager.UpdateProductAsync(productId, productDto, cancellationToken: cancellationToken);

        if (product is null)
            return Accepted();

        return Ok(product);
    }

    #region Image Products Methods

    /// <summary>
    /// Retrieves a paginated list of images associated with a specific product.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="productId">The unique identifier of the product for which images are being retrieved.</param>
    /// <param name="skip">The number of images to skip from the start of the list.</param>
    /// <param name="take">The maximum number of images to retrieve.</param>
    /// <returns>A task representing an asynchronous operation that returns an IActionResult containing a list of ImageProductDto objects if found, or a no-content response if no images are available.</returns>
    [HttpGet("{productId}/images")]
    [ProducesResponseType(typeof(List<ImageProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken, int productId, int skip = 0,
        int take = 10)
    {
        var images =
            await _productManager.GetProductImagesAsync(productId, skip, take, cancellationToken: cancellationToken);

        if (images is null || images.Count == 0)
            return NotFound();

        return Ok(images);
    }

    /// <summary>
    /// Retrieves the details of a specific image associated with a product.
    /// </summary>
    /// <param name="productId">The unique identifier of the product.</param>
    /// <param name="imageId">The unique identifier of the image.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A specific image product</returns>
    [HttpGet("{productId}/images/{imageId}"), ActionName("GetDetailedImageProduct")]
    [ProducesResponseType(typeof(ImageProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(int productId, int imageId, CancellationToken cancellationToken)
    {
        var imageProduct = await _productManager.GetProductImageByIdAsync(productId, imageId, cancellationToken);

        if (imageProduct is null)
            return NotFound();

        return Ok(imageProduct);
    }

    /// <summary>
    /// Adds a new image to a product.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="productId">The ID of the product to which the image will be added.</param>
    /// <param name="productImageDto">The data of the product image to be added.</param>
    /// <returns>The result of the operation, including the status and any related data.</returns>
    [HttpPost("{productId}/images")]
    [ProducesResponseType(typeof(ImageProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CancellationToken cancellationToken, int productId,
        [FromBody] ImageProductDto productImageDto)
    {
        var createdImageProduct = await _productManager.CreateImageProductAsync(productId, productImageDto,
            cancellationToken: cancellationToken);

        if (createdImageProduct is null)
            return Accepted();

        return CreatedAtAction("GetDetailedImageProduct", new { productId, imageId = createdImageProduct.Id },
            createdImageProduct);
    }

    /// <summary>
    /// Deletes a product image associated with the provided product ID and image ID.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="productId">The unique identifier of the product that the image belongs to.</param>
    /// <param name="imageId">The unique identifier of the image to be deleted.</param>
    /// <returns>A task representing the asynchronous operation that removes the specified product image.</returns>
    [HttpDelete("{productId}/images/{imageId}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(CancellationToken cancellationToken, int productId, int imageId)
    {
        var affectedRows =
            await _productManager.DeleteImageProductAsync(productId, imageId, cancellationToken: cancellationToken);

        if (affectedRows > 0)
            return Ok();

        return NoContent();
    }

    /// <summary>
    /// Updates an existing product image for a specified product.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="productId">The unique identifier of the product to which the image belongs.</param>
    /// <param name="imageId">The unique identifier of the image to be updated.</param>
    /// <param name="productImageDto">The updated details of the product image.</param>
    /// <returns>The updated Image Product</returns>
    [HttpPut("{productId}/images/{imageId}")]
    [ProducesResponseType(typeof(ImageProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(CancellationToken cancellationToken, int productId, int imageId,
        [FromBody] ImageProductDto productImageDto)
    {
        var updatedImageProduct = await _productManager.UpdateImageProductAsync(productId, imageId, productImageDto,
            cancellationToken: cancellationToken);

        if (updatedImageProduct is null)
            return Accepted();

        return Ok(updatedImageProduct);
    }

    #endregion
}