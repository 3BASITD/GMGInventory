using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class TransferWarehouse3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_Warehouses_WarehouseId",
                table: "TransferItemDetails");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_Warehouses_WarehouseId",
                table: "TransferItemDetails");

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
                value: "07107058-b14b-4ade-9a78-a7c64bf6cdc2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "69b1da41-3960-40d0-8b94-9365ab8a7118");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "7822e62a-d74d-4776-8365-128fb16bff13");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "da4ee70c-c4dd-4c3c-a0fb-0ac539f0102b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "f26c015a-657b-489b-b7e2-aed5370eeec3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "0d822ba8-c01d-40ac-a098-995d0ebf11e9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eba8137b-195c-4dc3-9b30-a8a2cbefd5ec", new DateTime(2022, 8, 8, 14, 41, 35, 510, DateTimeKind.Local).AddTicks(4393), "AQAAAAEAACcQAAAAEDofmzMU1Wt5cj7jUAVHpvmx4WmsDCnO6tW0F5dpALl1NMU3T87LuFJuWFqQwJfXog==", "53b31676-45e8-44c3-9026-ca4be4cd7992" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8d70671-0a46-455f-9e11-bae53e0ade63", new DateTime(2022, 8, 8, 14, 41, 35, 494, DateTimeKind.Local).AddTicks(4731), "AQAAAAEAACcQAAAAEAOxH8gaBqoVXblFAVorxbS2FMv1Xs89KRf83c5tUTtNATlXjpha8WrwigQ52vf9qQ==", "86d74844-eb3d-4ea0-9e19-ffe9949b7003" });

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemDetails_Warehouses_WarehouseId",
                table: "TransferItemDetails",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }
    }
}