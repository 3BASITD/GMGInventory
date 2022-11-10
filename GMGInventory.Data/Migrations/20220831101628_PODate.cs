using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PODate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReplenishDate",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "264ce80f-4121-4169-94f0-30ef43b8ba0c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "569b38d2-2af7-4a8f-8822-1e9bccd53d47");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "d927e31c-47b8-451b-a37b-004f275d1c82");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "89eaeed8-e460-4d80-848e-7b3c9cb68f71");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "a5f56381-776f-4532-bc00-c09bdb54c6c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "71997481-1db3-402a-9dd7-1d79f50ab0ba");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ed0f4d3-a59a-4ede-a83e-839ecb400d9c", new DateTime(2022, 8, 31, 18, 16, 27, 516, DateTimeKind.Local).AddTicks(2241), "AQAAAAEAACcQAAAAEKSnGmlPG94eBaKmgRZZkxsWBwIE+dIBQ+cHOora24p6l3GoT3Z+oAlDYISAwYe3Eg==", "8df0200a-9340-4b83-9d03-01de704618a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a017ef78-1f91-4508-85ed-8a32e3ed0f17", new DateTime(2022, 8, 31, 18, 16, 27, 509, DateTimeKind.Local).AddTicks(285), "AQAAAAEAACcQAAAAEKuaL9ZS2lSzd07kFrPqrqZUfCfMzbSomdrjPxQPOgZG0CjPlLhmmXgYguPku0J/Jw==", "15518c9d-5ecc-49c1-80b5-f08e991181ca" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplenishDate",
                table: "PurchaseOrders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "ca92c6df-7690-4fa3-af42-b09f71308dae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "10c780a1-0f00-494d-8739-1415182558cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "6ea50082-6f0b-460d-9f13-f3abd1270daf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "76b5dbca-0234-4533-931a-d41fa26ce477");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "8450d2ca-57f0-4c68-868b-ce547fc8b39a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "7fc2cd33-3fff-47e2-9e1e-964f9d41db46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11775bc5-7f09-45a4-bdcb-8013d923ce7d", new DateTime(2022, 8, 27, 5, 22, 43, 445, DateTimeKind.Local).AddTicks(5139), "AQAAAAEAACcQAAAAEG2ByF8NtjubkISFpqadEal0W0ltE6QLYIluEvwxw3mL957KtWQC9pO0VCcy+uWFIw==", "a7e2bd95-682d-4b91-9b7a-f984c1f3b3b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32cb5d48-3536-43fe-b421-5a6b81e96670", new DateTime(2022, 8, 27, 5, 22, 43, 438, DateTimeKind.Local).AddTicks(4856), "AQAAAAEAACcQAAAAEHTHqQuOtjmYsExJ0IQO6Jg+PR7K8h72a5LlY64/WgX1YZGZRYy6cpsvoynMP8Ymdw==", "c116cb12-3e4a-4634-9e55-619d55a6c729" });
        }
    }
}