using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class CustomerPaymentRemarksTo500 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CustomerPayments",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "6ff86e3d-135e-4d71-8248-6d26d194b249");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "7b4f8169-7140-409e-90c0-f0b94bbfd7f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "a3c1a8fd-3c30-4bd5-a3d1-6170fc4e2b15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "57b40d43-3693-43e2-8e6d-4443037b7e27");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "3937c0c1-f063-4a0b-bae6-7d85b321fc73");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "044b59fb-28d0-4e7b-9557-fb5b89374437");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71dace9a-40e0-4ac6-acb8-be5a97034cb7", new DateTime(2023, 6, 16, 23, 0, 51, 489, DateTimeKind.Local).AddTicks(9490), "AQAAAAEAACcQAAAAEIp9WrqO3c0eChus/xoi0QnVUkTcW4r2u22fOkf4WE33luW1rq2jYbJI2ZBdK2FxPQ==", "07d8648a-caa3-4322-aea9-79b0eb0858bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2fb4d776-0ff7-41ea-98eb-f26e2e5806f1", new DateTime(2023, 6, 16, 23, 0, 51, 480, DateTimeKind.Local).AddTicks(9571), "AQAAAAEAACcQAAAAEAY1Ne58aMrbytcvJ4Ifrh02c6BdodnwODCSRm/+Lbqg4m3Kx3unG1sJPgEQyxF8XA==", "e2bffb79-2376-4955-a9aa-6b61507231c1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CustomerPayments",
                type: "nvarchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "852f2a08-0481-4d04-98c0-b520e0cdef9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "a82dbf30-026b-4542-9056-5b32f80b4dd5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "f5f9e3a6-0eb8-45a4-80eb-0ecefd71649a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "c64e6f9c-a8b1-4a99-bf89-bedd8d695324");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "8174bf8c-3e55-4ac4-9922-dea2a1b8a4ef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "64c9d47a-e2e9-4161-a002-2642b98b745e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43d203f6-47f6-40d4-a422-44ab8a511795", new DateTime(2023, 6, 16, 20, 13, 58, 768, DateTimeKind.Local).AddTicks(8204), "AQAAAAEAACcQAAAAEH2r5xtm9sMTfsGwH4pzq0Etbh0X69IPEoJcXwzhVLsQyQl64JkKyi5uj44uWiA1vg==", "a687ebad-6bf2-4df3-9b28-20809c2c7c5c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df22ed21-a8fa-43c6-9a6b-fbf430dbdf00", new DateTime(2023, 6, 16, 20, 13, 58, 759, DateTimeKind.Local).AddTicks(8779), "AQAAAAEAACcQAAAAEAfo5bpESX/WvfGFjnCDKWxgqD2LRrHFp8Oebm0dmEgZCp2Kjm7opBmsSXFg+OQBwA==", "42d289bc-cd90-4cb7-b18e-511163afc1b5" });
        }
    }
}
