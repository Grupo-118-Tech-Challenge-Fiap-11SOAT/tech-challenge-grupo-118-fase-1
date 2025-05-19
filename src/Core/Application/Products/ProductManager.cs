using Domain.Products.Dtos;
using Domain.Products.Entities;
using Domain.Products.Ports.In;
using Domain.Products.Ports.Out;

namespace Application.Products;

public class ProductManager : IProductManager
{
    private readonly IProductRepository _productRepository;

    public ProductManager(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    #region Product Methods

    public async Task<List<ProductDto>?> GetProductsAsync(int skip = 0, int take = 10,
        bool searchActiveProducts = false, CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetProductsAsync(skip, take, searchActiveProducts, cancellationToken: cancellationToken);

        if (products is null || products.Count == 0)
            return null;

        var productDto = products.ConvertAll(p => new ProductDto(p));

        return productDto;
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id, bool includeImages = false, int skip = 0, int take = 10, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetProductByIdAsync(id, includeImages, skip, take, cancellationToken: cancellationToken);

        if (product is null)
            return null;

        var productDto = new ProductDto(product);
        return productDto;
    }

    public async Task<int> CreateProductAsync(ProductDto productDto, CancellationToken cancellationToken = default)
    {
        var product = new Product(productDto.Name,
            productDto.Description,
            productDto.Category,
            productDto.Price,
            productDto.IsActive);

        var productId = await _productRepository.CreateProductAsync(product, cancellationToken: cancellationToken);
        return productId;
    }

    public async Task<int> UpdateProductAsync(int productId, ProductDto productDto, CancellationToken cancellationToken = default)
    {
        var product = new Product(productDto.Name,
            productDto.Description,
            productDto.Category,
            productDto.Price,
            productDto.IsActive);

        var affectedRows = await _productRepository.UpdateProductAsync(productId, product, cancellationToken: cancellationToken);
        return affectedRows;
    }

    #endregion

    #region Image Product Methods

    public async Task<List<ImageProductDto>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetProductByIdAsync(productId, true, skip, take, cancellationToken: cancellationToken);

        if (product is null && product?.Images?.Count == 0)
            return null;

        var productImagesDto = product?.Images?.ConvertAll(pi => new ImageProductDto(pi));
        return productImagesDto;
    }

    public async Task<int> CreateImageProductAsync(int productId, ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetProductByIdAsync(productId, true, cancellationToken: cancellationToken);

        if (product is null)
            return 0;

        var imageProduct = new ImageProduct(product.Id, imageProductDto.Position, imageProductDto.Url);

        product.AddImage(imageProduct);

        return await _productRepository.CreateImageProductAsync(imageProduct, cancellationToken: cancellationToken);
    }

    public async Task<int> DeleteImageProductAsync(int productId, int imageProductId,
        CancellationToken cancellationToken = default)
    {
        return await _productRepository.DeleteImageProductAsync(productId, imageProductId, cancellationToken: cancellationToken);
    }

    public async Task<int> UpdateImageProductAsync(int productId, int imageId, ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetProductByIdAsync(productId, true, cancellationToken: cancellationToken);

        if (product is null)
            return 0;

        var imageProduct = new ImageProduct(productId, imageProductDto.Position, imageProductDto.Url);

        product.ChangeImage(imageProduct);

        var affectedRows = await _productRepository.UpdateImageProductAsync(productId, imageId, imageProduct, cancellationToken: cancellationToken);
        return affectedRows;
    }

    #endregion
}