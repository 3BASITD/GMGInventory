using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class sinvoiceRemarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Invoices",
                type: "nvarchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "452588c7-48d1-46a6-9567-5821b8839497", "AQAAAAIAAYagAAAAELdBqdGtPlwmNRS/GbZLGMxmC//5mDMr4DkW1afmrfXHM14aklY3cJmzwk1AdRsnhw==", "6eaf5c53-5a1d-4cfe-b681-be66d875177f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c5596b9-0e70-4a05-a8b1-cf78bb4dccf1", "AQAAAAIAAYagAAAAEBRpDmg2ybE0iPCzQ66f9ijTN7XQBzf+nZsxj777p1tu+FxU+GyyMrPZvR49Q9oFOQ==", "5376de64-f11a-43f2-911b-b398d80246a2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Invoices",
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
                values: new object[] { "830e665b-222a-4064-ab82-5cb6505a8872", "AQAAAAIAAYagAAAAEEZbuKc/BZhBrPqvY4cYaexrO8vRiRRu2lVJ8aCzXQCUygblOEFnBvS4d5KEhvHQ4g==", "2d3066ee-2d65-4273-9dfe-d8fd4a880360" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bcb71f7-9316-4370-9b5d-02fb5f3e0c70", "AQAAAAIAAYagAAAAEIxvUH98eRoBcEn7YQio87GFnDm+vUXzRWVfTagq/APrVJji9wRM3WfiqmpkAqqrjg==", "d7c16163-f0a6-46f3-8f10-8923d5b8412c" });
        }
    }
}
