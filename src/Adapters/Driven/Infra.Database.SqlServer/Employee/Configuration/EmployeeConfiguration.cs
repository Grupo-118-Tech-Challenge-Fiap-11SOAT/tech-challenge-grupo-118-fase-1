using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.SqlServer.Employee.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Employee.Entities.Employee>
{
    public void Configure(EntityTypeBuilder<Domain.Employee.Entities.Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .UseIdentityColumn();

        builder.Property(builder => builder.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(builder => builder.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(builder => builder.Surname)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(builder => builder.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(builder => builder.BirthDay)
            .IsRequired();

        builder.Property(builder => builder.Password)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(builder => builder.CreatedAt)
            .IsRequired()
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()")
            .ValueGeneratedOnAdd();

        builder.Property(builder => builder.UpdatedAt)
            .HasColumnType("datetimeoffset")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.Property(builder => builder.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(builder => builder.Role)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasIndex(builder => builder.Email)
            .IsUnique()
            .HasDatabaseName("IX_Employees_Email");

        builder.HasIndex(builder => builder.Cpf)
            .IsUnique()
            .HasDatabaseName("IX_Employees_Cpf");
    }
}
