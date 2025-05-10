using System.Runtime.InteropServices.ComTypes;
using Domain.Products.Ports.In;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TechChallengeFastFood.API.Controllers;

/// <summary>
/// Controller to Manage Products
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
    [HttpGet]
    public async Task<IActionResult> GetAsync(int skip = 0, int take = 0)
    {
        return Ok(await _productManager.GetProductsAsync(skip, take));
    }

    /// <summary>
    /// Get a detailed product
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
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
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] object productDto)
    {
        return Created();
    }

    /// <summary>
    /// Update a Product
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="productDto"></param>
    /// <returns></returns>
    [HttpPut("{productId}")]
    public async Task<IActionResult> PutAsync(int productId, [FromBody] object productDto)
    {
        return Ok();
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