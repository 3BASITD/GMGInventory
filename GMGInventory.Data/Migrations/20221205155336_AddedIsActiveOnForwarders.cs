using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class AddedIsActiveOnForwarders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "RequiredDate",
                table: "SalesOrders");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Forwarders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "66797640-ecc3-435a-b98d-e3121110fe40");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "732da911-ef76-4e88-a93d-52a52354bf63");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "56764c49-8223-4bcc-8967-9568620b6b11");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "227e7066-a125-41b2-9a82-62ca8636d0e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "e00ca1a6-f5c5-417a-b859-cf7fb3ae0a4a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "7c8126f2-ce6d-4278-958c-3a9055e0177c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "823ddf83-c2c0-4a39-bf45-70b4e59116fa", new DateTime(2022, 12, 5, 23, 53, 35, 891, DateTimeKind.Local).AddTicks(7657), "AQAAAAEAACcQAAAAEH+8trBcF98RELxQApX5kIAwTHYihbPW7z8Jj0UMOR1xzihuQ6uT5LqC3TTGr6/Dbg==", "a531afd1-b24d-4ad6-8674-ae919c664e4a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed8bfe06-c636-42d1-a337-a86a288cd234", new DateTime(2022, 12, 5, 23, 53, 35, 884, DateTimeKind.Local).AddTicks(6959), "AQAAAAEAACcQAAAAECmjbUYlfA3rkj/1Y0VeQcK8rRzWz4ywIZcHEzdx7Zwlo9cIPEMSoi8lSeMAha1SOg==", "48715c73-1f3c-4545-81b0-1a1f17bb9cc7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Forwarders");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RequiredDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "5ca2b5e7-c79f-4fe0-96be-a1240592d39c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "266ff8f0-5089-4ad1-94d5-1e131cc35cac");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "185da536-4b0d-4288-ac55-9d5a0b1b2c63");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "92c7ea9c-6550-4c3d-9487-125759be5dfc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "3f033597-2ef6-45df-a461-ac1bc12b0967");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "c3cee8ea-cd17-4316-9003-b61a6312ca48");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca5e8ab1-333f-4bd7-b8c7-71b43a8a57ae", new DateTime(2022, 11, 4, 13, 9, 46, 339, DateTimeKind.Local).AddTicks(5314), "AQAAAAEAACcQAAAAEIBx1xuvlMIAzr6CF2KxkiZWn0SdzOA8+dps1DokKGzGRG/GjnmZTQ2nKV3KhnR8Mg==", "90d7b3fa-5773-40ab-9dde-e683f13d5ae6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a5a16dc-4671-4ecf-8143-8d1fbd68ae9e", new DateTime(2022, 11, 4, 13, 9, 46, 327, DateTimeKind.Local).AddTicks(5434), "AQAAAAEAACcQAAAAEHhBrpjffV7otB3ihrzWXmCurP/7+wLO7pE28SVngrSNuKTrfSNQWmmioYURPteqww==", "2d53fa74-f440-4179-a4ca-e3f4ad8995e7" });
        }
    }
}
