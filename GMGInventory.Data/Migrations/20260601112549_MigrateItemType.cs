using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateItemType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_ItemTypes_ItemTypeId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_ItemTypeId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ItemTypeId",
                table: "Brands");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "fc4de692-b822-4958-9d2e-e2d3740bfa7d", "AQAAAAIAAYagAAAAEE8Dk5n3OncNCAvVit2tvi4+eVZWeJscYtF03EbpoaEAmUXUcRw3bEYhklqitMFKIw==", "a9856724-f609-4554-af1e-54f0a0901c10" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "87b25dcd-c9c0-4f7b-aca2-f9818d76f997", "AQAAAAIAAYagAAAAEC0SoTYY/vI9I+XMkLTyMJ2AFb39fMgeyQ8C/UQHxdchj3GM/CjCZGyhoM9YR6hz8Q==", "8d2a9f00-a411-4204-878d-93458484e3ba" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemTypeId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b07b8ae-564c-4000-845c-741179e6e882", "AQAAAAIAAYagAAAAELQL3m1vTKBzPenCE6tD4q7McUIKrywIaeO8UJ4yzkN7Tg2KV3mKFLn9UDoMhw7suA==", "0ec49fb7-94b6-42bf-b39c-912477213ef3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d704dad4-07ea-4537-bde5-a55d2d859a10", "AQAAAAIAAYagAAAAEPiBs08wgX4ZGsReAS7JyTNauCWeP3Nmy92f1c0QlPgJuSMvi6M8bf2D9wIIgBgAwA==", "cc4e24de-1672-46d9-9c87-92fa54b9111b" });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ItemTypeId",
                table: "Brands",
                column: "ItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_ItemTypes_ItemTypeId",
                table: "Brands",
                column: "ItemTypeId",
                principalTable: "ItemTypes",
                principalColumn: "Id");
        }
    }
}
