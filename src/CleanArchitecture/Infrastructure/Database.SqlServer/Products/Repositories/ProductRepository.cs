using Common.Dto.Products;
using Common.Interfaces.Products.Repositories;
using Microsoft.EntityFrameworkCore;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Products.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CleanArchDbContext _dbContext;

    public ProductRepository(CleanArchDbContext context)
    {
        _dbContext = context;
    }

    public async Task<List<ProductPersistence>?> GetProductsAsync(int skip = 0, int take = 10,
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

        var productsDto = new List<ProductPersistence>();

        productsEntities.ForEach(productEntity =>
        {
            productsDto.Add(new ProductPersistence
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                Category = productEntity.Category,
                Price = productEntity.Price,
                IsActive = productEntity.IsActive
            });
        });

        return productsDto;
    }

    public async Task<List<ProductPersistence>?> GetProductsByTypeAsync(ProductType type, int skip = 0, int take = 10,
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

        var productsDto = new List<ProductPersistence>();

        productsEntities.ForEach(productEntity =>
        {
            productsDto.Add(new ProductPersistence
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                Category = productEntity.Category,
                Price = productEntity.Price,
                IsActive = productEntity.IsActive
            });
        });

        return productsDto;
    }

    public async Task<List<ProductPersistence>?> GetProductsByIdsAsync(int[] ids,
        CancellationToken cancellationToken = default)
    {
        var productsEntities = await _dbContext.Products
            .Where(p => ids.Contains(p.Id))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (productsEntities.Count == 0)
            return null;

        var productsDto = new List<ProductPersistence>();

        productsEntities.ForEach(productEntity =>
        {
            productsDto.Add(new ProductPersistence
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                Category = productEntity.Category,
                Price = productEntity.Price,
                IsActive = productEntity.IsActive
            });
        });

        return productsDto;
    }

    public async Task<ProductPersistence?> GetProductByIdAsync(int id, bool includeImages = false, int skip = 0,
        int take = 10,
        CancellationToken cancellationToken = default)
    {
        var productQuery = _dbContext.Products.AsQueryable();

        if (includeImages)
            productQuery = productQuery.Include(p => p.Images.Skip(skip).Take(take));

        var productEntity = await productQuery.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (productEntity is null)
            return null;

        var productDto = new ProductPersistence
        {
            Id = productEntity.Id,
            Name = productEntity.Name,
            Description = productEntity.Description,
            Category = productEntity.Category,
            Price = productEntity.Price,
            IsActive = productEntity.IsActive
        };

        return productDto;
    }

    public async Task<ProductPersistence> CreateProductAsync(ProductPersistence product,
        CancellationToken cancellationToken = default)
    {
        var productEntity = new Product(product.Name, product.Description, product.Category, product.Price,
            product.IsActive);

        await _dbContext.Products.AddAsync(productEntity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var productDto = new ProductPersistence
        {
            Id = productEntity.Id,
            Name = productEntity.Name,
            Description = productEntity.Description,
            Category = productEntity.Category,
            Price = productEntity.Price,
            IsActive = productEntity.IsActive
        };

        return productDto;
    }

    public async Task<ProductPersistence?> UpdateProductAsync(int productId, ProductPersistence product,
        CancellationToken cancellationToken = default)
    {
        var productEntity = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

        if (productEntity is null)
            return null;

        productEntity.UpdateProduct(product);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var productDto = new ProductPersistence
        {
            Id = productEntity.Id,
            Name = productEntity.Name,
            Description = productEntity.Description,
            Category = productEntity.Category,
            Price = productEntity.Price,
            IsActive = productEntity.IsActive
        };

        return productDto;
    }
}