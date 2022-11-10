using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesOrderSellingPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "SalesOrderLineItems",
                newName: "SellingPrice");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "355fba11-8795-4f21-895f-e72d929989a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "51b3f565-7d48-42ae-9e58-56142834db66");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "af038293-de18-434c-9286-578bdadfc744");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "71eb1fb2-5cc0-45b5-a52a-9eef2ba441f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "6e0ed5b0-d129-4b6d-8000-1bbeacc7eb3c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "0a3e93fd-81cf-4865-a895-baeb17d0f107");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4be25b8d-02d7-46dc-80bd-ea82c61c08f6", new DateTime(2022, 9, 20, 8, 44, 12, 415, DateTimeKind.Local).AddTicks(8314), "AQAAAAEAACcQAAAAEEk0RhvzqTUDErpUXpLbSQ0eFMGEBR1SoegKMQF/hrzy+ZUjEwLqmySuSmojYyYW7Q==", "6abe0a1e-60c8-4a48-9e84-13a906798b6d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85503245-3767-42c4-b268-9eba593fd484", new DateTime(2022, 9, 20, 8, 44, 12, 406, DateTimeKind.Local).AddTicks(9615), "AQAAAAEAACcQAAAAENoq9FtAurQEawY2o3tP1fsZqIVxh2nqcxipb6u9exBE236ABzlXwCuVzjDAlcyTpA==", "ccafc0fa-e6f5-47d5-871a-9a62445ce5ce" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellingPrice",
                table: "SalesOrderLineItems",
                newName: "UnitPrice");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "4057f9b4-a39d-4a68-aa64-d2b2917cb7cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "8917d915-0b9e-4fd3-ae45-f4ac934b4e6e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "1e318333-ce78-4215-8291-13474160c69e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "08bc4652-15d0-4219-b804-fcb3d6795447");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "a0ff06ca-3568-4fe3-8412-711266a063c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "c26277bd-6bed-4a0e-b913-a21dd83b8b53");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8612cbb1-2a6b-4d01-b9cb-2a2eec240dc8", new DateTime(2022, 9, 17, 3, 49, 41, 187, DateTimeKind.Local).AddTicks(7433), "AQAAAAEAACcQAAAAENh7Uw365SdDydJ55rUtsR7BJQMlwkj8H3sMaKWKjZ5u9LzbUfoPmYMsw1oLb1zusw==", "20df97e4-f521-40f7-8981-a3efd68d8503" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bcab3885-ad00-4660-9785-ebe9b6792de6", new DateTime(2022, 9, 17, 3, 49, 41, 180, DateTimeKind.Local).AddTicks(6743), "AQAAAAEAACcQAAAAENPsMU61gxNqgSub4sf/NWHptk1dAzprfxaHScgAy766zDpU4hphKZpCjHFmqPTY6Q==", "f5fb6c54-4ba8-44c2-9259-7e4c57ba9776" });
        }
    }
}