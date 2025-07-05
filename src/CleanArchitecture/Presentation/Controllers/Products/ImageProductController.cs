using Common.Dto.Products;
using Common.Interfaces.Products.Controller;
using Common.Interfaces.Products.Gateway;
using Common.Interfaces.Products.Presenter;
using Common.Interfaces.Products.Repositories;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products.ImageProduct;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Products;

public class ImageProductController : IImageProductController
{
    private readonly GetProductImagesUseCase _getProductImageUseCase;
    private readonly GetProductImageByIdUseCase _getProductImageByIdUseCase;
    private readonly CreateImageProductUseCase _createImageProductUseCase;
    private readonly UpdateImageProductUseCase _updateImageProductUseCase;
    private readonly DeleteImageProductUseCase _deleteImageProductUseCase;

    private readonly IImageProductPresenter _imageProductPresenter;

    public ImageProductController(IImageProductGateway imageProductGateway, IProductGateway productGateway,
        IImageProductPresenter imageProductPresenter)
    {
        _getProductImageUseCase = GetProductImagesUseCase.Create(imageProductGateway);
        _getProductImageByIdUseCase = GetProductImageByIdUseCase.Create(imageProductGateway);
        _createImageProductUseCase = CreateImageProductUseCase.Create(imageProductGateway, productGateway);
        _updateImageProductUseCase = UpdateImageProductUseCase.Create(imageProductGateway, productGateway);
        _deleteImageProductUseCase = DeleteImageProductUseCase.Create(imageProductGateway);

        _imageProductPresenter = imageProductPresenter;
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
        var imageProduct = await _createImageProductUseCase.ExecuteAsync(productId, imageProductDto, cancellationToken);

        if (imageProduct is null)
            return null;

        return _imageProductPresenter.Convert(imageProduct);
    }

    public async Task<ImageProductDto?> UpdateImageProductAsync(int productId, int imageId,
        ImageProductDto imageProductDto,
        CancellationToken cancellationToken = default)
    {
        var imageProduct =
            await _updateImageProductUseCase.ExecuteAsync(productId, imageId, imageProductDto, cancellationToken);

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