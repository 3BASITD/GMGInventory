using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MinorChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itemPrices_Items_ItemId",
                table: "itemPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_itemPrices",
                table: "itemPrices");

            migrationBuilder.RenameTable(
                name: "itemPrices",
                newName: "ItemPrices");

            migrationBuilder.RenameIndex(
                name: "IX_itemPrices_ItemId",
                table: "ItemPrices",
                newName: "IX_ItemPrices_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemPrices",
                table: "ItemPrices",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "cef493f9-7e9d-4447-ab21-ffb91f2e1327");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "67959ac1-cd22-4878-a9a7-d6abf4d695e5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "27ae3f9d-d17d-4363-9b1a-ba52bf8cca15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "ee117075-aa51-48ec-ba30-3fddd44c542c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "2f1a2601-3f5b-4c9b-91e9-c478e42574d5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "7f041e9f-ed3b-43b6-9d66-8a5bb2d1128e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20acbed1-352a-4d46-8645-80457a481b1c", new DateTime(2022, 7, 20, 4, 18, 48, 815, DateTimeKind.Local).AddTicks(3826), "AQAAAAEAACcQAAAAEOyXL4h3EdvKsN7N0NqLcgPjLENSYAtJxLGMO2cvQiN0eeXzmInWwo5Wu3Ep0kfk8A==", "a9820aa9-ab1c-4220-ac53-61aecbaeec82" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ead3b30a-45fb-40d8-a950-139a56108c93", new DateTime(2022, 7, 20, 4, 18, 48, 807, DateTimeKind.Local).AddTicks(848), "AQAAAAEAACcQAAAAEK0MrKHm0tPcc6W8KCkyFVc4Zh3NyuU9QcsstC1o9ECXJz94vNIa3I9Nkk8clnDgVg==", "11c0ccfe-9f9a-4c45-8b67-d92d51329d0d" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_Items_ItemId",
                table: "ItemPrices",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_Items_ItemId",
                table: "ItemPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemPrices",
                table: "ItemPrices");

            migrationBuilder.RenameTable(
                name: "ItemPrices",
                newName: "itemPrices");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPrices_ItemId",
                table: "itemPrices",
                newName: "IX_itemPrices_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itemPrices",
                table: "itemPrices",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "83b78fe6-f994-457b-a264-55c43db65bd7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "c15bcfc5-5815-47c3-a7fe-124b8420985e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "199a4a8e-18bf-4896-91bc-b335b1452233");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "e0f064cb-c9a2-4473-9017-ebddc59533ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "0c46f687-d59c-4b7e-883c-80733ff08c8b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "0b3c83c9-d366-4059-8b1d-7213a3374934");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b37b9860-e5fc-4f2e-9d59-2ac7be6e1ab3", new DateTime(2022, 7, 18, 15, 6, 54, 364, DateTimeKind.Local).AddTicks(8234), "AQAAAAEAACcQAAAAEFC4nJ+t8M+/1xxnO0xTLUOyS52Si221ARH21VAMZkKLfSuNtXWdHEAKLp+JVY78MA==", "3341b1c1-c803-433f-a2d1-edb5dfc5b6b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "736723ee-3f84-47d8-ac16-2881b535b1ff", new DateTime(2022, 7, 18, 15, 6, 54, 357, DateTimeKind.Local).AddTicks(7165), "AQAAAAEAACcQAAAAEInvCoV8OWFIIVNBsk+Q6HafVmMadyTJtt85RF7d7zwcUrSjf0Ycn4lvT8ZpKqfy1g==", "6de42754-99de-4bab-920b-abb33ab97cd1" });

            migrationBuilder.AddForeignKey(
                name: "FK_itemPrices_Items_ItemId",
                table: "itemPrices",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}