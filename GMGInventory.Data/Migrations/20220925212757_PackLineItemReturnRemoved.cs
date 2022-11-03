using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PackLineItemReturnRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "f0c957a7-98d7-44e7-acd4-d76c79fdb166");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "b38abef3-46c0-44bc-ac48-39760cfe1f3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "44b1e621-a5a0-4409-8c4d-927b6ed8eab8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "271e7c2b-09b1-48c8-8469-4aebffab0304");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "9f1177cb-f562-4e6b-8512-b09c84790a22");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "7e4bff2f-bd73-4a49-beb7-73903a1eef31");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d939cf5-c0aa-4460-bae6-86b4d761fdb0", new DateTime(2022, 9, 26, 5, 27, 56, 380, DateTimeKind.Local).AddTicks(1629), "AQAAAAEAACcQAAAAEIluMjYSNgOIErKvMMIa3CQpzeJva4jiXx+32EyAxtapEbn+vj7ng52v/u1+XJo5Rw==", "1039cd88-c228-4915-ab52-30cd5cbc7483" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e03cc1d4-ae6a-4136-9d55-2c72e669c226", new DateTime(2022, 9, 26, 5, 27, 56, 372, DateTimeKind.Local).AddTicks(9693), "AQAAAAEAACcQAAAAENiEez8RMBl+CCfP7fVzL4R5lrof7lQNPhFmJP9t4ddZeuqkOKuH19mv0oTxptTjMg==", "261f58bf-1285-4851-812e-0d2335d28452" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}