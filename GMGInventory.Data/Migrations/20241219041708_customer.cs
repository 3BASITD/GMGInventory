using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommodityQuantity",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "RequiredDeliveryDate",
                table: "Customers");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequiredDeliveryDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f9855e37-0127-42c0-80d2-419aceefa1c5", "AQAAAAIAAYagAAAAELoBjkFqqMuxPAV/+Ti+XRMOtfkNANL1tO7HPniXmTWAorvpLuZ15UwgC1NZHE/ryQ==", "0932d31f-d8ea-4d53-9c04-ec7000f1a76a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c5f31ee-1395-490b-b85d-8915ec1df62a", "AQAAAAIAAYagAAAAEPjZzXWu5UfR4GLf23Bq4c0II66hlv70CmDlyBMjip5ooREvkz111sb/UmYIf2+jTQ==", "4ba7b882-536f-4357-a098-3db86940d8ac" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredDeliveryDate",
                table: "SalesOrders");

            migrationBuilder.AddColumn<int>(
                name: "CommodityQuantity",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequiredDeliveryDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a6846a4-53d4-4a96-9ad3-091e679a09aa", "AQAAAAIAAYagAAAAEDC5DpkG9ANh1MBL6cz5awm5DeyAoMPe8umPbXn/yY+vflGPoCb2IqXK/zdAfzpbNA==", "01aa277b-85ee-4789-9329-fe83ae2dfed8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "018f6647-1c20-4a90-bfbf-71f849ff628c", "AQAAAAIAAYagAAAAEKL/vziXXqB1pj7tG2mD9tiq0xKVCYYMNcH5YLtWvaTmQ410V8Bt/do3esKx1fgA2A==", "13dbba94-a4e4-4271-8451-120761d4866a" });
        }
    }
}
