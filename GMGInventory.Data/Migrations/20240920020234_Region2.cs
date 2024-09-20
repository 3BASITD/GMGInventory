using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Region2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Regions",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Regions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Regions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Regions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Regions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Regions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "306da8cf-4a9c-4c8c-b37e-244507d2504e", new DateTime(2024, 9, 20, 10, 2, 33, 363, DateTimeKind.Local).AddTicks(5091), "AQAAAAIAAYagAAAAEN0Sby7eGZ/Q9iqxNKP7JmRwWqliS+OU/hrnWcZN0ohi0NIBnTFWMcCqgqVNTFUGQA==", "37707739-0a92-4b20-bb90-3f5fc1975f77" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cef40c76-a0c1-426e-94ed-5c62393fb816", new DateTime(2024, 9, 20, 10, 2, 33, 278, DateTimeKind.Local).AddTicks(5590), "AQAAAAIAAYagAAAAEOIgOhxZc7SYslNgtyA1hgcY49RvHy0WkdQN99Z1v8sBIz1h/M+cMTSeynQaLAXCKw==", "b940b505-f899-4a80-840d-7733444c3bc8" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "UserCreated" },
                values: new object[] { new DateTime(2024, 9, 20, 10, 2, 33, 192, DateTimeKind.Local).AddTicks(7168), "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "UserCreated" },
                values: new object[] { new DateTime(2024, 9, 20, 10, 2, 33, 192, DateTimeKind.Local).AddTicks(7187), "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "UserCreated" },
                values: new object[] { new DateTime(2024, 9, 20, 10, 2, 33, 192, DateTimeKind.Local).AddTicks(7190), "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "UserCreated" },
                values: new object[] { new DateTime(2024, 9, 20, 10, 2, 33, 192, DateTimeKind.Local).AddTicks(7192), "CDE19596-2C9E-4ECE-837C-85950E9E907D" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Regions");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Regions",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8950ca25-7ac3-4450-b976-89ee4aac3395", new DateTime(2024, 9, 19, 16, 3, 13, 477, DateTimeKind.Local).AddTicks(3444), "AQAAAAIAAYagAAAAEK/yU5uHpbm2VwWip0jlt6Cup9SJ/wbn9HM7l5jgcimCE9yZlqFSmFDalVYLudXxMw==", "939ede76-8f6f-452e-b96c-b573594a8d8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e7c67b1-f7c4-4c45-937c-f76ed61d6018", new DateTime(2024, 9, 19, 16, 3, 13, 373, DateTimeKind.Local).AddTicks(2114), "AQAAAAIAAYagAAAAEMWBG2/3FEvgNMtWhpZQyONxvNfRZ0uEj6xrCYlpHx/C4dRla/yCBp+HYHTIKh+oWA==", "77f5445b-5491-4f45-a6d8-7697f440dc53" });
        }
    }
}
