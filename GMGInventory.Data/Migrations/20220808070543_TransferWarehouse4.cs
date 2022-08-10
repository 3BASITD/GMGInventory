using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class TransferWarehouse4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_Warehouses_WarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Warehouses_WarehouseId",
                table: "TransferItems");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "TransferItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "TransferItemDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_Warehouses_WarehouseId",
                table: "TransferItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_Warehouses_WarehouseId",
                table: "TransferItems");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "TransferItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "TransferItemDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "f9dbf797-f57e-4066-9644-f12fa7d36fcb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "14326437-8ff9-4481-bba6-6c6cceecad5a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "04422cce-6a81-4376-805e-627ad0116e4f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "26059b80-868e-4c7c-a63e-6809fac1d5d8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "339f0343-0c53-4a88-bc83-e04f04180db4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "6477e554-7a20-4080-88af-cb07d55fb172");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "239e4d56-c3c8-4669-adc1-0d7c6504e1e0", new DateTime(2022, 8, 8, 15, 4, 28, 896, DateTimeKind.Local).AddTicks(9678), "AQAAAAEAACcQAAAAEDSgmXsFSHZyPluZ3GM07cEueb1uLe8AJcmQpb2URjk8WeWUB4EtohvPLGaV9hkmnw==", "c99d0ef1-0e9d-443d-9da6-95c7f435e1f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6615cac6-e2d0-41fb-8621-d5d0d1d2f191", new DateTime(2022, 8, 8, 15, 4, 28, 887, DateTimeKind.Local).AddTicks(4104), "AQAAAAEAACcQAAAAEDEIYc3nhXZbnpFrKd34l4Wdw3e7IRUbzp8npbDN8Kba6IdW/YPhuUZgLxu/2nWlaA==", "167eae0d-e69d-462d-b6dd-9a4ed49849a5" });

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemDetails_Warehouses_WarehouseId",
                table: "TransferItemDetails",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Warehouses_WarehouseId",
                table: "TransferItems",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
