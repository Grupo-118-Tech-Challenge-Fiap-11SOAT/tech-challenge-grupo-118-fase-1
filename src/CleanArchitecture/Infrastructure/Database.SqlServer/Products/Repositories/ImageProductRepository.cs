using Common.Interfaces.Products.Repositories;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Products.Repositories;

public class ImageProductRepository : IImageProductRepository
{
    private readonly CleanArchDbContext _context;

    public ImageProductRepository(CleanArchDbContext context)
    {
        _context = context;
    }

    public async Task<object> CreateImageProductAsync(object imageProduct, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteImageProductAsync(int productId, int imageId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<object?> UpdateImageProductAsync(int productId, int imageId, object imageProduct,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<object?> GetImageProductByIdAsync(int productId, int imageId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}