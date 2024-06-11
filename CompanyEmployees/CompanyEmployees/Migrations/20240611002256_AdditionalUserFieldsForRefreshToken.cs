using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalUserFieldsForRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "207cdbd0-01c3-46f5-88e6-6f6228611331");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb660bb6-b99a-4231-a10d-243cb65d0c4c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "034976af-59ad-424d-b603-a96510a40e4d", null, "Administrator", "ADMINISTRATOR" },
                    { "3d41f1e9-7976-49b6-a213-4b07e504349d", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "034976af-59ad-424d-b603-a96510a40e4d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d41f1e9-7976-49b6-a213-4b07e504349d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "207cdbd0-01c3-46f5-88e6-6f6228611331", null, "Manager", "MANAGER" },
                    { "cb660bb6-b99a-4231-a10d-243cb65d0c4c", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
