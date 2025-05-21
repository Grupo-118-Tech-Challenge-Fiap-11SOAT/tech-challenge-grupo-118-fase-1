using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Database.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class removingEmployeeIdIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_Id",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_Id",
                table: "Employees",
                column: "Id");
        }
    }
}
