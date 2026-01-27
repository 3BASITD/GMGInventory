using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateProcurementWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_Warehouses_WarehouseId",
                table: "PurchaseReturnNews");

            //migrationBuilder.DeleteData(
            //    table: "Accesses",
            //    keyColumn: "Id",
            //    keyValue: 81);

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: false,
                defaultValue: 2,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "705a35ba-a992-4f3d-a6cd-3356d696ed2e", "AQAAAAIAAYagAAAAEL3pm8GzBxo92jQ1Ncdvk28xhevUWz1QzUPviunTRJbL3g7S2qd2tbfvZ31GdTLoXw==", "06abd314-267b-4ac8-92ab-f0cc8d727071" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "168f2c81-2f42-4a2f-8ab3-70e7304d6ba4", "AQAAAAIAAYagAAAAEMTAz2D5KRFccYk5z1PFlLMeis5BZLObwRlh2G9SixJ1cHvbFnVfOdfgMd8mzKwtRA==", "454faad8-68f7-465d-8f68-8e1c172c2294" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_Warehouses_WarehouseId",
                table: "PurchaseReturnNews",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_Warehouses_WarehouseId",
                table: "PurchaseReturnNews");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Accesses",
                columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
                values: new object[] { 81, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 81, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null });

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

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_Warehouses_WarehouseId",
                table: "PurchaseReturnNews",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }
    }
}
