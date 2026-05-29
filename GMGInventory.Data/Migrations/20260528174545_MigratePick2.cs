using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigratePick2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pick_SalesOrders_SalesOrderId",
                table: "Pick");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pick",
                table: "Pick");

            migrationBuilder.RenameTable(
                name: "Pick",
                newName: "Picks");

            migrationBuilder.RenameIndex(
                name: "IX_Pick_SalesOrderId",
                table: "Picks",
                newName: "IX_Picks_SalesOrderId");

            migrationBuilder.AddColumn<DateTime>(
                name: "PickDate",
                table: "Picks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Picks",
                table: "Picks",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7042d1f1-2d58-4b61-8913-de1e4eb96ef4", "AQAAAAIAAYagAAAAEFHmK9iJto3aLTd1LaXgzAVladlPAYw+txch8ZvlEKzUfC537WltPyk8rGasLaZCAw==", "b3822a8a-f134-45fe-87bc-8e5aa8c93cc4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38fcd515-2ef0-4de9-a3f2-1ba4db1ccb91", "AQAAAAIAAYagAAAAEF/X4iAjgaeTK2BjtIXFa8inGAtLULh/l0+tFe3OUpj0on4mMpCFADBSGnRMGxvljQ==", "e27f5fdb-c493-4609-9ae8-96dd7101ed9f" });

            migrationBuilder.AddForeignKey(
                name: "FK_Picks_SalesOrders_SalesOrderId",
                table: "Picks",
                column: "SalesOrderId",
                principalTable: "SalesOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picks_SalesOrders_SalesOrderId",
                table: "Picks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Picks",
                table: "Picks");

            migrationBuilder.DropColumn(
                name: "PickDate",
                table: "Picks");

            migrationBuilder.RenameTable(
                name: "Picks",
                newName: "Pick");

            migrationBuilder.RenameIndex(
                name: "IX_Picks_SalesOrderId",
                table: "Pick",
                newName: "IX_Pick_SalesOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pick",
                table: "Pick",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07f15efe-532c-4b4b-b9ef-df4a4135af6d", "AQAAAAIAAYagAAAAECnj64nLxua7Xb/gHdbPx/cy+iGtIx/FHsygOTM3gFBoL894tlaVRNhDyqLZKWatXQ==", "5932af51-3115-4baa-8e34-cee4aeb78c15" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7b11e1e-1bf3-441f-9702-0e11f724f1c8", "AQAAAAIAAYagAAAAEDcCv7IxQFtavWcXokVLnQB9JhTrp4fQxfwS/zasdxcqW8WNu5pIs74Gu7byMrlEXw==", "f2f0191b-e7ff-442a-80c6-6d26a5d7fc48" });

            migrationBuilder.AddForeignKey(
                name: "FK_Pick_SalesOrders_SalesOrderId",
                table: "Pick",
                column: "SalesOrderId",
                principalTable: "SalesOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
