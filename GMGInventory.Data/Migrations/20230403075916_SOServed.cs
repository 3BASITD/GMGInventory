using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SOServed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityServed",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityUnserved",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "636ee7e9-2c37-435e-bc25-d25197737df7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "0673f746-9f09-4594-8aac-230356e1a8ea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "afd08cde-f2df-418b-85b7-5c24eb3fd44d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "1ed629a5-2705-43b5-ae67-04aa450d855e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "bc94f43d-0a2d-4d32-b119-a6209af4e845");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "370e6b59-43b7-454a-a4f6-45197273087e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce0a7925-b830-4087-9690-8152bfad39dc", new DateTime(2023, 4, 3, 15, 59, 15, 503, DateTimeKind.Local).AddTicks(6812), "AQAAAAEAACcQAAAAEF/2XinCnlY8CW/4iuuuVoxvHxZTUck5vAz0H7Cl0aavE9eE4jfYX7J7MIDSaoAUhw==", "78833e97-a76e-4b67-9a7f-e270c6ff7c63" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cb109aa-a0dc-4384-a89e-d0625157f561", new DateTime(2023, 4, 3, 15, 59, 15, 496, DateTimeKind.Local).AddTicks(3233), "AQAAAAEAACcQAAAAEHtzoqLjHFc8mPfhAO3q1PXZLHLvVGAlKeeqxj49RPtOl/NKQ3MEktMa5hnwYJgnnQ==", "54957602-b773-4b14-a122-aa59e7aeb68a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityServed",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "QuantityUnserved",
                table: "SalesOrders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "bb87bf23-af78-4bc7-97c2-a204e2355dc6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "5145ed2a-8398-4f29-b4ca-349d7a8f7cb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "5647c768-f528-4579-a4a1-856156e0e54a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "9f72dc94-a532-4521-982c-6dd49d7ce8a2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "3a922f14-4c5c-444e-b766-20ceea9b447a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "ffaf2281-9875-4b34-abbb-f6f84a78f6e9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "767ac710-ceee-4c2c-b04d-a0171ab7f2cd", new DateTime(2023, 3, 15, 21, 20, 19, 317, DateTimeKind.Local).AddTicks(1927), "AQAAAAEAACcQAAAAEOwEtwuCacIFg1++q1wdBVGQ5MhVbVZLkkMSFPOCOXZADhvVx5jkd05CKE0yGAWHsw==", "78688539-eb3d-467c-a4de-de2ae5a2c927" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "463721c5-2aff-4970-a8ca-c5c5d2809ffb", new DateTime(2023, 3, 15, 21, 20, 19, 308, DateTimeKind.Local).AddTicks(3319), "AQAAAAEAACcQAAAAEO/5gGzYKF4OgVcZTyfkKSw8T000mYdtgLyU65GX0YaKSisz1HiwWLg/Kw1HQTTLpQ==", "65c3ba1d-d3a8-45bd-bea4-14a87253ac5c" });
        }
    }
}
