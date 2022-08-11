using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class TransferWarehouse5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_Warehouses_WarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Warehouses_WarehouseId",
                table: "TransferItems");

            migrationBuilder.DropIndex(
                name: "IX_TransferItems_WarehouseId",
                table: "TransferItems");

            migrationBuilder.DropIndex(
                name: "IX_TransferItemDetails_WarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "dcd11527-26ca-4ee2-a95a-fbf5a29d7cc7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "8c0dedea-f9f1-4f64-b9ea-56a7f16e14ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "5bae5431-3ce4-489a-be70-e2c89ca17357");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "068d7fa0-3b7e-499d-ac2f-330969ad5a03");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "327a51ab-429b-46f9-b617-83582c6c1b5b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "9bb9acac-442f-4ac5-9add-8473d6f15f09");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93ebd2df-db39-4821-ae2f-ea431806d1fc", new DateTime(2022, 8, 8, 15, 8, 5, 526, DateTimeKind.Local).AddTicks(8461), "AQAAAAEAACcQAAAAEPivHSmRm1e6mwZyC5/UuXb6WwGdG/xnt5GRrYwdcHeC6McgANgs/TC7MU/XTBVWwg==", "e2589246-7fdd-46ae-9105-32ac81897698" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67d3d82d-cda9-4314-8838-729ca3171c14", new DateTime(2022, 8, 8, 15, 8, 5, 518, DateTimeKind.Local).AddTicks(614), "AQAAAAEAACcQAAAAEDVoTy7rPxNxltNlAkHVkMd28thPOSRC2GA7aUa4vDDpOTUXNhlfZoQtjvdE+n1E8A==", "b8b95b33-a66f-4fd0-8e3a-fdd5264b105a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                value: "bb86966d-700d-49cb-88b1-a5e1e64ba950");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "b00c0808-b496-4853-8f5c-06a879c4fa1b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "e859b5e1-d8c2-415f-aa83-77a5b6c929ed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "d71e5485-33b0-4aa5-809a-089e13188e99");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "cfcb757e-7bc5-4824-9a8c-49dbdc612b08");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "ff061dc0-b206-4213-87d8-b2436a7f8ce1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4384092-3bed-4f46-94f6-d9b7a10788fa", new DateTime(2022, 8, 8, 15, 5, 42, 321, DateTimeKind.Local).AddTicks(8246), "AQAAAAEAACcQAAAAEEJjKUlYERaEzWFrdT3t6jg5cy+4LzYrKhda4dKS1ldhasZzB87fQyhOOKBIosXT5Q==", "f2fcb83f-32cd-4d35-bf3e-a8806e14d10f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9ce159a-ff97-4876-815d-f2565ba08339", new DateTime(2022, 8, 8, 15, 5, 42, 313, DateTimeKind.Local).AddTicks(8444), "AQAAAAEAACcQAAAAELD32rm2ow61Z0ykbud9cHijYXdSxZQO06t3i0RM7kFQVdOemgOak4ttgro8T0U0mQ==", "2e1d1b7f-1d5b-4aa1-bf21-41d74a2b95e5" });

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_WarehouseId",
                table: "TransferItems",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItemDetails_WarehouseId",
                table: "TransferItemDetails",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemDetails_Warehouses_WarehouseId",
                table: "TransferItemDetails",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Warehouses_WarehouseId",
                table: "TransferItems",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }
    }
}
