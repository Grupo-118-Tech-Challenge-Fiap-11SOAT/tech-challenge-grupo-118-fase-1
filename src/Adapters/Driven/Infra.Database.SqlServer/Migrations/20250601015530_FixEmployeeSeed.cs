using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Database.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class FixEmployeeSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "QBYnGddxOZ/VOBgUr1koYDLMawbe/D8NaYYxOXQ0LHN8TO/ysQ5UvBZc70kbQkfXarxn+KobEuH7KpXkiElivg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "adminPass");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDay", "Cpf", "CreatedAt", "Email", "IsActive", "Name", "Password", "Role", "Surname" },
                values: new object[,]
                {
                    { 2, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "46002306048", new DateTimeOffset(new DateTime(2025, 4, 20, 10, 50, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "cook@cook.com", true, "Cook", "cookPass", "Cook", "Doe" },
                    { 3, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "75186057088", new DateTimeOffset(new DateTime(2025, 4, 20, 10, 50, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "waiter@waiter.com", true, "Waiter", "waiterPass", "Waiter", "Doe" },
                    { 4, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "90643516000", new DateTimeOffset(new DateTime(2025, 4, 20, 10, 50, 5, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "cleaner@cleaner.com", true, "Cleaner", "cleanerPass", "Cleaner", "Doe" }
                });
        }
    }
}
