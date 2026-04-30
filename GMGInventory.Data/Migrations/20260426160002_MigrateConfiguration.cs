using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de18ebf0-eeab-4793-9551-9a0b6648806b", "AQAAAAIAAYagAAAAEP+JGeCWnNnIVEW3OYclKbv9jGRyoIBN3j024x7MPpInrf/6/BphW+xZvTLJIkqA0w==", "abda63c5-f9e8-448b-a2ca-4da498b33c6d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "890ff2b3-d704-4c9e-925f-6b0e80b56635", "AQAAAAIAAYagAAAAEBVRn3gUxUDnzxrrnniuWUAcyTu6o9nD+tGMHwlP68sqJUsZm8ZdTcG9u7KeGKgEjg==", "c8bfdc49-77e5-4dc6-a25c-d8a5234e772b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f6eaaf5-ec45-4789-8777-4d7b2368ec35", "AQAAAAIAAYagAAAAEOnI+7d/d9nmqauJ66wAuajzYO9IhEOkoioP627kNx20GIs65Irz9T98WVtJpApbXQ==", "f8a69686-c044-4f94-bc50-e011ffbcba68" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53497f40-8f13-45f2-9c95-c1d5a19c15f2", "AQAAAAIAAYagAAAAEPvxggC+EaTZucphMmoqvKE6eOrGskc7TpGqGUwgMLzVJNZ/gvLdD4VNQLUN6vlZUQ==", "a382e553-66e9-4e52-8ec3-f90396d52072" });
        }
    }
}
