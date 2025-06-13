using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class itemoldsku3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldSKU",
                table: "Items",
                newName: "OldCode");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "9cb064a4-550e-483e-97dc-7fa44b87075a", "AQAAAAIAAYagAAAAEGVIbn8l+oyWGuo0jVIZNHAEo0V65Y7+ZHX2juLVW43y6yYzB+8WE6SS+/V1RuLovA==", "9c603717-50c8-4f8e-9f00-8ea4654c21ef" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "3e8f674c-2f0a-4951-8f6f-a1e582facb7c", "AQAAAAIAAYagAAAAEDJCgT2Frw5jlbtUTT/HfJyYaXWSgnl+dNVjbzD3Q3S0Ej+QFl9tVURIYYwH7bhigQ==", "66e24d06-77cb-4566-9020-7a5917559a8b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldCode",
                table: "Items",
                newName: "OldSKU");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f7e29b9f-8271-4b48-901b-d822db51e1bc", "AQAAAAIAAYagAAAAEE0ZHkVJkeMb+HG5rNqw3O3h92Obgifrwks4oQN2fL8qNOIeNeujon48IE/xCQMHeg==", "ae6fb350-bfb4-4f42-b8ea-1eddd0a4d2ed" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0dfd9ee9-977c-46a8-8704-7516035bb384", "AQAAAAIAAYagAAAAEFtYJF1/rlhfv9R7SufUP7ZFZ+uYze0WRKVG47oQbuAeiVRH5ejvuh+61YPdOJpDNA==", "f12b3f4a-b819-41ef-9232-1161c4b31ec2" });
        }
    }
}
