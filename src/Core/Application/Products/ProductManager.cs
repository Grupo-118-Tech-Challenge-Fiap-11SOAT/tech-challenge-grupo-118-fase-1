using Application.Products.Exceptions;
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
        var products = await _productRepository.GetProductsAsync(skip, take, searchActiveProducts,
            cancellationToken: cancellationToken);

        if (products is null || products.Count == 0)
            return null;

        var productDto = products.ConvertAll(p => new ProductDto(p));

        return productDto;
    }

    public async Task<List<ProductDto>?> GetActiveProductsByIds(int[] ids,
        CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetProductsByIds(ids, cancellationToken);

        if (products is null || products.Count == 0)
            return null;

        if (products.Any(p => !p.IsActive))
            throw new DeactivatedProductException();

        return products.ConvertAll(p => new ProductDto(p));
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id, bool includeImages = false, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        var product =
            await _productRepository.GetProductByIdAsync(id, includeImages, skip, take,
                cancellationToken: cancellationToken);

        if (product is null)
            return null;

        var productDto = new ProductDto(product);
        return productDto;
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto productDto,
        CancellationToken cancellationToken = default)
    {
        var product = new Product(productDto.Name,
            productDto.Description,
            productDto.Category,
            productDto.Price,
            productDto.IsActive);

        var createdProduct = await _productRepository.CreateProductAsync(product, cancellationToken: cancellationToken);
        return new ProductDto(createdProduct);
    }

    public async Task<ProductDto?> UpdateProductAsync(int productId, ProductDto productDto,
        CancellationToken cancellationToken = default)
    {
        var product = new Product(productDto.Name,
            productDto.Description,
            productDto.Category,
            productDto.Price,
            productDto.IsActive);

        var updatedProduct =
            await _productRepository.UpdateProductAsync(productId, product, cancellationToken: cancellationToken);

        if (updatedProduct is null)
            return null;

        return new ProductDto(updatedProduct);
    }

    #endregion

    #region Image Product Methods

    public async Task<List<ImageProductDto>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        var product =
            await _productRepository.GetProductByIdAsync(productId, true, skip, take,
                cancellationToken: cancellationToken);

        if (product is null && product?.Images?.Count == 0)
            return null;

        var productImagesDto = product?.Images?.ConvertAll(pi => new ImageProductDto(pi));
        return productImagesDto;
    }

    public async Task<ImageProductDto?> CreateImageProductAsync(int productId, ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        var product =
            await _productRepository.GetProductByIdAsync(productId, true, cancellationToken: cancellationToken);

        if (product is null)
            return null;

        var imageProduct = new ImageProduct(product.Id, imageProductDto.Position, imageProductDto.Url);

        product.AddImage(imageProduct);

        var createdImageProduct =
            await _productRepository.CreateImageProductAsync(imageProduct, cancellationToken: cancellationToken);
        return new ImageProductDto(createdImageProduct);
    }

    public async Task<int> DeleteImageProductAsync(int productId, int imageProductId,
        CancellationToken cancellationToken = default)
    {
        return await _productRepository.DeleteImageProductAsync(productId, imageProductId,
            cancellationToken: cancellationToken);
    }

    public async Task<ImageProductDto?> UpdateImageProductAsync(int productId, int imageId,
        ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        var product =
            await _productRepository.GetProductByIdAsync(productId, true, cancellationToken: cancellationToken);

        if (product is null)
            return null;

        var imageProduct =
            new ImageProduct(productId, imageProductDto.Position, imageProductDto.Url, imageId);

        product.ChangeImage(imageProduct);

        var updatedImageProduct = await _productRepository.UpdateImageProductAsync(productId, imageId, imageProduct,
            cancellationToken: cancellationToken);

        if (updatedImageProduct is null)
            return null;

        return new ImageProductDto(updatedImageProduct);
    }

    public async Task<ImageProductDto?> GetProductImageByIdAsync(int productId, int imageId,
        CancellationToken cancellationToken = default)
    {
        var imageProduct = await _productRepository.GetImageProductByIdAsync(productId, imageId, cancellationToken);

        if (imageProduct is null)
            return null;

        var imageProductDto = new ImageProductDto(imageProduct);
        return imageProductDto;
    }

    #endregion
}