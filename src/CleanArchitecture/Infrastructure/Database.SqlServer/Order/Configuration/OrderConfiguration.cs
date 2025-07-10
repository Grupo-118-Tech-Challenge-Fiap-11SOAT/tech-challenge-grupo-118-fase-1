using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Order.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Common.Dto.Order.Database.Order>
{
    public void Configure(EntityTypeBuilder<Common.Dto.Order.Database.Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderNumber)
            .IsRequired();

        builder.Property(o => o.Cpf)
            .HasMaxLength(11);

        builder.Property(o => o.Total)
            .IsRequired();

        builder.Property(o => o.Status)
            .IsRequired();

        builder.Property(o => o.CreatedAt)
            .IsRequired()
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()")
            .ValueGeneratedOnAdd();

        builder.Property(o => o.UpdatedAt)
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.Ignore(o => o.IsActive);

        // Relationship (optional, but recommended for navigation)
        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(o=> o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Common.Dto.Payments.Database.Payment>(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}