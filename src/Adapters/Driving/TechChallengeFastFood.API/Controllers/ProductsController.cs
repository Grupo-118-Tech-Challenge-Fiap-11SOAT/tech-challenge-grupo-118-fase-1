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
    /// <param name="skip">The number of products to skip from the start of the list.</param>
    /// <param name="take">The maximum number of products to retrieve.</param>
    /// <param name="searchActiveProducts">A flag indicating whether to include only active products in the result.</param>
    /// <returns>A task representing an asynchronous operation that returns an IActionResult containing a list of products if available, or a no content response if no products are found.</returns>
    [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<IActionResult> GetAsync(int skip = 0, int take = 10, bool searchActiveProducts = false)
    {
        var products = await _productManager.GetProductsAsync(skip, take, searchActiveProducts);

        if (products is null || products.Count == 0)
            return NoContent();

        return Ok(products);
    }

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to retrieve.</param>
    /// <returns>A task representing an asynchronous operation that returns an IActionResult containing the product details if found, or a no content response if not found.</returns>
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetAsync(int productId)
    {
        return Ok(await _productManager.GetProductByIdAsync(productId));
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="productDto">The product data transfer object containing information about the product to create.</param>
    /// <returns>The ID of the newly created product.</returns>
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] ProductDto productDto)
    {
        try
        {
            var productId = await _productManager.CreateProductAsync(productDto);
            return new ObjectResult(productId) { StatusCode = StatusCodes.Status201Created };
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Updates an existing product with the specified product details.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to be updated.</param>
    /// <param name="productDto">The object containing the updated product information.</param>
    /// <returns>Returns an HTTP result indicating the outcome of the update operation.</returns>
    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> PutAsync(int productId, [FromBody] ProductDto productDto)
    {
        var affectedRows = await _productManager.UpdateProductAsync(productId, productDto);

        if (affectedRows > IntPtr.Zero)
            return Ok(affectedRows);

        return Accepted();
    }

    /// <summary>
    /// Retrieves a paginated list of images associated with a specific product.
    /// </summary>
    /// <param name="productId">The unique identifier of the product for which images are being retrieved.</param>
    /// <param name="skip">The number of images to skip from the start of the list.</param>
    /// <param name="take">The maximum number of images to retrieve.</param>
    /// <returns>A task representing an asynchronous operation that returns an IActionResult containing a list of ImageProductDto objects if found, or a no content response if no images are available.</returns>
    [HttpGet("{productId}/images")]
    [ProducesResponseType(typeof(List<ImageProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAsync(int productId, int skip = 0, int take = 10)
    {
        var images = await _productManager.GetProductImagesAsync(productId, skip, take);

        if (images is null || images.Count == 0)
            return NoContent();

        return Ok(images);
    }

    /// <summary>
    /// Adds a new image to a product.
    /// </summary>
    /// <param name="productId">The ID of the product to which the image will be added.</param>
    /// <param name="productImageDto">The data of the product image to be added.</param>
    /// <returns>The result of the operation, including the status and any related data.</returns>
    [HttpPost("{productId}/images")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(int productId, [FromBody] ImageProductDto productImageDto)
    {
        try
        {
            var imageId = await _productManager.CreateImageProductAsync(productId, productImageDto);
            return new ObjectResult(imageId) { StatusCode = StatusCodes.Status201Created };
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Deletes a product image associated with the provided product ID and image ID.
    /// </summary>
    /// <param name="productId">The unique identifier of the product that the image belongs to.</param>
    /// <param name="imageId">The unique identifier of the image to be deleted.</param>
    /// <returns>A task representing the asynchronous operation that removes the specified product image.</returns>
    [HttpDelete("{productId}/images/{imageId}")]
    public async Task<IActionResult> DeleteAsync(int productId, int imageId)
    {
        await _productManager.DeleteImageProductAsync(productId, imageId);
        return Ok();
    }

    /// <summary>
    /// Updates an existing product image for a specified product.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to which the image belongs.</param>
    /// <param name="imageId">The unique identifier of the image to be updated.</param>
    /// <param name="productImageDto">The updated details of the product image.</param>
    /// <returns>A task representing an asynchronous operation that returns an IActionResult indicating the result of the operation.</returns>
    [HttpPut("{productId}/images/{imageId}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> PutAsync(int productId, int imageId,
        [FromBody] ImageProductDto productImageDto)
    {
        var affectedRows = await _productManager.UpdateImageProductAsync(productId, imageId, productImageDto);

        if (affectedRows > IntPtr.Zero)
        {
            return Ok(affectedRows);
        }

        return Accepted();
    }
}