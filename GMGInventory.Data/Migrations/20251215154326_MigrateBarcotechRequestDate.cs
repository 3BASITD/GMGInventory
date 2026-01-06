using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateBarcotechRequestDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarcotechTransferId",
                table: "TransferItemDetails");

            migrationBuilder.AddColumn<string>(
                name: "BarcotechTransferCode",
                table: "TransferItemDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BarcotechRequestDate",
                table: "SalesReturns",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BarcotechRequestDate",
                table: "PurchaseReturnNews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BarcotechRequestDate",
                table: "PurchaseOrderNews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BarcotechRequestDate",
                table: "Packs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a8a0167-fb0e-4471-8809-58413a8d3b11", "AQAAAAIAAYagAAAAEEjmBubKJcYyNBr9PoobN3+T9coblaWtIGk0y99wRthtMrtmSuahjn8eXabs5+qREg==", "a6ad6735-6b59-43f8-923d-dc2df8d39a7a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da39f335-dccf-4e08-b28d-9b4867a0a7c3", "AQAAAAIAAYagAAAAEDQwHWIF1/ZPaY9f6tUdsNFMnFMl0TP0SO+Nd+56wXnaJxmof9TEIylp40+9LETDmA==", "01ab234a-93b3-43c4-9558-b44766fe2d36" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarcotechTransferCode",
                table: "TransferItemDetails");

            migrationBuilder.DropColumn(
                name: "BarcotechRequestDate",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "BarcotechRequestDate",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "BarcotechRequestDate",
                table: "PurchaseOrderNews");

            migrationBuilder.DropColumn(
                name: "BarcotechRequestDate",
                table: "Packs");

            migrationBuilder.AddColumn<int>(
                name: "BarcotechTransferId",
                table: "TransferItemDetails",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c50e84f6-7601-4ad9-91a3-8b33283b8001", "AQAAAAIAAYagAAAAEHW08H6tb8raJggyubzxDmzwtuTTvP4DuWUe6Td4GdhCtqRTRK7CLpQi1yLdJm9S9Q==", "1d9d2f7b-bcdd-4da0-87ec-e45bcc82eec8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5bbc4d7-515e-4706-9500-d0a748879992", "AQAAAAIAAYagAAAAEER5hAWTvc5XAdJP/xL0AC0jSiONikDyUoVFspFvDVu6NgnwOk0tDvpNOGcEAXDCKA==", "afe83b0a-3b1b-43f6-8ddd-808a64cf77b4" });
        }
    }
}
