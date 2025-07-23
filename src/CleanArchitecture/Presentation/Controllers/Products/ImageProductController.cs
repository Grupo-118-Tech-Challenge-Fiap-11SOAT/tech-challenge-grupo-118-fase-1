using Common.Dto.Products;
using Common.Interfaces.Products.Controller;
using Common.Interfaces.Products.Gateway;
using Common.Interfaces.Products.Presenter;
using Common.Interfaces.Products.Repositories;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products.ImageProduct;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Products;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Products;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Products;

public class ImageProductController : IImageProductController
{
    private readonly GetProductImagesUseCase _getProductImageUseCase;
    private readonly GetProductImageByIdUseCase _getProductImageByIdUseCase;
    private readonly CreateImageProductUseCase _createImageProductUseCase;
    private readonly UpdateImageProductUseCase _updateImageProductUseCase;
    private readonly DeleteImageProductUseCase _deleteImageProductUseCase;

    private readonly GetProductByIdUseCase _getProductByIdUseCase;

    private readonly IImageProductPresenter _imageProductPresenter;

    public ImageProductController(IImageProductRepository imageProductRepository, IProductRepository productRepository)
    {
        IImageProductGateway imageProductGateway =
            ImageProductGateway.Create(imageProductRepository, productRepository);

        IProductGateway productGateway = ProductGateway.Create(productRepository);

        _getProductImageUseCase = GetProductImagesUseCase.Create(imageProductGateway);
        _getProductImageByIdUseCase = GetProductImageByIdUseCase.Create(imageProductGateway);
        _createImageProductUseCase = CreateImageProductUseCase.Create(imageProductGateway);
        _updateImageProductUseCase = UpdateImageProductUseCase.Create(imageProductGateway);
        _deleteImageProductUseCase = DeleteImageProductUseCase.Create(imageProductGateway);

        _getProductByIdUseCase = GetProductByIdUseCase.Create(productGateway);

        _imageProductPresenter = ImageProductPresenter.Create();
    }

    public async Task<List<ImageProductDto>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        var imageProducts = await _getProductImageUseCase.ExecuteAsync(productId, skip, take, cancellationToken);

        if (imageProducts is null)
            return null;

        return _imageProductPresenter.Convert(imageProducts);
    }

    public async Task<ImageProductDto?> GetProductImageByIdAsync(int productId, int imageId,
        CancellationToken cancellationToken = default)
    {
        var imageProduct = await _getProductImageByIdUseCase.ExecuteAsync(productId, imageId, cancellationToken);

        if (imageProduct is null)
            return null;

        return _imageProductPresenter.Convert(imageProduct);
    }

    public async Task<ImageProductDto?> CreateImageProductAsync(int productId, ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        var product = await _getProductByIdUseCase.ExecuteAsync(productId, true, cancellationToken);

        if (product is null)
            return null;

        var imageProduct = await _createImageProductUseCase.ExecuteAsync(product, imageProductDto, cancellationToken);

        if (imageProduct is null)
            return null;

        return _imageProductPresenter.Convert(imageProduct);
    }

    public async Task<ImageProductDto?> UpdateImageProductAsync(int productId, int imageId,
        ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        var product = await _getProductByIdUseCase.ExecuteAsync(productId, true, cancellationToken);

        if (product is null)
            return null;

        var imageProduct =
            await _updateImageProductUseCase.ExecuteAsync(product, imageId, imageProductDto, cancellationToken);

        if (imageProduct is null)
            return null;

        return _imageProductPresenter.Convert(imageProduct);
    }

    public async Task<int> DeleteImageProductAsync(int productId, int imageId,
        CancellationToken cancellationToken = default)
    {
        return await _deleteImageProductUseCase.ExecuteAsync(productId, imageId, cancellationToken);
    }
}