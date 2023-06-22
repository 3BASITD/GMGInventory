using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PackTypeIdToNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackLineItems_PackTypes_PackTypeId",
                table: "PackLineItems");

            migrationBuilder.AlterColumn<int>(
                name: "PackTypeId",
                table: "PackLineItems",
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

            migrationBuilder.AddForeignKey(
                name: "FK_PackLineItems_PackTypes_PackTypeId",
                table: "PackLineItems",
                column: "PackTypeId",
                principalTable: "PackTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackLineItems_PackTypes_PackTypeId",
                table: "PackLineItems");

            migrationBuilder.AlterColumn<int>(
                name: "PackTypeId",
                table: "PackLineItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "53d64e24-347e-4ba8-ad5d-5672d5cc9554");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "b9b6e6a1-2c22-4d23-99dc-28be03f53333");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "5b6a7eff-daf5-4139-b7a1-9ee5a01dd57c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "ec8a12e4-90a3-4fdf-88d8-8551c6bb2a17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "fee26133-8615-4ae2-b65b-829eb6007402");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "c1691e5c-3897-4730-a5ec-f179093c7d16");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "753afe32-25fa-40e8-920e-26051b25e6d1", new DateTime(2023, 6, 16, 19, 18, 53, 217, DateTimeKind.Local).AddTicks(9916), "AQAAAAEAACcQAAAAEKMYEfTy+fGpnWUK4aojmf6mcVLlCpfSva8wn/47PkD9oSwt3l/g4eQ4v8GAo+tX2g==", "a7cdd493-64fc-41fb-9487-fac7a8bcfcf2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d82e6178-3b96-46b5-9f09-eba4b276c281", new DateTime(2023, 6, 16, 19, 18, 53, 209, DateTimeKind.Local).AddTicks(7735), "AQAAAAEAACcQAAAAEKlXWimwrJvfABGQ6fEDKIHpXGmpHzTS8E0SMaDbH9tgcisal9so15OOypfpaPb+1A==", "4c2585bf-159f-4197-a780-1b5f33be50f9" });

            migrationBuilder.AddForeignKey(
                name: "FK_PackLineItems_PackTypes_PackTypeId",
                table: "PackLineItems",
                column: "PackTypeId",
                principalTable: "PackTypes",
                principalColumn: "Id");
        }
    }
}
