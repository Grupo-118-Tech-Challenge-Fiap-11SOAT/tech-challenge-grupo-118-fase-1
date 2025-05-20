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

    public async Task<List<Product>?> GetProductsAsync(int skip = 0, int take = 10, bool searchActiveProducts = false,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Products.AsQueryable();

        if (searchActiveProducts)
            query = query.Where(p => p.IsActive);

        var productsEntities = await query.OrderBy(p => p.Id)
            .Skip(skip)
            .Take(take)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (productsEntities.Count == 0)
            return null;

        var products = productsEntities.ConvertAll(p => p.ToDomain());

        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int id, bool includeImages = false, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        var productQuery = _dbContext.Products.AsQueryable();

        if (includeImages)
            productQuery = productQuery.Include(p => p.Images.Skip(skip).Take(take));

        var productEntity = await productQuery.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (productEntity is null)
            return null;

        var product = productEntity.ToDomain();
        return product;
    }

    public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        var productEntity = new Entities.Product();

        productEntity.DomainToEntity(product);

        _dbContext.Products.Add(productEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return productEntity.ToDomain();
    }

    public async Task<Product?> UpdateProductAsync(int productId, Product product,
        CancellationToken cancellationToken = default)
    {
        var productEntity = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

        if (productEntity is null)
            return null;

        productEntity.DomainToEntity(product);

        var affectedRows = await _dbContext.SaveChangesAsync(cancellationToken);

        return productEntity.ToDomain();
    }

    #endregion

    #region Image Products

    public async Task<ImageProduct> CreateImageProductAsync(ImageProduct imageProduct,
        CancellationToken cancellationToken = default)
    {
        var imageProductEntity = new Entities.ImageProduct();

        imageProductEntity.DomainToEntity(imageProduct);

        _dbContext.ImageProducts.Add(imageProductEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return imageProductEntity.ToDomain();
    }

    public async Task<int> DeleteImageProductAsync(int productId, int imageId,
        CancellationToken cancellationToken = default)
    {
        var imageProductEntity =
            await _dbContext.ImageProducts.FirstOrDefaultAsync(ip => ip.Id == imageId && ip.ProductId == productId,
                cancellationToken);

        if (imageProductEntity is null)
            return 0;

        _dbContext.ImageProducts.Remove(imageProductEntity);
        var affectedRows = await _dbContext.SaveChangesAsync(cancellationToken);

        return affectedRows;
    }

    public async Task<ImageProduct?> UpdateImageProductAsync(int productId, int imageId, ImageProduct imageProduct,
        CancellationToken cancellationToken = default)
    {
        var imageProductEntity =
            await _dbContext.ImageProducts.FirstOrDefaultAsync(ip => ip.Id == imageId && ip.ProductId == productId,
                cancellationToken);

        if (imageProductEntity is null)
            return null;

        imageProductEntity.Position = imageProduct.Position;
        imageProductEntity.Url = imageProduct.Url;
        imageProductEntity.UpdatedAt = DateTimeOffset.UtcNow;

        var affectedRows = await _dbContext.SaveChangesAsync(cancellationToken);

        return imageProductEntity.ToDomain();
    }

    public async Task<ImageProduct?> GetImageProductByIdAsync(int productId, int imageId,
        CancellationToken cancellationToken = default)
    {
        var imageProductEntity =
            await _dbContext.ImageProducts
                .AsNoTracking()
                .FirstOrDefaultAsync(ip => ip.Id == imageId && ip.ProductId == productId, cancellationToken);

        if (imageProductEntity is null)
            return null;
        
        return imageProductEntity.ToDomain();
    }

    #endregion
}