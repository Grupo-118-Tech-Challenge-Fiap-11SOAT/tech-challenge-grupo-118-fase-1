using Infra.Database.SqlServer.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.SqlServer.Products.Configuration;

public class ImageProductsConfiguration : IEntityTypeConfiguration<ImageProduct>
{
    public void Configure(EntityTypeBuilder<ImageProduct> builder)
    {
        builder.HasOne(i => i.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.ProductId);
    }
}