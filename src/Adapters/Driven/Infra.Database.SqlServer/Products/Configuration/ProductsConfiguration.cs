using Infra.Database.SqlServer.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.SqlServer.Products.Configuration;

public class ProductsConfiguration : IEntityTypeConfiguration<Product>
{
    private const string DEFAULT_DATETIMEOFFSET_COLUMN_TYPE = "datetimeoffset";
    private const string DEFAULT_DATETIMEOFFSET_COLUMN_DEFAULT_VALUE = "SYSDATETIMEOFFSET()";
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Category)
            .IsRequired();

        builder.Property(p => p.Price)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnType(DEFAULT_DATETIMEOFFSET_COLUMN_TYPE)
            .HasDefaultValueSql(DEFAULT_DATETIMEOFFSET_COLUMN_DEFAULT_VALUE)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.UpdatedAt)
            .HasColumnType(nameof(DateTimeOffset).ToLower())
            .HasDefaultValueSql(DEFAULT_DATETIMEOFFSET_COLUMN_DEFAULT_VALUE);
    }
}