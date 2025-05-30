using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Database.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserPaymentCode = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 0, "Hamburguer de 2 carnes com queijo e presunto. Acompanha de frango e bacon.", true, "Hamburguer 2 carnes", 35.95m },
                    { 2, 0, "Hamburguer de 4 carnes com queijo, presunto e molho da casa. Acompanha de frango e bacon.", true, "Hamburguer 4 carnes", 50.95m },
                    { 3, 1, "Uma salada ceasar bem temperada.", true, "Salada Ceasar", 15.95m },
                    { 4, 1, "Deliciosos onion rings com cebola e tomate.", true, "Onion Rings", 20.95m },
                    { 5, 2, "Coca-Cola Lata de 350ML para matar a sua sede!", true, "Coca-Cola Lata", 4.50m },
                    { 6, 2, "Heineken Longneck bem gelada.", true, "Heineken Longneck", 5.50m },
                    { 7, 3, "Uma deliciosa casquinha de chocolate com sorvete de leite.", true, "Sorvete de casquinha - chocolate", 3.34m },
                    { 8, 3, "Um incrível milkshake de morango com leite e chocolate.", true, "Milkshake de morango e chocolate", 10.0m }
                });

            migrationBuilder.InsertData(
                table: "ImageProducts",
                columns: new[] { "Id", "Position", "ProductId", "Url" },
                values: new object[,]
                {
                    { 1, 1, 1, "https://ogimg.infoglobo.com.br/rioshow/25033674-ea4-3ac/FT1086A/smash-burger.jpg" },
                    { 2, 2, 1, "https://seurestaurante.spotdelivery.com.br/wp-content/uploads/2016/09/37.png" },
                    { 3, 1, 2, "https://www.mulher.com.br/wp-content/uploads/migration/big-four-0419-1400x1400.jpg.jpg" },
                    { 4, 2, 2, "https://d1muf25xaso8hp.cloudfront.net/https://img.criativodahora.com.br/2024/01/criativo-65b82d664176bMjkvMDEvMjAyNCAxOWg1Nw==.jpg" },
                    { 5, 1, 3, "https://static.itdg.com.br/images/1200-630/f6acb58cd0215a6d2118c4a87ebab1fe/153730-original.jpg" },
                    { 6, 2, 3, "https://dicheff.com.br/wp-content/uploads/2023/09/salada-caesar.jpg" },
                    { 7, 1, 4, "https://amandascookin.com/wp-content/uploads/2022/06/Onion-Rings-RCSQ.jpg" },
                    { 8, 2, 4, "https://www.kuchpakrahahai.in/wp-content/uploads/2023/09/Air-fryer-onion-rings-vegan.jpg" },
                    { 13, 1, 7, "https://img.freepik.com/fotos-premium/casquinha-de-sorvete-com-cobertura-de-chocolate_499484-1493.jpg" },
                    { 14, 2, 7, "https://png.pngtree.com/png-vector/20240525/ourlarge/pngtree-a-chocolate-ice-cream-cone-png-image_12523196.png" },
                    { 15, 1, 8, "https://mavalerio.com.br/wp-content/uploads/2019/07/181002_receitas-415-1-500x340.png" },
                    { 16, 2, 8, "https://p2.trrsf.com/image/fget/cf/940/0/images.terra.com/2023/09/12/890326590-milkshake-de-chocolate-crocante-1024x576.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Cpf",
                table: "Employees",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageProducts_ProductId",
                table: "ImageProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PAYMENT_Uuid",
                table: "PAYMENT",
                column: "Uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ImageProducts");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "PAYMENT");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
