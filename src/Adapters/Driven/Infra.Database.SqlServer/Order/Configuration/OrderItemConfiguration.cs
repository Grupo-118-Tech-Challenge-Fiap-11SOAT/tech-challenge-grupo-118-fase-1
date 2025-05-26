

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.SqlServer.Order.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<Domain.Order.Entities.OrderItem>
{
    public void Configure(EntityTypeBuilder<Domain.Order.Entities.OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(x => new { x.OrderId, x.ProductId });

        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

    }
}