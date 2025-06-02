using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Database.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class ChangePaymentTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PAYMENT",
                table: "PAYMENT");

            migrationBuilder.RenameTable(
                name: "PAYMENT",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_PAYMENT_Uuid",
                table: "Payments",
                newName: "IX_Payments_Uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "PAYMENT");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_Uuid",
                table: "PAYMENT",
                newName: "IX_PAYMENT_Uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PAYMENT",
                table: "PAYMENT",
                column: "Id");
        }
    }
}
