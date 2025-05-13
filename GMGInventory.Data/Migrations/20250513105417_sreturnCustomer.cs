using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class sreturnCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "SalesReturns",
                type: "int",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "830e665b-222a-4064-ab82-5cb6505a8872", "AQAAAAIAAYagAAAAEEZbuKc/BZhBrPqvY4cYaexrO8vRiRRu2lVJ8aCzXQCUygblOEFnBvS4d5KEhvHQ4g==", "2d3066ee-2d65-4273-9dfe-d8fd4a880360" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "2bcb71f7-9316-4370-9b5d-02fb5f3e0c70", "AQAAAAIAAYagAAAAEIxvUH98eRoBcEn7YQio87GFnDm+vUXzRWVfTagq/APrVJji9wRM3WfiqmpkAqqrjg==", "d7c16163-f0a6-46f3-8f10-8923d5b8412c" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_CustomerId",
                table: "SalesReturns",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_Customers_CustomerId",
                table: "SalesReturns",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_Customers_CustomerId",
                table: "SalesReturns");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturns_CustomerId",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "SalesReturns");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "b8a74e42-805b-4963-ad46-c4c2ad5912fe", "AQAAAAIAAYagAAAAEKEC7p5yqUQCjXoGmCsglMujQM+pGQxL0wK3/duStTntl7e38N1Sw5ZGqvLl6hMewQ==", "f94153b6-b341-42f2-abf1-6e5f24c0aa08" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "a966ca01-1376-42eb-87d2-8a425af0ac94", "AQAAAAIAAYagAAAAEByEGcbkmR20f51qW1BrV4zXwuZSC7m/nVFCCEuVg3SBtwZbc9eMsGGOb1kP4XVdhA==", "ac9fda08-ad10-480d-bb9a-263f909d757b" });
        }
    }
}
