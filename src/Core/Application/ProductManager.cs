using Domain.Products.Dtos;
using Domain.Products.Entities;
using Domain.Products.Ports.In;
using Domain.Products.Ports.Out;

namespace Application;

public class ProductManager : IProductManager
{
    private readonly IProductRepository _productRepository;

    public ProductManager(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductDto>> GetProductsAsync(int skip = 0, int take = 10)
    {
        var products = await _productRepository.GetProductsAsync(skip, take);

        if (products is null || products.Count == 0)
            return null;

        var productDto = products.ConvertAll(p => new ProductDto(p));

        return productDto;
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        if (product is null)
            return null;

        var productDto = new ProductDto(product);
        return productDto;
    }

    public async Task<int> CreateProductAsync(ProductDto productDto)
    {
        var product = new Product(productDto);

        var productId = await _productRepository.CreateProductAsync(product);
        return productId;
    }

    public async Task<int> UpdateProductAsync(int productId, ProductDto productDto)
    {
        var product = new Product(productDto);

        var affectedRows = await _productRepository.UpdateProductAsync(productId, product);
        return affectedRows;
    }
}