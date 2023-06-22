using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PurchaseOrderReferenceNumberTo100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "PurchaseOrders",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "8f92a4fe-a6c5-450c-96f5-d2ba5117da9f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "b7282dd5-d81c-4ac1-9454-f6108d8099f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "e6514a8c-bcfc-4ae6-a2f5-a79d494d4524");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "099aed54-1c0f-4d1b-83d5-aa566127fd8f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "7c8f7c4e-7f15-41ad-9030-7dfaa02c364f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "72ff9228-f824-481f-9446-ecaa6139730b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc5030fe-242f-4692-82c3-cb39ed375780", new DateTime(2023, 6, 16, 18, 53, 31, 880, DateTimeKind.Local).AddTicks(324), "AQAAAAEAACcQAAAAECCd9DEHZu7zsgaP1IHo78pZyzKp3IncE7CZOumKjf0lOUiKhZvMadYXPwA3L2KhEQ==", "1d776cee-4e27-40fa-9e0d-9054b9502e35" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7aa026c8-6b70-41e4-b89f-87fada549aa3", new DateTime(2023, 6, 16, 18, 53, 31, 872, DateTimeKind.Local).AddTicks(5229), "AQAAAAEAACcQAAAAEMjgC0ZiZ8FfzPuTeG5io+sOq5HVcQcDkmX4OIo9a+J20aeIea5tQwOdEt3m66NL4Q==", "116af0fb-eaeb-40dd-9e8e-26654a31260b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "PurchaseOrders",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "fc239dc8-f2bc-45d9-aaf7-c7067a8fe5ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "098ebfca-d5d1-49b3-8438-8ba8420ea41c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "f4cb6894-c7f3-429f-84b8-5baf69c878f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "09cbaaed-59bc-4ccc-9b47-ac013f475c6f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "7ef21c8e-7aba-416b-aab6-cc7fb71bb65a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "ee13f6ae-ed7a-4999-9e94-79524613cab9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0efa8dd6-f4c1-49ae-a943-88a2033f6298", new DateTime(2023, 6, 9, 13, 52, 12, 7, DateTimeKind.Local).AddTicks(7419), "AQAAAAEAACcQAAAAEK8fwgDbBkD0XINC5RD93ifGufBH2c6GJpaDKxC/F1qWQQn+vb2uJVvZ6PIUUhWKWQ==", "523feac7-52e3-44d9-928a-252e2b3addab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39c8f60f-efff-4dde-85e2-4bced8800bd9", new DateTime(2023, 6, 9, 13, 52, 11, 998, DateTimeKind.Local).AddTicks(8613), "AQAAAAEAACcQAAAAEEpifvJSF8LmwaS4rYYO2/a7L3qEX0Dfu6+sIdX1o4TpknnhN/+O0F/83Hr+E2UhMQ==", "4dbf5ee6-bada-452b-ab20-f35978f39887" });
        }
    }
}
