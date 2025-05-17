using Domain.Products.Entities;
using Domain.Products.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.SqlServer.Products.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Products

    public async Task<List<Product>> GetProductsAsync(int skip = 0, int take = 10, bool searchActiveProducts = false)
    {
        var query = _dbContext.Products.AsQueryable();

        if (searchActiveProducts)
            query = query.Where(p => p.IsActive);

        var productsEntities = await query.OrderBy(p => p.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        var products = productsEntities.ConvertAll(p => Product.Load(
            p.Id,
            p.Name,
            p.Description,
            p.Category,
            p.Price,
            p.IsActive
        ));

        return products;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var productEntity = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (productEntity is null)
            return null;

        var product = Product.Load(
            productEntity.Id,
            productEntity.Name,
            productEntity.Description,
            productEntity.Category,
            productEntity.Price,
            productEntity.IsActive
        );

        return product;
    }

    public async Task<int> CreateProductAsync(Product product)
    {
        var productEntity = new Entities.Product
        {
            Category = product.Category,
            Description = product.Description,
            IsActive = product.IsActive,
            Name = product.Name,
            Price = product.Price,
        };

        _dbContext.Products.Add(productEntity);
        await _dbContext.SaveChangesAsync();

        return productEntity.Id;
    }

    public async Task<int> UpdateProductAsync(int productId, Product product)
    {
        var affectedRows = await _dbContext.Products
            .Where(p => p.Id == productId)
            .ExecuteUpdateAsync(p => p
                .SetProperty(pp => pp.Name, product.Name)
                .SetProperty(pp => pp.Description, product.Description)
                .SetProperty(pp => pp.Category, product.Category)
                .SetProperty(pp => pp.Price, product.Price)
                .SetProperty(pp => pp.IsActive, product.IsActive)
                .SetProperty(pp => pp.UpdatedAt, DateTimeOffset.UtcNow));

        return affectedRows;
    }

    #endregion

    #region Image Products

    public async Task<List<ImageProduct>> GetProductImagesAsync(int productId, int skip = 0, int take = 10)
    {
        var imageEntities = await _dbContext.ImageProducts
            .Where(ip => ip.ProductId == productId)
            .OrderBy(ip => ip.Position)
            .ThenBy(ip => ip.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        var imageProducts = imageEntities.ConvertAll(ip => ImageProduct.Load(ip.Id, ip.Url, ip.ProductId, ip.Position));

        return imageProducts;
    }

    public async Task<int> CreateImageProductAsync(ImageProduct imageProduct)
    {
        var imageProductEntity = new Entities.ImageProduct
        {
            ProductId = imageProduct.ProductId,
            Position = imageProduct.Position,
            Url = imageProduct.Url
        };

        _dbContext.ImageProducts.Add(imageProductEntity);
        await _dbContext.SaveChangesAsync();

        return imageProductEntity.Id;
    }

    public async Task<int> DeleteImageProductAsync(int productId, int imageId)
    {
        var affectedRows = await _dbContext
            .ImageProducts
            .Where(ip => ip.ProductId == productId
                         && ip.Id == imageId)
            .ExecuteDeleteAsync();

        return affectedRows;
    }

    public async Task<int> UpdateImageProductAsync(int productId, int imageId, ImageProduct imageProduct)
    {
        var affectedRows = await _dbContext.ImageProducts
            .Where(ip => ip.Id == imageId && ip.ProductId == productId)
            .ExecuteUpdateAsync(ip => ip
                .SetProperty(i => i.Position, imageProduct.Position)
                .SetProperty(i => i.Url, imageProduct.Url)
                .SetProperty(i => i.UpdatedAt, DateTimeOffset.UtcNow));

        return affectedRows;
    }

    #endregion
}