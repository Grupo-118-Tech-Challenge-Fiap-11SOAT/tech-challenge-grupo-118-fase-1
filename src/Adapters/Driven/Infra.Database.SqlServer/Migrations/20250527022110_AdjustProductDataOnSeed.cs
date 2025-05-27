using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Database.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AdjustProductDataOnSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProductId",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ImageProducts",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProductId",
                value: 1);
        }
    }
}
