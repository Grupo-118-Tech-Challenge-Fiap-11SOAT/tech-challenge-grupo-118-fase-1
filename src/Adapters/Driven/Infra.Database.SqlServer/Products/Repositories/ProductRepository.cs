using Domain.Products.Entities;
using Domain.Products.Ports.Out;

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
        throw new NotImplementedException();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateProductAsync(Product product)
    {
        // throw new NotImplementedException();

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
}