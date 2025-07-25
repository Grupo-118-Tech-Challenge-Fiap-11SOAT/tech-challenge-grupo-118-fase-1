using Common.Dto.Products.Database;
using Common.Enums;
using Common.Interfaces.Products.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Products.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CleanArchDbContext _dbContext;

    public ProductRepository(CleanArchDbContext context)
    {
        _dbContext = context;
    }

    public static IProductRepository Create(CleanArchDbContext context)
    {
        return new ProductRepository(context);
    }

    public async Task<List<Product>?> GetProductsAsync(int skip = 0, int take = 10,
        bool searchActiveProducts = false,
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

        return productsEntities;
    }

    public async Task<List<Product>?> GetProductsByTypeAsync(ProductType type, int skip = 0, int take = 10,
        CancellationToken cancellationToken = default)
    {
        var productsEntities = await _dbContext.Products
            .Where(p => p.Category == type)
            .OrderBy(p => p.Id)
            .Skip(skip)
            .Take(take)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (productsEntities.Count == 0)
            return null;

        return productsEntities;
    }

    public async Task<List<Product>?> GetProductsByIdsAsync(int[] ids,
        CancellationToken cancellationToken = default)
    {
        var productsEntities = await _dbContext.Products
            .Where(p => ids.Contains(p.Id))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (productsEntities.Count == 0)
            return null;

        return productsEntities;
    }

    public async Task<Product?> GetProductByIdAsync(int id, bool includeImages = false, int skip = 0,
        int take = 10,
        CancellationToken cancellationToken = default)
    {
        var productQuery = _dbContext.Products.AsQueryable();

        if (includeImages)
            productQuery = productQuery.Include(p => p.Images.Skip(skip).Take(take));

        var productEntity = await productQuery.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (productEntity is null)
            return null;

        return productEntity;
    }

    public async Task<Product> CreateProductAsync(Product product,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<Product?> UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        _dbContext.Update(product).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return product;
    }
}