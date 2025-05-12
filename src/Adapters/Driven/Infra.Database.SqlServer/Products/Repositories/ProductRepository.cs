using Domain.Products.Entities;
using Domain.Products.Ports.Out;

namespace Infra.Database.SqlServer.Products.Repositories;

public class ProductRepository: IProductRepository
{
    public async Task<List<Product>> GetProductsAsync(int skip = 0, int take = 10)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}