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

    public async Task<List<Product>?> GetProductsAsync(int skip = 0, int take = 10, bool searchActiveProducts = false)
    {
        var query = _dbContext.Products.AsQueryable();

        if (searchActiveProducts)
            query = query.Where(p => p.IsActive);

        var productsEntities = await query.OrderBy(p => p.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        if (productsEntities.Count == 0)
            return null;

        var products = productsEntities.ConvertAll(p => p.ToDomain());

        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        var productEntity = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (productEntity is null)
            return null;

        var product = productEntity.ToDomain();

        return product;
    }

    public async Task<int> CreateProductAsync(Product product)
    {
        var productEntity = new Entities.Product();

        productEntity.DomainToEntity(product);

        _dbContext.Products.Add(productEntity);
        await _dbContext.SaveChangesAsync();

        return productEntity.Id;
    }

    public async Task<int> UpdateProductAsync(int productId, Product product)
    {
        var productEntity = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (productEntity is null)
            return 0;

        productEntity.DomainToEntity(product);

        var affectedRows = await _dbContext.SaveChangesAsync();

        return affectedRows;
    }

    #endregion

    #region Image Products

    public async Task<List<ImageProduct>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10)
    {
        var imageEntities = await _dbContext.ImageProducts
            .Where(ip => ip.ProductId == productId)
            .OrderBy(ip => ip.Position)
            .ThenBy(ip => ip.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        var imageProducts = imageEntities.ConvertAll(ip => ip.ToDomain());

        return imageProducts;
    }

    public async Task<int> CreateImageProductAsync(ImageProduct imageProduct)
    {
        var imageProductEntity = new Entities.ImageProduct();
        
        imageProductEntity.DomainToEntity(imageProduct);

        _dbContext.ImageProducts.Add(imageProductEntity);
        await _dbContext.SaveChangesAsync();

        return imageProductEntity.Id;
    }

    public async Task<int> DeleteImageProductAsync(int productId, int imageId)
    {
        var imageProductEntity =
            await _dbContext.ImageProducts.FirstOrDefaultAsync(ip => ip.Id == imageId && ip.ProductId == productId);

        if (imageProductEntity is null)
            return 0;

        _dbContext.ImageProducts.Remove(imageProductEntity);
        var affectedRows = await _dbContext.SaveChangesAsync();

        return affectedRows;
    }

    public async Task<int> UpdateImageProductAsync(int productId, int imageId, ImageProduct imageProduct)
    {
        var imageProductEntity =
            await _dbContext.ImageProducts.FirstOrDefaultAsync(ip => ip.Id == imageId && ip.ProductId == productId);

        if (imageProductEntity is null)
            return 0;

        imageProductEntity.Position = imageProduct.Position;
        imageProductEntity.Url = imageProduct.Url;
        imageProductEntity.UpdatedAt = DateTimeOffset.UtcNow;

        var affectedRows = await _dbContext.SaveChangesAsync();

        return affectedRows;
    }

    #endregion
}