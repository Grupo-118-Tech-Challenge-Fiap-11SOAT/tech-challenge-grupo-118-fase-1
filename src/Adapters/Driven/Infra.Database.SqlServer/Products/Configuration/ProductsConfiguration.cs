using Domain.Products.ValueObjects;
using Infra.Database.SqlServer.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.SqlServer.Products.Configuration;

public class ProductsConfiguration : IEntityTypeConfiguration<Product>
{
    private const string DEFAULT_DATETIMEOFFSET_COLUMN_TYPE = "datetimeoffset";
    private const string DEFAULT_DATETIMEOFFSET_COLUMN_DEFAULT_VALUE = "SYSDATETIMEOFFSET()";

    private readonly DateTimeOffset DefaultDateTimeOffset = new DateTimeOffset(2025, 5, 26, 0, 0, 0, TimeSpan.Zero);

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

        builder.HasData(GenerateProductData());
    }


    private IEnumerable<Product> GenerateProductData()
    {
        var listToSeed = new List<Product>();

        listToSeed.AddRange(GenerateSnackData());
        listToSeed.AddRange(GenerateAccompanimentData());
        listToSeed.AddRange(GenerateDrinkData());
        listToSeed.AddRange(GenerateDessertData());

        return listToSeed;
    }

    private IEnumerable<Product> GenerateSnackData()
    {
        return new List<Product>
        {
            new()
            {
                Id = 1,
                Name = "Hamburguer 2 carnes",
                Description = "Hamburguer de 2 carnes com queijo e presunto. Acompanha de frango e bacon.",
                Category = ProductType.Snack,
                Price = 35.95M,
                IsActive = true,
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset
            },
            new()
            {
                Id = 2,
                Name = "Hamburguer 4 carnes",
                Description =
                    "Hamburguer de 4 carnes com queijo, presunto e molho da casa. Acompanha de frango e bacon.",
                Category = ProductType.Snack,
                Price = 50.95M,
                IsActive = true,
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset
            }
        };
    }

    private IEnumerable<Product> GenerateAccompanimentData()
    {
        return new List<Product>
        {
            new()
            {
                Id = 3,
                Name = "Salada Ceasar",
                Description = "Uma salada ceasar bem temperada.",
                Category = ProductType.Accompaniment,
                Price = 15.95M,
                IsActive = true,
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset
            },
            new()
            {
                Id = 4,
                Name = "Onion Rings",
                Description = "Deliciosos onion rings com cebola e tomate.",
                Category = ProductType.Accompaniment,
                Price = 20.95M,
                IsActive = true,
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset
            }
        };
    }

    private IEnumerable<Product> GenerateDrinkData()
    {
        return new List<Product>
        {
            new()
            {
                Id = 5,
                Name = "Coca-Cola Lata",
                Description = "Coca-Cola Lata de 350ML para matar a sua sede!",
                Category = ProductType.Drink,
                Price = 4.50M,
                IsActive = true,
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset
            },
            new()
            {
                Id = 6,
                Name = "Heineken Longneck",
                Description = "Heineken Longneck bem gelada.",
                Category = ProductType.Drink,
                Price = 8.50M,
                IsActive = true,
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset
            }
        };
    }

    private IEnumerable<Product> GenerateDessertData()
    {
        return new List<Product>
        {
            new()
            {
                Id = 7,
                Name = "Sorvete de casquinha - chocolate",
                Description = "Uma deliciosa casquinha de chocolate com sorvete de leite.",
                Category = ProductType.Dessert,
                Price = 3.34M,
                IsActive = true,
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset
            },
            new()
            {
                Id = 8,
                Name = "Milkshake de morango e chocolate",
                Description = "Um incrível milkshake de morango com leite e chocolate.",
                Category = ProductType.Dessert,
                Price = 100,
                IsActive = true,
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset
            }
        };
    }
}