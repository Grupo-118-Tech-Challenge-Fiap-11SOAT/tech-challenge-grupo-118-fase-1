using Domain.Products.Entities;

namespace Domain.Products.Ports.Out;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync(int skip = 0, int take = 10);
    
    Task<Product> GetProductByIdAsync(int id);
    
    Task<int> CreateProductAsync(Product product);
    
    Task<int> UpdateProductAsync(int productId, Product product);
}