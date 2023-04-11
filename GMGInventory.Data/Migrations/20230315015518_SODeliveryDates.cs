using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SODeliveryDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeliver",
                table: "SalesOrderDeliveryMonitorings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePickup",
                table: "SalesOrderDeliveryMonitorings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "060343cf-b8c0-457b-89ff-7ebe48c736cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "131a78cc-906c-42a8-8b89-81573a6c3581");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "5f8d86b3-1323-4f37-9f13-39442be24e63");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "fc1d9c7f-431d-483f-b755-353181b1e27f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "4f97be2e-1cfe-4824-a424-cf2d12fe3592");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "1fe2bcd1-c432-4a9a-b548-3c49aa6cec76");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ebcaf3ee-abef-4210-847c-96f127d339c5", new DateTime(2023, 3, 15, 9, 55, 17, 771, DateTimeKind.Local).AddTicks(9719), "AQAAAAEAACcQAAAAENmN55JeKdHtMzjpuiKdS/BjDcI1Oh2S5ycGkScr2mDaTcRSoC0HG+x5kjP3lM4Hhw==", "45677d0f-11f3-44ff-9ff9-75d2e709d780" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45cb3175-09e5-494c-87e0-6af52a1b61bf", new DateTime(2023, 3, 15, 9, 55, 17, 764, DateTimeKind.Local).AddTicks(9093), "AQAAAAEAACcQAAAAEGZaLyg0CMtKVpN8L0F+PTxj+fzEXnZFyChQfg5H7kxxZRn1kM8DRIv0N1/1Tf7XXA==", "ced54908-e8b2-42d7-b3a8-02b20b246ed5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeliver",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropColumn(
                name: "DatePickup",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "196abb82-e202-4957-88bd-c924839489bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "c545ee36-683c-4400-ab47-e6f77d38155a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "b4352afd-090a-4d62-91fe-736115ef9a0d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "c495e80b-f6e8-4579-895d-d4bc0a663481");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "079a8290-9df0-445d-86f0-573c987ddb98");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "48c50626-b7e3-40b6-b13b-a84bcb6c05d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0f0e4dc-fd56-4091-bd01-f7ed90da9569", new DateTime(2023, 3, 8, 12, 44, 45, 132, DateTimeKind.Local).AddTicks(5613), "AQAAAAEAACcQAAAAEAHryfot5JtbmK6CE1NvPlxGHPbYJrxBa0+ruek2IPwbSGs86sM1E6BQ0jOIIVNXZA==", "21e262a6-3ae2-4679-9b5a-0b7cb8b58047" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4d20bbf-7d38-4fc0-b976-1e72751844d2", new DateTime(2023, 3, 8, 12, 44, 45, 125, DateTimeKind.Local).AddTicks(498), "AQAAAAEAACcQAAAAEJqD0awDeK5g79SbNHw1S9lwxll6K/Km4lCDF47bi9DquGDQqhwGg2j3po1eAxGm/A==", "9025f2ad-6be9-4a21-973e-aaa56deea919" });
        }
    }
}
