using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Database.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddProductImageProductSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 0, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Hamburguer de 2 carnes com queijo e presunto. Acompanha de frango e bacon.", true, "Hamburguer 2 carnes", 35.95m, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, 0, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Hamburguer de 4 carnes com queijo, presunto e molho da casa. Acompanha de frango e bacon.", true, "Hamburguer 4 carnes", 50.95m, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, 1, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Uma salada ceasar bem temperada.", true, "Salada Ceasar", 15.95m, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 4, 1, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Deliciosos onion rings com cebola e tomate.", true, "Onion Rings", 20.95m, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 5, 2, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Coca-Cola Lata de 350ML para matar a sua sede!", true, "Coca-Cola Lata", 4.50m, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 6, 2, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Heineken Longneck bem gelada.", true, "Heineken Longneck", 8.50m, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 7, 3, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Uma deliciosa casquinha de chocolate com sorvete de leite.", true, "Sorvete de casquinha - chocolate", 3.34m, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 8, 3, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Um incrível milkshake de morango com leite e chocolate.", true, "Milkshake de morango e chocolate", 100m, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "ImageProducts",
                columns: new[] { "Id", "CreatedAt", "Position", "ProductId", "UpdatedAt", "Url" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 1, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://ogimg.infoglobo.com.br/rioshow/25033674-ea4-3ac/FT1086A/smash-burger.jpg" },
                    { 2, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://seurestaurante.spotdelivery.com.br/wp-content/uploads/2016/09/37.png" },
                    { 3, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 2, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://www.mulher.com.br/wp-content/uploads/migration/big-four-0419-1400x1400.jpg.jpg" },
                    { 4, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 1, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://d1muf25xaso8hp.cloudfront.net/https://img.criativodahora.com.br/2024/01/criativo-65b82d664176bMjkvMDEvMjAyNCAxOWg1Nw==.jpg" },
                    { 5, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 3, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://static.itdg.com.br/images/1200-630/f6acb58cd0215a6d2118c4a87ebab1fe/153730-original.jpg" },
                    { 6, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 3, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://dicheff.com.br/wp-content/uploads/2023/09/salada-caesar.jpg" },
                    { 7, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 4, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://amandascookin.com/wp-content/uploads/2022/06/Onion-Rings-RCSQ.jpg" },
                    { 8, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 4, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://www.kuchpakrahahai.in/wp-content/uploads/2023/09/Air-fryer-onion-rings-vegan.jpg" },
                    { 9, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 5, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://telecris.app/wp-content/uploads/2024/06/imagem-produto-coca-cola-lata-50-telecris-galeteria-cristofoli.png" },
                    { 10, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 5, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://images.tcdn.com.br/img/img_prod/816596/coca_cola_lata_350ml_sabor_original_1503_2_316da21a214d3eaa1b8eb4bf0075527a.jpg" },
                    { 11, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 6, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://www.ycar.com.br/site20/wp-content/uploads/2020/02/heineken-anuncia-recall-de-garrafas-long-neck.png" },
                    { 12, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 6, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://a-static.mlcdn.com.br/800x600/cerveja-heineken-premium-puro-malte-lager-pilsen-6-garrafas-long-neck-330ml/magazineluiza/225339400/4d6ddb2219e15a46f011c71a4da05120.jpg" },
                    { 13, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 7, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://img.freepik.com/fotos-premium/casquinha-de-sorvete-com-cobertura-de-chocolate_499484-1493.jpg" },
                    { 14, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 7, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://png.pngtree.com/png-vector/20240525/ourlarge/pngtree-a-chocolate-ice-cream-cone-png-image_12523196.png" },
                    { 15, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 8, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://mavalerio.com.br/wp-content/uploads/2019/07/181002_receitas-415-1-500x340.png" },
                    { 16, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 8, new DateTimeOffset(new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "https://p2.trrsf.com/image/fget/cf/940/0/images.terra.com/2023/09/12/890326590-milkshake-de-chocolate-crocante-1024x576.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
