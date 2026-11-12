using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PRWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "PurchaseRequests",
                type: "int",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_WarehouseId",
                table: "PurchaseRequests",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_Warehouses_WarehouseId",
                table: "PurchaseRequests",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_Warehouses_WarehouseId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_WarehouseId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "PurchaseRequests");

        }
    }
}
