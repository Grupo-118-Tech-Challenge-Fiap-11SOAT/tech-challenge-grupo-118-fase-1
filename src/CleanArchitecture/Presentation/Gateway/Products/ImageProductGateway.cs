using Common.Interfaces.Products.Gateway;
using Common.Interfaces.Products.Repositories;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;
using ImageProductDomain = TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities.ImageProduct;
using ImageProductEntity = Common.Dto.Products.Database.ImageProduct;


namespace TechChallengeFastFood.CleanArch.Presentation.Gateway.Products;

public class ImageProductGateway : IImageProductGateway
{
    private readonly IImageProductRepository _imageProductRepository;
    private readonly IProductRepository _productRepository;

    public ImageProductGateway(IImageProductRepository imageProductRepository, IProductRepository productRepository)
    {
        _imageProductRepository = imageProductRepository;
        _productRepository = productRepository;
    }

    public async Task<List<ImageProductDomain>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetProductByIdAsync(productId, true, skip, take, cancellationToken);

        if (product is null || product?.Images?.Count == 0)
            return null;

        var imageProductsDto = new List<ImageProductDomain>();

        product?.Images?.ForEach(imageProductEntity =>
        {
            imageProductsDto.Add(new ImageProductDomain(
                imageProductEntity.ProductId,
                imageProductEntity.Position,
                imageProductEntity.Url,
                imageProductEntity.Id));
        });

        return imageProductsDto;
    }

    public async Task<ImageProductDomain?> GetImageByIdAsync(int productId, int id,
        CancellationToken cancellationToken = default)
    {
        var imageProduct = await _imageProductRepository.GetImageProductByIdAsync(productId, id, cancellationToken);

        if (imageProduct is null)
            return null;

        return new ImageProductDomain(
            imageProduct.ProductId,
            imageProduct.Position,
            imageProduct.Url,
            imageProduct.Id);
    }

    public async Task<ImageProduct?> UpdateImageAsync(int productId, int imageId, ImageProduct imageProduct,
        CancellationToken cancellationToken = default)
    {
        var imageProductEntity =
            new ImageProductEntity(productId,
                imageProduct.Position,
                imageProduct.Url,
                imageId);

        await _imageProductRepository.UpdateImageProductAsync(productId, imageId, imageProductEntity,
            cancellationToken);

        return imageProduct;
    }

    public async Task<int> DeleteImageAsync(int productId, int imageId, CancellationToken cancellationToken = default)
    {
        return await _imageProductRepository.DeleteImageProductAsync(productId, imageId, cancellationToken);
    }

    public async Task<ImageProduct?> CreateImageAsync(int productId, ImageProduct imageProduct,
        CancellationToken cancellationToken = default)
    {
        var imageProductEntity =
            new ImageProductEntity(productId,
                imageProduct.Position,
                imageProduct.Url);

        var createdImage = await _imageProductRepository.CreateImageProductAsync(imageProductEntity, cancellationToken);

        imageProduct.Id = createdImage.Id;

        return imageProduct;
    }
}