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

    public async Task<List<Product>> GetProductsAsync(int skip = 0, int take = 10)
    {
        var productsEntities = await _dbContext.Products.Skip(skip).Take(take).ToListAsync();

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
        var productEntity = new Entities.Product
        {
            Id = productId,
            Category = product.Category,
            Description = product.Description,
            IsActive = product.IsActive,
            Name = product.Name,
            Price = product.Price,
        };
        
        _dbContext.Products.Update(productEntity);
        var affectedRows = await _dbContext.SaveChangesAsync();
        
        return affectedRows;
    }
}