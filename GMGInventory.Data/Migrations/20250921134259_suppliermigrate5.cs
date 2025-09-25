using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class suppliermigrate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Regions_RegionId",
                table: "Suppliers");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Suppliers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59d5d044-1e9b-4580-b481-8bd130dc7c35", "AQAAAAIAAYagAAAAEB8IycWOqoQh5XOqkTwEAl4IS4Mi2nYWvxxcPx9j/V6oz1jE4ZZjCWCigDZlXNpQpw==", "ea429ba7-9e76-4920-b8d7-de015945f74f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c520851-ed36-4431-949f-180e9d612166", "AQAAAAIAAYagAAAAEMiowsa3G2zgOYsGlL80yBvRKclnF4kBPmzSxXAY/5BS2emweGQo2Zwe0ydq8H7mag==", "d42a3eac-ad8e-4f15-8c1e-2ccea0b0419a" });

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Regions_RegionId",
                table: "Suppliers",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Regions_RegionId",
                table: "Suppliers");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ea47d4b-7ad7-4f7d-abe9-4d0833501fa3", "AQAAAAIAAYagAAAAEDvALM+Td2cpwJwa21fBXr4nIvFV+mXQSVfeL92qOTHj0ovMEB7rlWHzNwzByODymA==", "a45482b3-ce96-4f1e-9710-90805cd23d01" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef8c4d6f-cb4d-4be5-8e31-7bc402ee8648", "AQAAAAIAAYagAAAAEK4sTfIiOW82eLjm6CAAnPrdYT6HCm+WPZLHXi1Qr6cL/LmXHK/yEeWP79nRFsutaw==", "bdb2894c-23a8-416b-9835-6599975fb3b4" });

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Regions_RegionId",
                table: "Suppliers",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
