using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Webapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("4d66633e-4b78-49db-a92b-ee609845ae52"), "312 Forest Avenue, BF 923", "USA", "Admin_Solutions Ltd" },
                    { new Guid("ffaf1c5e-49ac-4c33-a92e-e4559a43d7bc"), "583 Wall Dr. Gwynn Oak, MD 21207", "USA", "IT_Solutions Ltd" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("0ca9d57d-5ac5-4d32-a2b7-1daedc5eece1"), 30, new Guid("ffaf1c5e-49ac-4c33-a92e-e4559a43d7bc"), "Jana McLeaf", "Software developer" },
                    { new Guid("b135c96f-a657-4b02-9594-a7dabccf1812"), 26, new Guid("ffaf1c5e-49ac-4c33-a92e-e4559a43d7bc"), "Sam Raiden", "Software developer" },
                    { new Guid("ffde63c9-7bbb-4ebe-8d47-4ff9c2782a0c"), 35, new Guid("4d66633e-4b78-49db-a92b-ee609845ae52"), "Kane Miller", "Administrator" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("0ca9d57d-5ac5-4d32-a2b7-1daedc5eece1"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("b135c96f-a657-4b02-9594-a7dabccf1812"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("ffde63c9-7bbb-4ebe-8d47-4ff9c2782a0c"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("4d66633e-4b78-49db-a92b-ee609845ae52"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("ffaf1c5e-49ac-4c33-a92e-e4559a43d7bc"));
        }
    }
}
