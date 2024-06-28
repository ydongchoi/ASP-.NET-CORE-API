using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedingEquipmentSensor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72016062-94dd-443f-9131-1f4e1d0ba1a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fb98f46-d0ab-46c6-b4ad-58e366cb7e97");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "196376c5-ba6e-459a-a961-51a4c433666b", null, "Administrator", "ADMINISTRATOR" },
                    { "715c9558-ab7e-4871-9efb-2998e8144ad7", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "EquipmentId", "CompanyId", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("a3e4c053-49b6-410c-bc78-2d54a9991823"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Equip2", "Stop" },
                    { new Guid("d2d4c053-49b6-410c-bc78-2d54a9991825"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Equip1", "InOperation" }
                });

            migrationBuilder.InsertData(
                table: "Sensors",
                columns: new[] { "SensorId", "EquipmentId", "Name", "Type" },
                values: new object[] { new Guid("c9a82a04-d9fd-4157-b0d5-bd2b72bb1915"), new Guid("d2d4c053-49b6-410c-bc78-2d54a9991825"), "Sensor1", "Pressure" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "196376c5-ba6e-459a-a961-51a4c433666b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "715c9558-ab7e-4871-9efb-2998e8144ad7");

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "EquipmentId",
                keyValue: new Guid("a3e4c053-49b6-410c-bc78-2d54a9991823"));

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "SensorId",
                keyValue: new Guid("c9a82a04-d9fd-4157-b0d5-bd2b72bb1915"));

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "EquipmentId",
                keyValue: new Guid("d2d4c053-49b6-410c-bc78-2d54a9991825"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "72016062-94dd-443f-9131-1f4e1d0ba1a2", null, "Administrator", "ADMINISTRATOR" },
                    { "9fb98f46-d0ab-46c6-b4ad-58e366cb7e97", null, "Manager", "MANAGER" }
                });
        }
    }
}
