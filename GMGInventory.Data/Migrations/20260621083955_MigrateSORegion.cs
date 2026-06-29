using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateSORegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 1);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "500953fb-c779-4dd5-8644-148b28bb5d37", "AQAAAAIAAYagAAAAEJgetzFKM3+ZLdCgqZXsyhc4UYLAd3rCcGFpCK0Upnuz0S38l3Kpf3Z+hLgfome15Q==", "83a6acc4-aa3d-4347-8588-19bea827d2e6" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "8b55d389-1f9a-42e6-87af-399e236177c9", "AQAAAAIAAYagAAAAEN4AazMkPN800NnYpazcJVcQly+ZrA8Vew8t6wq0Fyy3kdsFqJuWFwo+SLRM7nEkxA==", "e16ae171-dbb6-4f13-855c-73876131a340" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_RegionId",
                table: "SalesOrders",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Regions_RegionId",
                table: "SalesOrders",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Regions_RegionId",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_RegionId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "SalesOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc4de692-b822-4958-9d2e-e2d3740bfa7d", "AQAAAAIAAYagAAAAEE8Dk5n3OncNCAvVit2tvi4+eVZWeJscYtF03EbpoaEAmUXUcRw3bEYhklqitMFKIw==", "a9856724-f609-4554-af1e-54f0a0901c10" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87b25dcd-c9c0-4f7b-aca2-f9818d76f997", "AQAAAAIAAYagAAAAEC0SoTYY/vI9I+XMkLTyMJ2AFb39fMgeyQ8C/UQHxdchj3GM/CjCZGyhoM9YR6hz8Q==", "8d2a9f00-a411-4204-878d-93458484e3ba" });
        }
    }
}
