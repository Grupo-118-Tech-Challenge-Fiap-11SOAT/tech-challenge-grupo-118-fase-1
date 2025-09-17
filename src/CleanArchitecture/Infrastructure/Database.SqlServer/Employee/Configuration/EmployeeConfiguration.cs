using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Employee.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Common.Dto.Employee.Database.Employee>
{
    public void Configure(EntityTypeBuilder<Common.Dto.Employee.Database.Employee> builder)
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
            .HasDefaultValueSql("SYSDATETIMEOFFSET()")
            .ValueGeneratedOnUpdate();

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

        builder.HasData(Generate());
    }

    private IEnumerable<Common.Dto.Employee.Database.Employee> Generate()
    {
        return new List<Common.Dto.Employee.Database.Employee>
        {
            new(
                "98659502000",
                "Admin",
                "Doe",
                "admin@admin.com",
                new DateOnly(1990, 1, 1),
                "QBYnGddxOZ/VOBgUr1koYDLMawbe/D8NaYYxOXQ0LHN8TO/ysQ5UvBZc70kbQkfXarxn+KobEuH7KpXkiElivg==",
                Roles.Admin,
                true,
                1)
        };
    }
}