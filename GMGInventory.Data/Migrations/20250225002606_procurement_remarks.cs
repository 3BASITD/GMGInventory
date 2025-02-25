using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class procurement_remarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseReturnNews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseRequests",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseOrderNews",
                type: "nvarchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bb71077-d07d-4ff7-8c7d-d9a07a41e590", "AQAAAAIAAYagAAAAEBXZ0+wQZzldkLaKDQSA8/UKsYU6MTUHKg4vaZ6p2dhJrYIa9nMxKfjyRH1NbzhJGw==", "bdedfb5b-2d96-41c0-a1e4-ae9c5b176cfe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67769012-4360-420c-8f98-47d382ef9bd5", "AQAAAAIAAYagAAAAENpjb24sxcWQaOnDjMAwQjv5+/ITByXvWzuPGmCLoDkFSizW1LrRfA3Kf2SwoYzWuA==", "317f988e-f928-414e-8bd5-5138c1b362bd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseReturnNews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseRequests",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseOrderNews",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd8af0e6-3417-423a-bd69-e63dcf5f0c83", "AQAAAAIAAYagAAAAEDFQYfJs8HdZR0J2GYuGLLMEIlf7JR3oCcwVfPjizP6kBobIZO6zVDN7AvkO1lH5cQ==", "9689f8e6-462a-4e56-baeb-da6e2c97e825" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0986c126-ebb9-45fe-b1c9-fa116195205f", "AQAAAAIAAYagAAAAEE5eq07LWzpbokypJsfajViVqPr+qWrDkWehz0p4Kb7sBzv4R1U/bb7m2J19HNGS3g==", "c646a47f-0e5b-4944-b222-d0d24ab5e777" });
        }
    }
}
