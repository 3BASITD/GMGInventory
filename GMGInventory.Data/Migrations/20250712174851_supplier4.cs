using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class supplier4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VAT",
                table: "Suppliers",
                type: "nvarchar(12)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "555a8e0a-1205-4a45-a386-7ca64b5386f2", "AQAAAAIAAYagAAAAELBvNqV0sHmOp7tRD3OO7W5IGefiHiaPxPSV5gvhIEtJXVTd3nEz941KXNk/3LxERQ==", "dfb1b1b5-2208-4693-abc6-9feb374ede1e" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "6f7dad90-52d0-42b8-9b73-ff69ad22b5ca", "AQAAAAIAAYagAAAAEG0A4f67j9UGYU/N8TNKNgGb4t13SpJ9ys3POwsd1hp/kehccyHsiwfMSUV9l7vBPA==", "fd7487d4-3f55-41dd-a5b1-2f48d794342c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VAT",
                table: "Suppliers",
                type: "nvarchar(12)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldNullable: true);

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
    }
}
