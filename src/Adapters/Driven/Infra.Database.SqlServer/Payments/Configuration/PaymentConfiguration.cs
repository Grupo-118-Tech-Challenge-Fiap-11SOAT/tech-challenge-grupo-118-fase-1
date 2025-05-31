using Domain.Payments.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.SqlServer.Payments.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .UseIdentityColumn();

        builder.Property(p => p.Uuid)
            .IsRequired();
        builder.HasIndex(p => p.Uuid)
            .IsUnique();

        //TODO: OrderId - Foreign Key
        builder.Property(p => p.OrderId)
            .IsRequired();

        builder.Property(p => p.Provider)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(p => p.Value)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.ExternalId)
            .HasMaxLength(100);

        builder.Property(p => p.UserPaymentCode)
            .HasMaxLength(500);
    }
}
