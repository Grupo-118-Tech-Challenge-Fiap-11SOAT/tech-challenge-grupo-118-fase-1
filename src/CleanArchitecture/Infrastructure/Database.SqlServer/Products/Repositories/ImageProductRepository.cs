using Common.Dto.Products.Database;
using Common.Interfaces.Products.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Products.Repositories;

public class ImageProductRepository : IImageProductRepository
{
    private readonly CleanArchDbContext _context;

    public ImageProductRepository(CleanArchDbContext context)
    {
        _context = context;
    }

    public async Task<ImageProduct> CreateImageProductAsync(ImageProduct imageProduct,
        CancellationToken cancellationToken = default)
    {
        await _context.ImageProducts.AddAsync(imageProduct, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return imageProduct;
    }

    public async Task<int> DeleteImageProductAsync(int productId, int imageId,
        CancellationToken cancellationToken = default)
    {
        var imageProductEntity =
            await _context.ImageProducts.FirstOrDefaultAsync(ip => ip.Id == imageId && ip.ProductId == productId,
                cancellationToken);

        if (imageProductEntity is null)
            return 0;

        _context.ImageProducts.Remove(imageProductEntity);
        var affectedRows = await _context.SaveChangesAsync(cancellationToken);

        return affectedRows;
    }

    public async Task<ImageProduct?> UpdateImageProductAsync(int productId, int imageId, ImageProduct imageProduct,
        CancellationToken cancellationToken = default)
    {
        var imageProductEntity =
            await _context.ImageProducts.FirstOrDefaultAsync(ip => ip.Id == imageId && ip.ProductId == productId,
                cancellationToken);

        if (imageProductEntity is null)
            return null;

        imageProductEntity.UpdateImageProduct(imageProduct);

        await _context.SaveChangesAsync(cancellationToken);

        return imageProductEntity;
    }

    public async Task<ImageProduct?> GetImageProductByIdAsync(int productId, int imageId,
        CancellationToken cancellationToken = default)
    {
        var imageProductEntity =
            await _context.ImageProducts
                .AsNoTracking()
                .FirstOrDefaultAsync(ip => ip.Id == imageId && ip.ProductId == productId, cancellationToken);

        if (imageProductEntity is null)
            return null;

        return imageProductEntity;
    }

    public async Task<List<ImageProduct>?> GetProductImagesAsync(int productId, int skip = 0, int take = 10, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}