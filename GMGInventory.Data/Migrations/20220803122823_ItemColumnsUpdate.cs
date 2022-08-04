using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ItemColumnsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNonInventory",
                table: "Items");

            migrationBuilder.AddColumn<decimal>(
                name: "NetSellingPrice",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RetailSellingPrice",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "93194199-38c1-4c10-8456-7eff4a264823");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "4c775b36-a32c-4b87-b328-2dadf49203a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "e074989c-61ea-4870-8644-53b79a5494d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "f8e216ba-83ed-4861-b86f-fab2777e6c97");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "cde7b167-2504-4d73-a015-96c7e49b92b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "da6d9b27-cf11-49fb-92e9-f4b1ee2da8cf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d5218c5-3591-4469-8603-e5011368b935", new DateTime(2022, 8, 3, 20, 28, 22, 431, DateTimeKind.Local).AddTicks(8503), "AQAAAAEAACcQAAAAEE2AZKBGQswN1ioD+45uESJRKlmK50itr9L9f0M3toX6qYuilsjAINBB+SH45Uondw==", "920e6554-2881-4ebd-9e4c-41946b6a87f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fbc75d2-eed3-4cac-915e-0dc03ac5114d", new DateTime(2022, 8, 3, 20, 28, 22, 421, DateTimeKind.Local).AddTicks(553), "AQAAAAEAACcQAAAAEFr6V8pNJGFskGOgZVKVAZGVs9TAmQhcjSsZtCFyom0QYhO1TtTTOEWXSRKy2Rn9vw==", "d5646cf6-e164-486b-83e2-0085fce740d1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetSellingPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "RetailSellingPrice",
                table: "Items");

            migrationBuilder.AddColumn<bool>(
                name: "IsNonInventory",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "aad71d2c-9fc9-4eff-8f8f-c86e8a900c12");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "7b6d707c-4bde-4743-8484-43d02c426e6d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "44fadb51-83b1-41de-bd02-b8c45123e365");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "237fc813-c1f1-4a19-8cb6-ac9f1493b137");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "15f4abd3-7fbc-4396-9686-c3db2886166a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "74610947-513c-4236-ae03-3c64f8964d41");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68befc99-12e0-40b2-969c-c4b01318787d", new DateTime(2022, 8, 1, 19, 35, 52, 37, DateTimeKind.Local).AddTicks(6102), "AQAAAAEAACcQAAAAEOmm6GBVznI+EvR4wTJGGoNvF3NJYZK1xGrj0VfdKhhiwuVstF1a/o1SmajpkEmASQ==", "8a8f7324-802a-48ab-9546-b70a6c00281c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5712ce91-bdb2-4aeb-a08a-790cbfe85eb1", new DateTime(2022, 8, 1, 19, 35, 52, 27, DateTimeKind.Local).AddTicks(9770), "AQAAAAEAACcQAAAAEGlUBA5fqySVyt1kIEJKbo/OGdzHE15IbM5CnMEJBXCu9Qe/mXCPR2TvzWDVWLLezw==", "1d8a06cf-5284-4ac1-b22b-8319d7d9b30e" });
        }
    }
}
