using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateBarcotechReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BarcotechTransferId",
                table: "TransferItemDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BarcotechReturnId",
                table: "SalesReturnLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BarcotechOutboundId",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BarcotechReceivingId",
                table: "PurchaseReceivingLineItems",
                type: "int",
                nullable: true);

            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[] { 81, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 81, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "c50e84f6-7601-4ad9-91a3-8b33283b8001", "AQAAAAIAAYagAAAAEHW08H6tb8raJggyubzxDmzwtuTTvP4DuWUe6Td4GdhCtqRTRK7CLpQi1yLdJm9S9Q==", "1d9d2f7b-bcdd-4da0-87ec-e45bcc82eec8" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e5bbc4d7-515e-4706-9500-d0a748879992", "AQAAAAIAAYagAAAAEER5hAWTvc5XAdJP/xL0AC0jSiONikDyUoVFspFvDVu6NgnwOk0tDvpNOGcEAXDCKA==", "afe83b0a-3b1b-43f6-8ddd-808a64cf77b4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Accesses",
            //    keyColumn: "Id",
            //    keyValue: 81);

            migrationBuilder.DropColumn(
                name: "BarcotechTransferId",
                table: "TransferItemDetails");

            migrationBuilder.DropColumn(
                name: "BarcotechReturnId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropColumn(
                name: "BarcotechOutboundId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "BarcotechReceivingId",
                table: "PurchaseReceivingLineItems");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "a5d0eaa1-35e8-4ca4-8c17-87f6760bc268", "AQAAAAIAAYagAAAAEB8VlxlBk26AmjpFKZwKCnY6rQAXgWy9lXgJHG64QQ4Uegy8+5ZDbVUKq4hGQon0kQ==", "75b6a71c-7640-49f7-b904-c1bcf5c8e455" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "28ca393c-334f-477c-8ca1-ecf75cce58cf", "AQAAAAIAAYagAAAAECekghKPAWVFOrmoRBKh9ssgX1Wv982peQJxAXhlehNxT9nDUf+CibmxXohaSvf7Qw==", "fd91b2aa-67eb-48e8-86c7-d361604fd632" });
        }
    }
}
