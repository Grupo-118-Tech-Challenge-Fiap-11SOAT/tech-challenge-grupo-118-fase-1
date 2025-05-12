using Domain.Products.Dtos;

namespace Domain.Products.Ports.In;

public interface IProductManager
{
    Task<List<ProductDto>> GetProductsAsync(int skip = 0, int take = 10);
    
    Task<object> GetProductByIdAsync(int id);
}