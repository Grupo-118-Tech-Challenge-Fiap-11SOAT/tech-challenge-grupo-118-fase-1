using Infra.Database.SqlServer.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.SqlServer.Products.Configuration;

public class ImageProductsConfiguration : IEntityTypeConfiguration<ImageProduct>
{
    private const string DEFAULT_DATETIMEOFFSET_COLUMN_TYPE = "datetimeoffset";
    private const string DEFAULT_DATETIMEOFFSET_COLUMN_DEFAULT_VALUE = "SYSDATETIMEOFFSET()";

    private readonly DateTimeOffset DefaultDateTimeOffset = new DateTimeOffset(2025, 5, 26, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<ImageProduct> builder)
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
            new()
            {
                Id = 1,
                ProductId = 1,
                Url = "https://ogimg.infoglobo.com.br/rioshow/25033674-ea4-3ac/FT1086A/smash-burger.jpg",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 1
            },
            new()
            {
                Id = 2,
                ProductId = 1,
                Url = "https://seurestaurante.spotdelivery.com.br/wp-content/uploads/2016/09/37.png",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 2
            },
            new()
            {
                Id = 3,
                ProductId = 2,
                Url = "https://www.mulher.com.br/wp-content/uploads/migration/big-four-0419-1400x1400.jpg.jpg",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 1
            },
            new()
            {
                Id = 4,
                ProductId = 2,
                Url =
                    "https://d1muf25xaso8hp.cloudfront.net/https://img.criativodahora.com.br/2024/01/criativo-65b82d664176bMjkvMDEvMjAyNCAxOWg1Nw==.jpg",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 2
            }
        };
    }

    private IEnumerable<ImageProduct> GenerateAccompanimentImageProductData()
    {
        return new List<ImageProduct>()
        {
            new()
            {
                Id = 5,
                ProductId = 3,
                Url = "https://static.itdg.com.br/images/1200-630/f6acb58cd0215a6d2118c4a87ebab1fe/153730-original.jpg",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 1
            },
            new()
            {
                Id = 6,
                ProductId = 3,
                Url = "https://dicheff.com.br/wp-content/uploads/2023/09/salada-caesar.jpg",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 2
            },
            new()
            {
                Id = 7,
                ProductId = 4,
                Url = "https://amandascookin.com/wp-content/uploads/2022/06/Onion-Rings-RCSQ.jpg",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 1
            },
            new()
            {
                Id = 8,
                ProductId = 4,
                Url = "https://www.kuchpakrahahai.in/wp-content/uploads/2023/09/Air-fryer-onion-rings-vegan.jpg",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 2
            }
        };
    }

    private IEnumerable<ImageProduct> GenerateDrinkImageProductData()
    {
        return new List<ImageProduct>()
        {
            new()
            {
                Id = 9,
                ProductId = 5,
                Url =
                    "https://telecris.app/wp-content/uploads/2024/06/imagem-produto-coca-cola-lata-50-telecris-galeteria-cristofoli.png",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 1
            },
            new()
            {
                Id = 10,
                ProductId = 5,
                Url =
                    "https://images.tcdn.com.br/img/img_prod/816596/coca_cola_lata_350ml_sabor_original_1503_2_316da21a214d3eaa1b8eb4bf0075527a.jpg",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 2
            },
            new()
            {
                Id = 11,
                ProductId = 6,
                Url =
                    "https://www.ycar.com.br/site20/wp-content/uploads/2020/02/heineken-anuncia-recall-de-garrafas-long-neck.png",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 1
            },
            new()
            {
                Id = 12,
                ProductId = 6,
                Url =
                    "https://a-static.mlcdn.com.br/800x600/cerveja-heineken-premium-puro-malte-lager-pilsen-6-garrafas-long-neck-330ml/magazineluiza/225339400/4d6ddb2219e15a46f011c71a4da05120.jpg",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 2
            }
        };
    }

    private IEnumerable<ImageProduct> GenerateDessertImageProductData()
    {
        return new List<ImageProduct>()
        {
            new()
            {
                Id = 13,
                ProductId = 7,
                Url =
                    "https://img.freepik.com/fotos-premium/casquinha-de-sorvete-com-cobertura-de-chocolate_499484-1493.jpg",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 1
            },
            new()
            {
                Id = 14,
                ProductId = 7,
                Url =
                    "https://png.pngtree.com/png-vector/20240525/ourlarge/pngtree-a-chocolate-ice-cream-cone-png-image_12523196.png",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 2
            },
            new()
            {
                Id = 15,
                ProductId = 8,
                Url = "https://mavalerio.com.br/wp-content/uploads/2019/07/181002_receitas-415-1-500x340.png",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 1
            },
            new()
            {
                Id = 16,
                ProductId = 8,
                Url =
                    "https://p2.trrsf.com/image/fget/cf/940/0/images.terra.com/2023/09/12/890326590-milkshake-de-chocolate-crocante-1024x576.jpg",
                CreatedAt = DefaultDateTimeOffset,
                UpdatedAt = DefaultDateTimeOffset,
                Position = 2
            }
        };
    }
}