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

    public ProductsController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    /// <summary>
    /// List Products
    /// </summary>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet]
    public async Task<IActionResult> GetAsync(int skip = 0, int take = 10)
    {
        return Ok(await _productManager.GetProductsAsync(skip, take));
    }

    /// <summary>
    /// Get a detailed product
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetAsync(int productId)
    {
        return Ok(await _productManager.GetProductByIdAsync(productId));
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
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
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Update a Product
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="productDto"></param>
    /// <returns></returns>
    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> PutAsync(int productId, [FromBody] ProductDto productDto)
    {
        var affectedRows = await _productManager.UpdateProductAsync(productId, productDto);

        if (affectedRows > IntPtr.Zero)
            return Ok(affectedRows);

        return NoContent();
    }

    /// <summary>
    /// List the product images from a product
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpGet("{productId}/images")]
    public async Task<IActionResult> GetProductImagesAsync(int productId)
    {
        return Ok("");
    }

    /// <summary>
    /// Create a new Product Image
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="productImageDto"></param>
    /// <returns></returns>
    [HttpPost("{productId}/images")]
    public async Task<IActionResult> PostProductImageAsync(int productId, [FromBody] object productImageDto)
    {
        return Created();
    }
}