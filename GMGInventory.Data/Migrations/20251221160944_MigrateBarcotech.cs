using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateBarcotech : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarcotechRequestDate",
                table: "SalesReturns");

            migrationBuilder.RenameColumn(
                name: "BarcotechTransferCode",
                table: "TransferItemDetails",
                newName: "BarcotechTransferId");

            migrationBuilder.AddColumn<string>(
                name: "BarcotechTransferCode",
                table: "TransferItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BarcotechReturnCode",
                table: "SalesReturns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BarcotechReturnId",
                table: "SalesReturnLineItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerBrandDiscountId",
                table: "SalesReturnLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemPromoId",
                table: "SalesReturnLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BarcotechOutboundCode",
                table: "SalesOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BarcotechOutboundId",
                table: "SalesOrderLineItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BarcotechReceivingCode",
                table: "PurchaseReceivings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BarcotechReceivingId",
                table: "PurchaseReceivingLineItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2812ce34-1e80-41b5-86f8-b8838401d44b", "AQAAAAIAAYagAAAAEOv7LnIv0k/Ga9hOlCsDEX0pAtfBPbvtPbCzxrSbAIQwIw6D0x2E5LMoJWbjMjh7Tg==", "462e6548-a7a4-48b1-846c-f382017c1239" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8425104e-ee1c-47f4-a697-e2839073cb36", "AQAAAAIAAYagAAAAEM9lae2XolCEk5D4ilOYIKNrZ1HqGAtc6r4s93KYgXyPCOZS0QmLHE9jEP5nrrwtpQ==", "0cdf4158-9236-4f6b-a69c-bdb52af0023a" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLineItems_CustomerBrandDiscountId",
                table: "SalesReturnLineItems",
                column: "CustomerBrandDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLineItems_ItemPromoId",
                table: "SalesReturnLineItems",
                column: "ItemPromoId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnLineItems_CustomerBrandDiscount_CustomerBrandDiscountId",
                table: "SalesReturnLineItems",
                column: "CustomerBrandDiscountId",
                principalTable: "CustomerBrandDiscount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnLineItems_ItemPromo_ItemPromoId",
                table: "SalesReturnLineItems",
                column: "ItemPromoId",
                principalTable: "ItemPromo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_CustomerBrandDiscount_CustomerBrandDiscountId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_ItemPromo_ItemPromoId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturnLineItems_CustomerBrandDiscountId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturnLineItems_ItemPromoId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropColumn(
                name: "BarcotechTransferCode",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "BarcotechReturnCode",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "CustomerBrandDiscountId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropColumn(
                name: "ItemPromoId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropColumn(
                name: "BarcotechOutboundCode",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "BarcotechReceivingCode",
                table: "PurchaseReceivings");

            migrationBuilder.RenameColumn(
                name: "BarcotechTransferId",
                table: "TransferItemDetails",
                newName: "BarcotechTransferCode");

            migrationBuilder.AddColumn<DateTime>(
                name: "BarcotechRequestDate",
                table: "SalesReturns",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BarcotechReturnId",
                table: "SalesReturnLineItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BarcotechOutboundId",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BarcotechReceivingId",
                table: "PurchaseReceivingLineItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2389fc2-272b-4ea0-9f29-b4d50edf6136", "AQAAAAIAAYagAAAAEAsyue1r6286Z5H/KBjqcZUfnRkbOuD0c1gzUUQZcIMx+TraqoSIx8NhJFLTP3Rpyw==", "f28e29fa-ff5b-4089-aded-c00a52eda1c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e661ddd-9cf4-4cb6-9032-1af3554ebee5", "AQAAAAIAAYagAAAAEG+xHZ7p8AmHpFxXPfr1cxv7IGw9/Ef3mSU6T1/azzB2TEspM85gd4bKvjiigvSorA==", "77c45926-1ed9-4190-b9d1-6e2cb0923c78" });
        }
    }
}
