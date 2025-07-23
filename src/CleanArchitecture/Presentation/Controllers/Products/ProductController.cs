using Common.Dto.Products;
using Common.Interfaces.Products.Controller;
using Common.Interfaces.Products.Gateway;
using Common.Interfaces.Products.Presenter;
using Common.Interfaces.Products.Repositories;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Products;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Products;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Products;

public class ProductController : IProductController
{
    private readonly GetProductsUseCase _getProductsUseCase;
    private readonly CreateProductUseCase _createProductUseCase;
    private readonly UpdateProductUseCase _updateProductUseCase;
    private readonly GetProductByTypeUseCase _getProductByTypeUseCase;
    private readonly GetProductByIdUseCase _getProductByIdUseCase;

    private readonly IProductPresenter _productPresenter;

    public ProductController(IProductRepository productRepository)
    {
        IProductGateway productGateway = ProductGateway.Create(productRepository);

        _getProductsUseCase = GetProductsUseCase.Create(productGateway);
        _createProductUseCase = CreateProductUseCase.Create(productGateway);
        _updateProductUseCase = UpdateProductUseCase.Create(productGateway);
        _getProductByTypeUseCase = GetProductByTypeUseCase.Create(productGateway);
        _getProductByIdUseCase = GetProductByIdUseCase.Create(productGateway);

        _productPresenter = ProductPresenter.Create();
    }

    public static IProductController Create(IProductRepository productRepository)
    {
        return new ProductController(productRepository);
    }

    public async Task<List<ProductDto>?> GetProductsAsync(int skip = 0, int take = 10,
        bool searchActiveProducts = false,
        CancellationToken cancellationToken = default)
    {
        var products = await _getProductsUseCase.ExecuteAsync(skip, take, searchActiveProducts, cancellationToken);

        return products is not null
            ? _productPresenter.Convert(products)
            : null;
    }

    public async Task<List<ProductDto>?> GetProductsByTypeAsync(string type, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        var products = await _getProductByTypeUseCase.ExecuteAsync(type, skip, take, cancellationToken);
        return products is not null
            ? _productPresenter.Convert(products)
            : null;
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _getProductByIdUseCase.ExecuteAsync(id, false, cancellationToken);

        return product is not null ? _productPresenter.Convert(product) : null;
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto productDto,
        CancellationToken cancellationToken = default)
    {
        var product = await _createProductUseCase.ExecuteAsync(productDto, cancellationToken);

        return _productPresenter.Convert(product);
    }

    public async Task<ProductDto?> UpdateProductAsync(int productId, ProductDto productDto,
        CancellationToken cancellationToken = default)
    {
        var product = await _updateProductUseCase.ExecuteAsync(productId, productDto, cancellationToken);

        return product is null ? null : _productPresenter.Convert(product);
    }
}