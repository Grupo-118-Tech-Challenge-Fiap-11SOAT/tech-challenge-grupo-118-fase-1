using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.SqlServer.Order.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Domain.Order.Entities.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Order.Entities.Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .UseIdentityColumn();

        builder.Property(builder => builder.OrderNumber)
            .IsRequired();

        builder.Property(builder => builder.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(builder => builder.Total)
            .IsRequired();

        builder.Property(builder => builder.Status)
            .IsRequired();

        builder.Property(builder => builder.CreatedAt)
            .IsRequired()
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()")
            .ValueGeneratedOnAdd();

        builder.Property(builder => builder.UpdatedAt)
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()")
            .ValueGeneratedOnUpdate();

        builder.Ignore(builder => builder.IsActive);
    }
}