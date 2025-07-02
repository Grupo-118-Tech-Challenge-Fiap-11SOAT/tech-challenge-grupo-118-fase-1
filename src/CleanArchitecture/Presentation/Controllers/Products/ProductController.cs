using Common.Dto.Products;
using Common.Interfaces.Products.Controller;
using Common.Interfaces.Products.Gateway;
using Common.Interfaces.Products.Presenter;
using TechChallengeFastFood.CleanArch.Application.UseCases.Products;

namespace TechChallengeFastFood.CleanArch.Presentation.Controllers.Products;

public class ProductController : IProductController
{
    private readonly GetProductsUseCase _getProductsUseCase;
    private readonly CreateProductUseCase _createProductUseCase;

    private readonly IProductPresenter _productPresenter;

    public ProductController(IProductGateway productGateway, IProductPresenter productPresenter)
    {
        _getProductsUseCase = GetProductsUseCase.Create(productGateway);
        _createProductUseCase = CreateProductUseCase.Create(productGateway);

        _productPresenter = productPresenter;
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
        throw new NotImplementedException();
    }

    public async Task<List<ProductDto>?> GetActiveProductsByIdsAsync(int[] ids,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id, bool includeImages = false, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}