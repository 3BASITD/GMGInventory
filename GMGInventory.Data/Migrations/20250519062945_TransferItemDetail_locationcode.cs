using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class TransferItemDetail_locationcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationCodeId",
                table: "TransferItemDetails",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd266446-e7c6-47d5-865e-89512be376db", "AQAAAAIAAYagAAAAECZUNtRzba6KYEshDJtBhzTYzCQVfJA8VthLfIYSvXHOWaHmSmGy05P/+XZCJOYaqQ==", "3899afaa-32e4-4691-b798-a74690b0c40f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7be083fc-0392-44bb-9ecb-6f0bfb403f8d", "AQAAAAIAAYagAAAAEFjW5yK+ZzQ8avYwbGTTZaqUrl1fE/an+yrJqpu8bCFnCprZbpXF4rY0tQIP49VV5A==", "4801088c-be59-44f3-b50c-daaf4a05038c" });

            migrationBuilder.CreateIndex(
                name: "IX_TransferItemDetails_LocationCodeId",
                table: "TransferItemDetails",
                column: "LocationCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemDetails_LocationCodes_LocationCodeId",
                table: "TransferItemDetails",
                column: "LocationCodeId",
                principalTable: "LocationCodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_LocationCodes_LocationCodeId",
                table: "TransferItemDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransferItemDetails_LocationCodeId",
                table: "TransferItemDetails");

            migrationBuilder.DropColumn(
                name: "LocationCodeId",
                table: "TransferItemDetails");

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
    }
}
