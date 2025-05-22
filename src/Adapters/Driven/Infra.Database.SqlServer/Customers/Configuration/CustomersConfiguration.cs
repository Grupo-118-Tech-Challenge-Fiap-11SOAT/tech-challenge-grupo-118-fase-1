using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Database.SqlServer.Customers.Configuration
{
    public class CustomersConfiguration : IEntityTypeConfiguration<Domain.Customer.Entities.Customer>
    {
        public void Configure(EntityTypeBuilder<Domain.Customer.Entities.Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(builder => builder.Id);
            builder.Property(builder => builder.Id)
                .UseIdentityColumn();

            builder.Property(builder => builder.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(builder => builder.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(builder => builder.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(builder => builder.BirthDay)
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

            builder.Property(builder => builder.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasIndex(builder => builder.Email)
                .IsUnique()
                .HasDatabaseName("IX_Employees_Email");

            builder.HasIndex(builder => builder.Cpf)
                .IsUnique()
                .HasDatabaseName("IX_Employees_Cpf");
        }
    }
}
