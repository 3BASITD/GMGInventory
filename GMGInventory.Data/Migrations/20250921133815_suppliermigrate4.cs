using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class suppliermigrate4 : Migration
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "173dc481-0e6b-4a6c-a712-e4ba5d47083b", "AQAAAAIAAYagAAAAEJI9YQhDXIHiOSK7tzXT+TWjwhQkHfDdO3PutIId0W2rwd0t3lYxWvFWLYlApoe9qQ==", "44854276-d9f1-40db-8beb-2dc4c2eb154b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53585fff-7d5f-4edb-a86e-9892e492d19d", "AQAAAAIAAYagAAAAEPhZSh+/NlnBRybLuYuoLbn1bOz6SXmGDGhgoxqvaCbF6u1Wkl8pMn3QRgO3OhYvjg==", "accaa79e-84d6-4c45-baba-2bc70f8dcc12" });

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Regions_RegionId",
                table: "Suppliers",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");
        }
    }
}
