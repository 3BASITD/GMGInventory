using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class WarehouseTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "Warehouses",
                type: "nvarchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "Warehouses",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "098b7d06-c9f5-4ec1-9180-14fb691816b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "e80f083d-4950-4884-9c08-fce4510bab15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "a5e73684-0a99-4b0f-b060-38238d1e0f77");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "9752b956-a42d-4c6c-954f-9349082d3c1d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "e7605d0f-46da-438b-9808-4fe61d910989");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "da7d1fc8-06f7-4250-9ee2-ee8ec308ecef");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10650e13-13ef-46ea-86e9-e3431f1305e7", new DateTime(2022, 7, 24, 17, 41, 58, 771, DateTimeKind.Local).AddTicks(5553), "AQAAAAEAACcQAAAAEFxaawZynEWgBJFio0CPlobEwGxTJdVT/fgEeCFwPjxZxJ4ngXEVcphxELRXAOqm2A==", "978eeffa-a221-43eb-badb-b380360297bf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0851dfed-1c62-4601-9c05-4bd292309bf7", new DateTime(2022, 7, 24, 17, 41, 58, 764, DateTimeKind.Local).AddTicks(4860), "AQAAAAEAACcQAAAAEBRmorkZTnbMI7ZlSev1f8Ek9xh3YUBqVk4d52vALBHPNgrzpvTItXMzbk0MjWaV7A==", "8d7b9ef0-8414-471f-a919-3abb5856cab2" });
        }
    }
}