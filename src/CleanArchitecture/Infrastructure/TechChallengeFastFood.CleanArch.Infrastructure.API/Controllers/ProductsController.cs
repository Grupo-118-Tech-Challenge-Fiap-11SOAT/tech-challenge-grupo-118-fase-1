using Common.Dto.Products;
using Common.Interfaces.Products.Controller;
using Common.Interfaces.Products.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallengeFastFood.CleanArch.Presentation.Controllers.Products;

namespace TechChallengeFastFood.CleanArch.API.Controllers;

/// <summary>
/// Endpoint to Manage Products
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductController _productController;
    private readonly IImageProductController _imageProductController;

    private readonly ProblemDetails PRODUCT_NOT_FOUND = new ProblemDetails
    {
        Title = "Product not found",
        Status = StatusCodes.Status404NotFound,
        Detail = "The specified product was not found."
    };

    private readonly ProblemDetails IMAGE_PRODUCT_NOT_FOUND = new ProblemDetails
    {
        Title = "Image Product not found",
        Status = StatusCodes.Status404NotFound,
        Detail = "The specified image product was not found."
    };

    /// <summary>
    /// Product constructor
    /// </summary>
    /// <param name="productRepository"></param>
    /// <param name="imageProductRepository"></param>
    public ProductsController(IProductRepository productRepository, IImageProductRepository imageProductRepository)
    {
        _productController = ProductController.Create(productRepository);
        _imageProductController = ImageProductController.Create(imageProductRepository, productRepository);
    }

    #region Products Methods

    /// <summary>
    /// Retrieves a paginated list of products.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="skip">The number of products to skip from the start of the list.</param>
    /// <param name="take">The maximum number of products to retrieve.</param>
    /// <param name="searchActiveProducts">A flag indicating whether to include only active products in the result.</param>
    /// <returns>A list of products if available, or a no-content response if no products are found.</returns>
    [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken, int skip = 0, int take = 10,
        bool searchActiveProducts = false)
    {
        var products = await _productController.GetProductsAsync(skip,
            take,
            searchActiveProducts,
            cancellationToken);

        if (products is null || products.Count == 0)
        {
            return NotFound(PRODUCT_NOT_FOUND);
        }

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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> GetByTypeAsync(CancellationToken cancellationToken, string category, int skip = 0,
        int take = 10)
    {
        var products = await _productController.GetProductsByTypeAsync(category, skip, take, cancellationToken);

        if (products is null || products.Count == 0)
            return NotFound(PRODUCT_NOT_FOUND);

        return Ok(products);
    }


    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The product details if found, or a no-content response if not found.</returns>
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpGet("{productId}"), ActionName("GetDetailedProduct")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAsync(int productId, CancellationToken cancellationToken)
    {
        var products = await _productController.GetProductByIdAsync(productId, cancellationToken);

        if (products is null)
            return NotFound(PRODUCT_NOT_FOUND);

        return Ok(products);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="productDto">The product data transfer object containing information about the product to create.</param>
    /// <returns>The created Product.</returns>
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> PostAsync(CancellationToken cancellationToken, [FromBody] ProductDto productDto)
    {
        var product = await _productController.CreateProductAsync(productDto, cancellationToken);
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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(CancellationToken cancellationToken, int productId,
        [FromBody] ProductDto productDto)
    {
        var product = await _productController.UpdateProductAsync(productId, productDto, cancellationToken);

        if (product is null)
            return Accepted();

        return Ok(product);
    }

    #endregion

    #region Image Products Methods

    /// <summary>
    /// Retrieves a paginated list of images associated with a specific product.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="productId">The unique identifier of the product for which images are being retrieved.</param>
    /// <param name="skip">The number of images to skip from the start of the list.</param>
    /// <param name="take">The maximum number of images to retrieve.</param>
    /// <returns>A list of ImageProductDto objects if found, or a no-content response if no images are available.</returns>
    [HttpGet("{productId}/images")]
    [ProducesResponseType(typeof(List<ImageProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken, int productId, int skip = 0,
        int take = 10)
    {
        var images = await _imageProductController.GetProductImagesAsync(productId, skip, take, cancellationToken);

        if (images is null || images.Count == 0)
            return NotFound(IMAGE_PRODUCT_NOT_FOUND);

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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAsync(int productId, int imageId, CancellationToken cancellationToken)
    {
        var imageProduct =
            await _imageProductController.GetProductImageByIdAsync(productId, imageId, cancellationToken);

        if (imageProduct is null)
            return NotFound(IMAGE_PRODUCT_NOT_FOUND);

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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CancellationToken cancellationToken, int productId,
        [FromBody] ImageProductDto productImageDto)
    {
        var createImageProduct =
            await _imageProductController.CreateImageProductAsync(productId, productImageDto, cancellationToken);

        if (createImageProduct is null)
            return Accepted();

        return CreatedAtAction("GetDetailedImageProduct", new { productId, imageId = createImageProduct.Id },
            createImageProduct);
    }

    /// <summary>
    /// Deletes a product image associated with the provided product ID and image ID.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="productId">The unique identifier of the product that the image belongs to.</param>
    /// <param name="imageId">The unique identifier of the image to be deleted.</param>
    /// <returns>operation status that removes the specified product image.</returns>
    [HttpDelete("{productId}/images/{imageId}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync(CancellationToken cancellationToken, int productId, int imageId)
    {
        var affectedRows =
            await _imageProductController.DeleteImageProductAsync(productId, imageId, cancellationToken);

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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(CancellationToken cancellationToken, int productId, int imageId,
        [FromBody] ImageProductDto productImageDto)
    {
        var updatedImageProduct =
            await _imageProductController.UpdateImageProductAsync(productId, imageId, productImageDto,
                cancellationToken);

        if (updatedImageProduct is null)
            return Accepted();

        return Ok(updatedImageProduct);
    }

    #endregion
}