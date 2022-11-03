using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class TransferWarehouse2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_TransferItems_Warehouses_WarehouseId",
                table: "TransferItems",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "8b0ff6a1-98f7-4003-ab07-dd491eed350b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "24070e9d-0a79-4156-aa28-838112315894");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "faf0b48b-01d1-40b9-936b-660ac080a292");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "e9084a37-84eb-46fa-ae83-4f7064fb6cd0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "5af6f1d3-56a7-4f6e-9254-3d00bc1c786a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "5aceed09-5d9d-4d7e-98eb-06dbb3122d5d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a124545c-068c-498f-b60c-cf3bddd707c4", new DateTime(2022, 8, 8, 14, 39, 31, 443, DateTimeKind.Local).AddTicks(7952), "AQAAAAEAACcQAAAAEJRygvI5JjEPtGmRnpTkvB4g5zz8wNKmXhDbmvfnsbBFO53t/ya+yTVfmnqMV4BS1A==", "439f9e64-99dc-40f9-8b22-1b52d55ddb53" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "992cf948-9696-47f7-beb2-0906dceaa7c4", new DateTime(2022, 8, 8, 14, 39, 31, 431, DateTimeKind.Local).AddTicks(7097), "AQAAAAEAACcQAAAAELesioYmbCzCAfCY/pm/Za8q9BboNT2X/izLm9rFInpvY7sUR4XowIWjVAZkOI/s+g==", "9dcba104-b70f-4c22-8b71-88b1a994dd89" });

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_Warehouses_WarehouseId",
                table: "TransferItems",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }
    }
}