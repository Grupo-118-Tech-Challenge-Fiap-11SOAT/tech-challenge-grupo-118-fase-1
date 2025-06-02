using Domain.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.SqlServer.Products.Configuration;

public class ImageProductsConfiguration : IEntityTypeConfiguration<Domain.Products.Entities.ImageProduct>
{
    private const string DEFAULT_DATETIMEOFFSET_COLUMN_TYPE = "datetimeoffset";
    private const string DEFAULT_DATETIMEOFFSET_COLUMN_DEFAULT_VALUE = "SYSDATETIMEOFFSET()";

    private readonly DateTimeOffset DefaultDateTimeOffset = new DateTimeOffset(2025, 5, 26, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<Domain.Products.Entities.ImageProduct> builder)
    {
        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnType(DEFAULT_DATETIMEOFFSET_COLUMN_TYPE)
            .HasDefaultValueSql(DEFAULT_DATETIMEOFFSET_COLUMN_DEFAULT_VALUE)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.UpdatedAt)
            .HasColumnType(nameof(DateTimeOffset).ToLower())
            .HasDefaultValueSql(DEFAULT_DATETIMEOFFSET_COLUMN_DEFAULT_VALUE);

        builder.HasOne(i => i.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.ProductId);

        builder.Ignore(p => p.IsActive);

        builder.HasData(GenerateImageProductData());
    }

    private IEnumerable<ImageProduct> GenerateImageProductData()
    {
        var listToSeed = new List<ImageProduct>();

        listToSeed.AddRange(GenerateSnackImageProductData());
        listToSeed.AddRange(GenerateAccompanimentImageProductData());

        listToSeed.AddRange(GenerateDrinkImageProductData());
        listToSeed.AddRange(GenerateDessertImageProductData());

        return listToSeed;
    }

    private IEnumerable<ImageProduct> GenerateSnackImageProductData()
    {
        return new List<ImageProduct>()
        {
            new(productId: 1,
                position: 1,
                url: "https://ogimg.infoglobo.com.br/rioshow/25033674-ea4-3ac/FT1086A/smash-burger.jpg",
                id: 1),

            new(productId: 1,
                position: 2,
                url: "https://seurestaurante.spotdelivery.com.br/wp-content/uploads/2016/09/37.png",
                id: 2),

            new(productId: 2,
                position: 1,
                url: "https://www.mulher.com.br/wp-content/uploads/migration/big-four-0419-1400x1400.jpg.jpg",
                id: 3),

            new(productId: 2,
                position: 2,
                url:
                "https://d1muf25xaso8hp.cloudfront.net/https://img.criativodahora.com.br/2024/01/criativo-65b82d664176bMjkvMDEvMjAyNCAxOWg1Nw==.jpg",
                id: 4),
        };
    }

    private IEnumerable<ImageProduct> GenerateAccompanimentImageProductData()
    {
        return new List<ImageProduct>()
        {
            new(productId: 3,
                position: 1,
                url: "https://static.itdg.com.br/images/1200-630/f6acb58cd0215a6d2118c4a87ebab1fe/153730-original.jpg",
                id: 5),

            new(productId: 3,
                position: 2,
                url: "https://dicheff.com.br/wp-content/uploads/2023/09/salada-caesar.jpg",
                id: 6),

            new(productId: 4,
                position: 1,
                url: "https://amandascookin.com/wp-content/uploads/2022/06/Onion-Rings-RCSQ.jpg",
                id: 7),

            new(productId: 4,
                position: 2,
                url:
                "https://www.kuchpakrahahai.in/wp-content/uploads/2023/09/Air-fryer-onion-rings-vegan.jpg",
                id: 8),
        };
    }

    private IEnumerable<ImageProduct> GenerateDrinkImageProductData()
    {
        return new List<ImageProduct>()
        {
            new(productId: 7,
                position: 1,
                url:
                "https://img.freepik.com/fotos-premium/casquinha-de-sorvete-com-cobertura-de-chocolate_499484-1493.jpg",
                id: 13),

            new(productId: 7,
                position: 2,
                url:
                "https://png.pngtree.com/png-vector/20240525/ourlarge/pngtree-a-chocolate-ice-cream-cone-png-image_12523196.png",
                id: 14),

            new(productId: 8,
                position: 1,
                url: "https://mavalerio.com.br/wp-content/uploads/2019/07/181002_receitas-415-1-500x340.png",
                id: 15),

            new(productId: 8,
                position: 2,
                url:
                "https://p2.trrsf.com/image/fget/cf/940/0/images.terra.com/2023/09/12/890326590-milkshake-de-chocolate-crocante-1024x576.jpg",
                id: 16),
        };
    }

    private IEnumerable<ImageProduct> GenerateDessertImageProductData()
    {
        return new List<ImageProduct>()
        {
        };
    }
}