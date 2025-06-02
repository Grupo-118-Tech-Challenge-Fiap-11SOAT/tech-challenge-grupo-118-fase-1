using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Database.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class employeeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDay", "Cpf", "CreatedAt", "Email", "IsActive", "Name", "Password", "Role", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "98659502000", new DateTimeOffset(new DateTime(2025, 4, 20, 10, 50, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "admin@admin.com", true, "Admin", "adminPass", "Admin", "Doe" },
                    { 2, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "46002306048", new DateTimeOffset(new DateTime(2025, 4, 20, 10, 50, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "cook@cook.com", true, "Cook", "cookPass", "Cook", "Doe" },
                    { 3, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "75186057088", new DateTimeOffset(new DateTime(2025, 4, 20, 10, 50, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "waiter@waiter.com", true, "Waiter", "waiterPass", "Waiter", "Doe" },
                    { 4, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "90643516000", new DateTimeOffset(new DateTime(2025, 4, 20, 10, 50, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "cleaner@cleaner.com", true, "Cleaner", "cleanerPass", "Cleaner", "Doe" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
