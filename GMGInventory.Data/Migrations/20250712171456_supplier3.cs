using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class supplier3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TIN",
                table: "Suppliers",
                type: "nvarchar(12)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)");

            migrationBuilder.AlterColumn<string>(
                name: "OfficeTelNo",
                table: "Suppliers",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Suppliers",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "Suppliers",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "Suppliers",
                type: "nvarchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f097aea1-e073-41a3-be03-870c80a44739", "AQAAAAIAAYagAAAAEP+zPq380nd0022/GqwM7Zjo6X6wTLWRUnokGQ8iabV0kQVSWyuMwrVT32JYFKgbZA==", "1b7b940b-f3e8-4beb-b56b-1e468d0269f3" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "1f30b240-bff1-412b-8de8-e9584ecfab8a", "AQAAAAIAAYagAAAAEJ1jUrDtdHqojMX2CGfhxRipuFu3k6XWg/F5y8kIc3C7cMrnbkME9KgTYOSGavh/Sg==", "0e1c5ac7-672c-4c0f-aa79-b28ce8f8e6dd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TIN",
                table: "Suppliers",
                type: "nvarchar(12)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OfficeTelNo",
                table: "Suppliers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Suppliers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "Suppliers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "Suppliers",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldNullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e4999e9d-6fbf-4a3a-b47e-135843d7dca7", "AQAAAAIAAYagAAAAEL6gyt7FqTZNfuCkz8C26UoxgZIac/NpJTC1G6XtnjzyQ4SHLrRNWXMyZKDqntPuwg==", "02650401-2fab-4d42-8cfa-84e5d38af0ef" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "5ec9bf0c-6be1-4f20-b619-5cae934c15e8", "AQAAAAIAAYagAAAAECl+Xr2/xL+bP1eGV2HwTFRAR/fqVo+dH9COAWvEokVscf/+sjTtHEQVNwBSSyg8Jw==", "d4c8126f-f231-4f31-afc0-5f7948598c09" });
        }
    }
}
