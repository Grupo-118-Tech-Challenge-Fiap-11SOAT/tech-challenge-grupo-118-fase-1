using Domain.Products.ValueObjects;
using Domain.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.SqlServer.Products.Configuration;

public class ProductsConfiguration : IEntityTypeConfiguration<Product>
{
    private const string DEFAULT_DATETIMEOFFSET_COLUMN_TYPE = "datetimeoffset";
    private const string DEFAULT_DATETIMEOFFSET_COLUMN_DEFAULT_VALUE = "SYSDATETIMEOFFSET()";

    private readonly DateTimeOffset DefaultDateTimeOffset = new DateTimeOffset(2025, 5, 26, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<Domain.Products.Entities.Product> builder)
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
            new Product("Hamburguer 2 carnes",
                "Hamburguer de 2 carnes com queijo e presunto. Acompanha de frango e bacon.",
                ProductType.Snack,
                35.95M,
                true,
                1),

            new Product("Hamburguer 4 carnes",
                "Hamburguer de 4 carnes com queijo, presunto e molho da casa. Acompanha de frango e bacon.",
                ProductType.Snack,
                50.95M,
                true,
                2),
        };
    }

    private IEnumerable<Product> GenerateAccompanimentData()
    {
        return new List<Product>
        {
            new Product("Salada Ceasar",
                "Uma salada ceasar bem temperada.",
                ProductType.Accompaniment,
                15.95M,
                true,
                3),

            new Product("Onion Rings",
                "Deliciosos onion rings com cebola e tomate.",
                ProductType.Accompaniment,
                20.95M,
                true,
                4),
        };
    }

    private IEnumerable<Product> GenerateDrinkData()
    {
        return new List<Product>
        {
            new Product("Coca-Cola Lata",
                "Coca-Cola Lata de 350ML para matar a sua sede!",
                ProductType.Drink,
                4.50M,
                true,
                5),

            new Product("Heineken Longneck",
                "Heineken Longneck bem gelada.",
                ProductType.Drink,
                5.50M,
                true,
                6),
        };
    }

    private IEnumerable<Product> GenerateDessertData()
    {
        return new List<Product>
        {
            new Product("Sorvete de casquinha - chocolate",
                "Uma deliciosa casquinha de chocolate com sorvete de leite.",
                ProductType.Dessert,
                3.34M,
                true,
                7),

            new Product("Milkshake de morango e chocolate",
                "Um incrível milkshake de morango com leite e chocolate.",
                ProductType.Dessert,
                10.0M,
                true,
                8),
        };
    }
}