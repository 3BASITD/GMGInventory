using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PurchaseOrderTypeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestTypeId",
                table: "PurchaseOrderTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c313561-5375-40cc-8ec4-665cba8b4ff0", "AQAAAAIAAYagAAAAEG2Evwy6lugPJKIAUFGJDOn34+tR9zk2ptD/CMh/JbVLrYslIqmARyJyMKVPm2cJ8A==", "0720e2e4-b040-4f5b-8620-0c9a0b34e57b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac9d3545-6ca0-4d5f-9036-b65c24b0bdef", "AQAAAAIAAYagAAAAEGIOWxw4Cn8M74nOBh1Kf3k+n00wnPlyGRJk19HOfTUOaUmdq7rupio6sWAAErnfLw==", "ce944142-55e5-42a0-b27c-343b736138de" });

            migrationBuilder.UpdateData(
                table: "PurchaseOrderTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "RequestTypeId" },
                values: new object[] { "Non-Automotive", 1 });

            migrationBuilder.UpdateData(
                table: "PurchaseOrderTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "RequestTypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "PurchaseOrderTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "RequestTypeId",
                value: 2);

            migrationBuilder.InsertData(
                table: "PurchaseOrderTypes",
                columns: new[] { "Id", "Code", "Name", "RequestTypeId" },
                values: new object[] { 4, "MR", "Maintenance & Repair", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderTypes_RequestTypeId",
                table: "PurchaseOrderTypes",
                column: "RequestTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderTypes_PurchaseRequestTypes_RequestTypeId",
                table: "PurchaseOrderTypes",
                column: "RequestTypeId",
                principalTable: "PurchaseRequestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderTypes_PurchaseRequestTypes_RequestTypeId",
                table: "PurchaseOrderTypes");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderTypes_RequestTypeId",
                table: "PurchaseOrderTypes");

            migrationBuilder.DeleteData(
                table: "PurchaseOrderTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "RequestTypeId",
                table: "PurchaseOrderTypes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4398308f-baf2-4193-b26c-db6134eeb03b", "AQAAAAIAAYagAAAAEPjF8D/bD4TARYgmgl2/MpODDy186DCbPEW8bxiTHs9jQjhv26C5Z6ld6Pt8q7bf1A==", "3a0afb00-0f8a-4469-b267-277b0fbe9190" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5f77e87-d5d1-4987-aa59-7ec53c0f2247", "AQAAAAIAAYagAAAAEKtI4rlyAE7IOxuUcfogGhOfzE2rji1QlKExIY+nIVV7Zn6qRUVy5nKvYnOKK9dvUw==", "231d80ec-47f3-4cfe-a506-645770a25594" });

            migrationBuilder.UpdateData(
                table: "PurchaseOrderTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Not applicable");
        }
    }
}
