using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ProcurementNonNullableItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNewLineItems_PurchaseRequestLineItems_RequestItemId",
                table: "PurchaseOrderNewLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivingLineItems_PurchaseOrderNewLineItems_OrderItemId",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNewLineItems_PurchaseReceivingLineItems_ReceivingItemId",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.AlterColumn<int>(
                name: "ReceivingItemId",
                table: "PurchaseReturnNewLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderItemId",
                table: "PurchaseReceivingLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RequestItemId",
                table: "PurchaseOrderNewLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af683b08-9a31-44b6-9dc8-4b52ccf7f231", "AQAAAAIAAYagAAAAEMWrEsvNRUkWwR5+DFaNxUxYPo8jEiPpJVin3RW5664kRrpF0fZ8MmPf6iTRwPCMIQ==", "3dc3e101-3169-4e6a-83cc-9c30d48c3be0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "015a71ef-6d96-45f9-928e-38a6a69d592b", "AQAAAAIAAYagAAAAED1T6eQXAIZxSU/SVeyUbs9gdS1r5pobVin2t5qGEXhpk/V18uqUCl9WFFR2hQeLJA==", "e2cad196-3359-4a3e-b913-5736d1286639" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNewLineItems_PurchaseRequestLineItems_RequestItemId",
                table: "PurchaseOrderNewLineItems",
                column: "RequestItemId",
                principalTable: "PurchaseRequestLineItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivingLineItems_PurchaseOrderNewLineItems_OrderItemId",
                table: "PurchaseReceivingLineItems",
                column: "OrderItemId",
                principalTable: "PurchaseOrderNewLineItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNewLineItems_PurchaseReceivingLineItems_ReceivingItemId",
                table: "PurchaseReturnNewLineItems",
                column: "ReceivingItemId",
                principalTable: "PurchaseReceivingLineItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNewLineItems_PurchaseRequestLineItems_RequestItemId",
                table: "PurchaseOrderNewLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivingLineItems_PurchaseOrderNewLineItems_OrderItemId",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNewLineItems_PurchaseReceivingLineItems_ReceivingItemId",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.AlterColumn<int>(
                name: "ReceivingItemId",
                table: "PurchaseReturnNewLineItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrderItemId",
                table: "PurchaseReceivingLineItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RequestItemId",
                table: "PurchaseOrderNewLineItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1419de0-5cff-4b24-9475-5b4a72204c2e", "AQAAAAIAAYagAAAAEKX0WS1IlygTrCZZVw0C6G28+SNauuI2kgzghbnuT0OOc9AA3fkfxYnnyL8uXZ/6Zg==", "35c895ab-8296-4588-b95f-5e62c13d3436" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "455de60d-2f0d-4df6-b2e9-1f1d0accefae", "AQAAAAIAAYagAAAAENqKH24Q0Q8/bRmP0dk8xj8c5UHR0rqe0a3R3KE0iOp7mWTQYzaA2BG8PzRo2zqkrg==", "aaaa160d-2f7b-4ffc-bdd1-6860c4b34712" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNewLineItems_PurchaseRequestLineItems_RequestItemId",
                table: "PurchaseOrderNewLineItems",
                column: "RequestItemId",
                principalTable: "PurchaseRequestLineItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivingLineItems_PurchaseOrderNewLineItems_OrderItemId",
                table: "PurchaseReceivingLineItems",
                column: "OrderItemId",
                principalTable: "PurchaseOrderNewLineItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNewLineItems_PurchaseReceivingLineItems_ReceivingItemId",
                table: "PurchaseReturnNewLineItems",
                column: "ReceivingItemId",
                principalTable: "PurchaseReceivingLineItems",
                principalColumn: "Id");
        }
    }
}
