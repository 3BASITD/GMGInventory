using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MinorChange3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetSellingPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "RetailSellingPrice",
                table: "Items");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "f87c7ba0-d5ae-4a83-8d0f-fea974bd3df4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "888075e2-d0a3-4443-877c-c08b963926aa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "05d6627a-9979-459f-8725-d082729a1b73");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "bcbc9d90-1857-4985-8503-49cb8d57791f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "85619313-b45b-4cac-90d3-1b5895fdbc19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "115468e8-ebfd-4544-b3ff-80b500cd81f9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ceebde00-a645-477a-8fe1-de58432dc417", new DateTime(2022, 8, 3, 21, 26, 26, 238, DateTimeKind.Local).AddTicks(2059), "AQAAAAEAACcQAAAAEP+ayEDunLu9Kvh0nEXVwabdwuD+swNy0WSqvjVQXA8MGyYyaZ411cDwvhBEFS+QJg==", "d9fa7ef9-a7a7-4bcf-b93f-77a95d850446" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fdc9abd-1c5b-45ff-8562-40d9e32f7d72", new DateTime(2022, 8, 3, 21, 26, 26, 223, DateTimeKind.Local).AddTicks(7098), "AQAAAAEAACcQAAAAECdZRbtYTf0+GGc0lQ5FDKeft5DZOyCssDbC/u6n3BZnC1V4PagyhBIvc3PlIIBfMA==", "d892799c-aca4-4cf1-ae1f-d359e7e87fb2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                value: "b5a34cef-a091-411c-b1de-721691278d15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "497b7b19-e370-409d-8cf3-ba0b8d47211b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "e36385e8-7670-4bfc-afbd-67cdecf83861");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "87c9fbb3-f3f1-4ec0-b357-abe3bb94900c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "1563ac5c-d5fa-466a-ad88-7391b6e8e4b9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "62ffe4c6-67e8-4e9e-a159-b8ff50e1e9f1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d363202-3e75-4848-9ef0-a5803b55a89a", new DateTime(2022, 8, 3, 21, 14, 21, 854, DateTimeKind.Local).AddTicks(6970), "AQAAAAEAACcQAAAAEM/gznXnrIKlgQ3rp5g49f1uDtzRk4FEX4HQXHoh1q8X8j3a5fgPmqjBkeTcbFk3Xw==", "a619e650-2877-46d9-a142-39f566b85ffc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6488190f-fbb7-4b00-921f-086057db1090", new DateTime(2022, 8, 3, 21, 14, 21, 846, DateTimeKind.Local).AddTicks(9218), "AQAAAAEAACcQAAAAEDHq8oSIkvTBTv0mM2mr5z8a/SkTdtca8U/UltRmeLsMnXLlseuoLFEj/L4khP18zQ==", "67d979b7-2509-4fe0-8c1c-5084edd44db2" });
        }
    }
}