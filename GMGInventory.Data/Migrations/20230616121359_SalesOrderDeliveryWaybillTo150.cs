using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesOrderDeliveryWaybillTo150 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Waybill",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Waybill",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "0c7e754f-f0de-426f-91e1-03047b1b59f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "5cc304cf-604a-4103-ae67-d33fbf9e45cf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "706beaab-0376-4c05-b04d-a1dc0a40ce90");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "3a9c136b-13d8-4dbb-b54f-e001ac4b2d02");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "64a16bd2-f680-40f6-b148-65ce05e7523f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "87dd6f3d-c544-4767-b5c2-bbeddfc14f2f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a1a5b9c-03db-4d83-add7-833f72fe9503", new DateTime(2023, 6, 16, 19, 53, 50, 491, DateTimeKind.Local).AddTicks(1657), "AQAAAAEAACcQAAAAEB/WAre6du948NGf33AdvjkyIkvm+2jDJyRYoF+PakAV41WxKjgC5Ftay8J2r25kgw==", "f7d51795-85d5-4060-b5dc-f3abb0c3c5b9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9597457b-7396-4078-9396-5b07d5a7e150", new DateTime(2023, 6, 16, 19, 53, 50, 483, DateTimeKind.Local).AddTicks(3240), "AQAAAAEAACcQAAAAEA/yZCkAA31/FI/9/d4Spth5ycSNFTaYcb+AWR+93S5Gn1tu2KeDKfxuHymSXhpAow==", "b99ad699-a7cc-40a5-8518-d017ffa271a2" });
        }
    }
}
