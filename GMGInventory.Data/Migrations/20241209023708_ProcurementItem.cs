using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ProcurementItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNewLineItems_Items_ItemId",
                table: "PurchaseOrderNewLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivingLineItems_Items_ItemId",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNewLineItems_Items_ItemId",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnNewLineItems_ItemId",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivingLineItems_ItemId",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderNewLineItems_ItemId",
                table: "PurchaseOrderNewLineItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "PurchaseOrderNewLineItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e7f56b2-6f5f-4e01-a08d-403304ff078a", "AQAAAAIAAYagAAAAENdxSYsgHJzBRev8cmzS5Q9HcrHOWYZCH/eaXsjPhAeeBvpeXutns85yxhRa1Ldxpg==", "153fabec-f818-4f45-ad6b-1686d60b1f12" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a3a068f-f1e7-44cf-bf74-1f595e55ea07", "AQAAAAIAAYagAAAAENv1ZIvmVUofyr71wRQpnUthXg9iIE251GOMMr3Q3xTBYmvfTvJguUz5pxBEf47Vvw==", "3c71fc65-84fc-45b8-83c5-9c18285d0eb0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "PurchaseReturnNewLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "PurchaseReceivingLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "PurchaseOrderNewLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02c6ad64-5aa8-4110-8d05-9f859437e0b8", "AQAAAAIAAYagAAAAEIxNudJwdxheutc4JN9KfMSpGSt8hhuEygRhiXxDVlq5q7HamL+O/iOYg/qDOUslqQ==", "25d1f9a3-a646-4b1c-a8f9-c17892d9dfb3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0168a97-f8b9-407a-9f4c-ce0de55f1732", "AQAAAAIAAYagAAAAECK0GcOeThN2oM6TpnnU3+9xCJpbBMbp2NYO3wy6KMO8vwNm5PcqhXDYWPmXfBV71Q==", "177d6ff9-ba7d-45dd-ab36-bdd08fca958a" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNewLineItems_ItemId",
                table: "PurchaseReturnNewLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivingLineItems_ItemId",
                table: "PurchaseReceivingLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNewLineItems_ItemId",
                table: "PurchaseOrderNewLineItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNewLineItems_Items_ItemId",
                table: "PurchaseOrderNewLineItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivingLineItems_Items_ItemId",
                table: "PurchaseReceivingLineItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNewLineItems_Items_ItemId",
                table: "PurchaseReturnNewLineItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
