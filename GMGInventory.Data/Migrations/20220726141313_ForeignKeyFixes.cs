using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ForeignKeyFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPayments_SOACRs_SOAId",
                table: "CustomerPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnAppliedAmounts_SalesReturnTotalReturnAmounts_TotalReturnId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRLineItems_SOACRs_SOAId",
                table: "SOACRLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Warehouses_ToWarehouseId",
                table: "TransferItems");

            migrationBuilder.DropIndex(
                name: "IX_TransferItems_ToWarehouseId",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "ToWarehouseId",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "StockCards");

            migrationBuilder.DropColumn(
                name: "Forwarder",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "SOAId",
                table: "SOACRLineItems",
                newName: "SOACRId");

            migrationBuilder.RenameIndex(
                name: "IX_SOACRLineItems_SOAId",
                table: "SOACRLineItems",
                newName: "IX_SOACRLineItems_SOACRId");

            migrationBuilder.RenameColumn(
                name: "TotalReturnId",
                table: "SalesReturnAppliedAmounts",
                newName: "SalesReturnTotalReturnAmountId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesReturnAppliedAmounts_TotalReturnId",
                table: "SalesReturnAppliedAmounts",
                newName: "IX_SalesReturnAppliedAmounts_SalesReturnTotalReturnAmountId");

            migrationBuilder.RenameColumn(
                name: "SOAId",
                table: "CustomerPayments",
                newName: "SOACRId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPayments_SOAId",
                table: "CustomerPayments",
                newName: "IX_CustomerPayments_SOACRId");

            migrationBuilder.AddColumn<int>(
                name: "ToWarehouseId",
                table: "TransferItemDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "StockCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesOrderDeliveryMonitoringId",
                table: "SalesReturnAppliedAmounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ForwarderId",
                table: "SalesOrderDeliveryMonitorings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "ac33e2bf-86e3-41fd-b3a6-38e97ab98546");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "b00d7f57-2e4e-4df8-b18d-a2ea8a45e6c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "baed9032-8843-4285-a8d0-ac901c694d36");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "3f0d8597-62b7-4650-b9a0-8fbfe27d25c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "3d51fee5-8f28-4f69-9524-2483bc23feba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "171f13c6-8aba-482e-ad27-aec78d4c9238");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75a22d95-7339-45a3-a99f-0c95fe8db61a", new DateTime(2022, 7, 26, 22, 13, 12, 845, DateTimeKind.Local).AddTicks(8507), "AQAAAAEAACcQAAAAEED7RzsYQVLeSA1l4GcomLWfPCL+32O132hcZ045SWlHlnXJYnxFVBOlshG/Or27jA==", "ce760775-1d09-42d3-b908-0dbbe716b551" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b17d4a4-c1c4-4135-a040-2811c0c62a54", new DateTime(2022, 7, 26, 22, 13, 12, 838, DateTimeKind.Local).AddTicks(2320), "AQAAAAEAACcQAAAAEKR6GuwBYnkFqgPXjuHQzok+m0MCRMPoctVoc3zvxcPic0x4wkOk7g2IWcbiqSqIEg==", "ff88f98d-5190-4313-903b-2a89dcca914d" });

            migrationBuilder.CreateIndex(
                name: "IX_TransferItemDetails_ToWarehouseId",
                table: "TransferItemDetails",
                column: "ToWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCards_ItemId",
                table: "StockCards",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnAppliedAmounts_SalesOrderDeliveryMonitoringId",
                table: "SalesReturnAppliedAmounts",
                column: "SalesOrderDeliveryMonitoringId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveryMonitorings_ForwarderId",
                table: "SalesOrderDeliveryMonitorings",
                column: "ForwarderId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnitId",
                table: "Items",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPayments_SOACRs_SOACRId",
                table: "CustomerPayments",
                column: "SOACRId",
                principalTable: "SOACRs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_UnitId",
                table: "Items",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_Forwarders_ForwarderId",
                table: "SalesOrderDeliveryMonitorings",
                column: "ForwarderId",
                principalTable: "Forwarders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnAppliedAmounts_SalesOrderDeliveryMonitorings_SalesOrderDeliveryMonitoringId",
                table: "SalesReturnAppliedAmounts",
                column: "SalesOrderDeliveryMonitoringId",
                principalTable: "SalesOrderDeliveryMonitorings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnAppliedAmounts_SalesReturnTotalReturnAmounts_SalesReturnTotalReturnAmountId",
                table: "SalesReturnAppliedAmounts",
                column: "SalesReturnTotalReturnAmountId",
                principalTable: "SalesReturnTotalReturnAmounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRLineItems_SOACRs_SOACRId",
                table: "SOACRLineItems",
                column: "SOACRId",
                principalTable: "SOACRs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockCards_Items_ItemId",
                table: "StockCards",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemDetails_Warehouses_ToWarehouseId",
                table: "TransferItemDetails",
                column: "ToWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPayments_SOACRs_SOACRId",
                table: "CustomerPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_Forwarders_ForwarderId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnAppliedAmounts_SalesOrderDeliveryMonitorings_SalesOrderDeliveryMonitoringId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnAppliedAmounts_SalesReturnTotalReturnAmounts_SalesReturnTotalReturnAmountId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRLineItems_SOACRs_SOACRId",
                table: "SOACRLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StockCards_Items_ItemId",
                table: "StockCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_Warehouses_ToWarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransferItemDetails_ToWarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.DropIndex(
                name: "IX_StockCards_ItemId",
                table: "StockCards");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturnAppliedAmounts_SalesOrderDeliveryMonitoringId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderDeliveryMonitorings_ForwarderId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropIndex(
                name: "IX_Items_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ToWarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "StockCards");

            migrationBuilder.DropColumn(
                name: "SalesOrderDeliveryMonitoringId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropColumn(
                name: "ForwarderId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "SOACRId",
                table: "SOACRLineItems",
                newName: "SOAId");

            migrationBuilder.RenameIndex(
                name: "IX_SOACRLineItems_SOACRId",
                table: "SOACRLineItems",
                newName: "IX_SOACRLineItems_SOAId");

            migrationBuilder.RenameColumn(
                name: "SalesReturnTotalReturnAmountId",
                table: "SalesReturnAppliedAmounts",
                newName: "TotalReturnId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesReturnAppliedAmounts_SalesReturnTotalReturnAmountId",
                table: "SalesReturnAppliedAmounts",
                newName: "IX_SalesReturnAppliedAmounts_TotalReturnId");

            migrationBuilder.RenameColumn(
                name: "SOACRId",
                table: "CustomerPayments",
                newName: "SOAId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerPayments_SOACRId",
                table: "CustomerPayments",
                newName: "IX_CustomerPayments_SOAId");

            migrationBuilder.AddColumn<int>(
                name: "ToWarehouseId",
                table: "TransferItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "StockCards",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Forwarder",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "Items",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "8c703056-9f7d-4d07-b230-b2876dc0ed69");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "8e29409b-f4eb-449d-a9b9-81f56e744eb1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "53f3cb45-ef4e-4fd3-a7c9-1b940e696232");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "5d695153-4a28-4893-9043-949ba2de20cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "e42b2333-1ba2-42f6-b104-53267f63b3c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "55f71592-2b44-4b18-a87c-1bf74f1f3423");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "274033fb-6b4b-4060-be84-80022ab854c8", new DateTime(2022, 7, 25, 4, 59, 38, 121, DateTimeKind.Local).AddTicks(488), "AQAAAAEAACcQAAAAEO6LhLl/TIcePi82h9b+0RJuRmb5uryQw2RHhLX9fNHp55SK2hdMBgAJ2UOvIjEjNQ==", "84c21440-e491-402a-89e0-12e78aa006e8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "994272f9-24c0-40ab-9c91-5db2f62445bf", new DateTime(2022, 7, 25, 4, 59, 38, 113, DateTimeKind.Local).AddTicks(7086), "AQAAAAEAACcQAAAAEJctmZQ3cDiA9jwlo+4VwvXfrhUkiNRuzv3LkGENjJ8+k3jNV/CsVghGEDbZSM2+Hg==", "7bedf75d-017f-40a0-a9c4-f9c84845dd89" });

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_ToWarehouseId",
                table: "TransferItems",
                column: "ToWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPayments_SOACRs_SOAId",
                table: "CustomerPayments",
                column: "SOAId",
                principalTable: "SOACRs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnAppliedAmounts_SalesReturnTotalReturnAmounts_TotalReturnId",
                table: "SalesReturnAppliedAmounts",
                column: "TotalReturnId",
                principalTable: "SalesReturnTotalReturnAmounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRLineItems_SOACRs_SOAId",
                table: "SOACRLineItems",
                column: "SOAId",
                principalTable: "SOACRs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Warehouses_ToWarehouseId",
                table: "TransferItems",
                column: "ToWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}