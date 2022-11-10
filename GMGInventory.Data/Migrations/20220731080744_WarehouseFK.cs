using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class WarehouseFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Warehouses_ToWarehouseId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_Warehouses_ToWarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Warehouses_FromWarehouseId",
                table: "TransferItems");

            migrationBuilder.DropIndex(
                name: "IX_TransferItemDetails_ToWarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.DropColumn(
                name: "ToWarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.RenameColumn(
                name: "FromWarehouseId",
                table: "TransferItems",
                newName: "SourceId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferItems_FromWarehouseId",
                table: "TransferItems",
                newName: "IX_TransferItems_SourceId");

            migrationBuilder.RenameColumn(
                name: "ToWarehouseId",
                table: "PurchaseOrders",
                newName: "DestinationId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrders_ToWarehouseId",
                table: "PurchaseOrders",
                newName: "IX_PurchaseOrders_DestinationId");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "TransferItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "TransferItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "TransferItemDetails",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "cff12848-0200-4147-bd80-151ee526d321");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "a7ec9c03-4c1f-4f72-953b-dd1e68168e0b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "204903fb-a41e-4d64-8c5f-e3ff8f5f0605");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "f23c75db-4418-478a-a5cf-ca1b6b1de998");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "8b3063d5-99a6-4cfd-ae76-dba73d25241b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "a61aed0b-9891-4831-b597-e473047740e0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6a62cce-a65c-4ecf-8adf-d031f79e320a", new DateTime(2022, 7, 31, 16, 7, 43, 901, DateTimeKind.Local).AddTicks(7099), "AQAAAAEAACcQAAAAEDHsbemGjRTW/2gURYY5UwiU3wM9lDqDK/ruDv8VGagTeVgtfGMojBemzapCFZRxow==", "6b7c07c5-103c-4647-b877-bc3d8dcbe7ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b0c8844-066e-419d-aa7a-b722d8327b03", new DateTime(2022, 7, 31, 16, 7, 43, 894, DateTimeKind.Local).AddTicks(5469), "AQAAAAEAACcQAAAAEIlS/JEW0EsG8Qfbmt4qRrKEo+C9+FqoF8Rgk9Zplay5AArgFWJO/ZJOTwEG+OKhjg==", "5a899ac2-7d94-4c8b-b93e-fbee239674eb" });

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_DestinationId",
                table: "TransferItems",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_WarehouseId",
                table: "TransferItems",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItemDetails_WarehouseId",
                table: "TransferItemDetails",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Warehouses_DestinationId",
                table: "PurchaseOrders",
                column: "DestinationId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemDetails_Warehouses_WarehouseId",
                table: "TransferItemDetails",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Warehouses_DestinationId",
                table: "TransferItems",
                column: "DestinationId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Warehouses_SourceId",
                table: "TransferItems",
                column: "SourceId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Warehouses_WarehouseId",
                table: "TransferItems",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Warehouses_DestinationId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_Warehouses_WarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Warehouses_DestinationId",
                table: "TransferItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Warehouses_SourceId",
                table: "TransferItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Warehouses_WarehouseId",
                table: "TransferItems");

            migrationBuilder.DropIndex(
                name: "IX_TransferItems_DestinationId",
                table: "TransferItems");

            migrationBuilder.DropIndex(
                name: "IX_TransferItems_WarehouseId",
                table: "TransferItems");

            migrationBuilder.DropIndex(
                name: "IX_TransferItemDetails_WarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "TransferItems",
                newName: "FromWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferItems_SourceId",
                table: "TransferItems",
                newName: "IX_TransferItems_FromWarehouseId");

            migrationBuilder.RenameColumn(
                name: "DestinationId",
                table: "PurchaseOrders",
                newName: "ToWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrders_DestinationId",
                table: "PurchaseOrders",
                newName: "IX_PurchaseOrders_ToWarehouseId");

            migrationBuilder.AddColumn<int>(
                name: "ToWarehouseId",
                table: "TransferItemDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "55f270b4-4118-49cf-a6b5-aca55e6edbe6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "ad668d30-298e-4f65-b672-28aee2fba64a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "27ec3132-7299-46c8-b7f3-8c75fb60d6c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "0059da36-42fa-485f-81c8-e2cc7e9456e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "022ea87e-d58a-47f6-89bc-054dacfb8074");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "5f2823b4-2c31-41e7-a0d9-43dcac26bde4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a68e52f9-0bfd-484e-9eeb-583f06b4e478", new DateTime(2022, 7, 29, 15, 1, 5, 582, DateTimeKind.Local).AddTicks(4877), "AQAAAAEAACcQAAAAEMLGh21uda7ccr5QD78FQ1grCBvqVlgT+YW0wh9Xo3eSINgZZil+IRsS55jHkseojA==", "b62eb847-2a77-4781-a5c9-ad77f99b4ae5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "808a0bde-9500-418e-87d1-0c65793fd685", new DateTime(2022, 7, 29, 15, 1, 5, 573, DateTimeKind.Local).AddTicks(8975), "AQAAAAEAACcQAAAAEOlHWZJWiNaxRZfIOFKLeNssRA/lsx1ZOT7w0pAK3oi6MxXUzAprFzPNnv5vlh4dCw==", "3545c44f-59dd-46ff-a9b6-9e8bf4ad476c" });

            migrationBuilder.CreateIndex(
                name: "IX_TransferItemDetails_ToWarehouseId",
                table: "TransferItemDetails",
                column: "ToWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Warehouses_ToWarehouseId",
                table: "PurchaseOrders",
                column: "ToWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemDetails_Warehouses_ToWarehouseId",
                table: "TransferItemDetails",
                column: "ToWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Warehouses_FromWarehouseId",
                table: "TransferItems",
                column: "FromWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}