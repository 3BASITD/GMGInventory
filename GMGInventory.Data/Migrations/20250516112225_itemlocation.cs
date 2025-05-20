using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class itemlocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLocation_Items_ItemId",
                table: "ItemLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemLocation_LocationCodes_LocationCodeId",
                table: "ItemLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemLocation",
                table: "ItemLocation");

            migrationBuilder.RenameTable(
                name: "ItemLocation",
                newName: "ItemLocations");

            migrationBuilder.RenameIndex(
                name: "IX_ItemLocation_LocationCodeId",
                table: "ItemLocations",
                newName: "IX_ItemLocations_LocationCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemLocation_ItemId",
                table: "ItemLocations",
                newName: "IX_ItemLocations_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemLocations",
                table: "ItemLocations",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4834bbba-81c6-4c14-9f4a-b608fa1d74b4", "AQAAAAIAAYagAAAAEEnA7AyzsQ/BgRZD8XDhwSe4pBe65n32Rpg1Zyug+f2mQjT1D50k8LNr84xuzOQtuw==", "2264d448-a87e-440d-a4da-e7a7858f9692" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4067438a-e2b6-4b4c-b9d8-9b1866b71df0", "AQAAAAIAAYagAAAAEJ/VIpp4d2c4qDEGZ/Ch013GjC3jyikH+a97y+CKrybilOe7NmuV1LZK0+dBNSyjnw==", "4cc5a528-49c7-4347-9dda-6886002aa0ee" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLocations_Items_ItemId",
                table: "ItemLocations",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLocations_LocationCodes_LocationCodeId",
                table: "ItemLocations",
                column: "LocationCodeId",
                principalTable: "LocationCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLocations_Items_ItemId",
                table: "ItemLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemLocations_LocationCodes_LocationCodeId",
                table: "ItemLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemLocations",
                table: "ItemLocations");

            migrationBuilder.RenameTable(
                name: "ItemLocations",
                newName: "ItemLocation");

            migrationBuilder.RenameIndex(
                name: "IX_ItemLocations_LocationCodeId",
                table: "ItemLocation",
                newName: "IX_ItemLocation_LocationCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemLocations_ItemId",
                table: "ItemLocation",
                newName: "IX_ItemLocation_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemLocation",
                table: "ItemLocation",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLocation_Items_ItemId",
                table: "ItemLocation",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLocation_LocationCodes_LocationCodeId",
                table: "ItemLocation",
                column: "LocationCodeId",
                principalTable: "LocationCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
