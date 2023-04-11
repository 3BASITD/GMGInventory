using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesOrderDisapproveReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisapprovedReason",
                table: "SalesOrders",
                type: "nvarchar(150)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisapprovedReason",
                table: "SalesOrders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "f2470ce6-fef2-4d5f-b086-f29e2d76e632");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "3debe674-4349-4add-b3ad-306d0ecdebdb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "3cd2818b-cb26-4f8e-8ad0-2545fbeeb11b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "d2fcae0f-0266-4ae2-adcb-17f0768e7864");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "dae02884-7ab3-4195-b062-50fff0b564e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "b42d825f-f0d3-493a-ab13-24c5466df03c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc4e3f78-dae7-4eaa-80cb-3ccb950595b6", new DateTime(2023, 3, 15, 12, 15, 2, 586, DateTimeKind.Local).AddTicks(6133), "AQAAAAEAACcQAAAAELwYlF+pYmSEdNx5jFoMkvhc1NxPO8FDBiuS4vrvSYPzwU8O1JeIruzR/mTFgSrgcg==", "f4a9622e-0d66-455b-86b6-2126be855a65" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc1674bf-236a-4b8c-bd27-78f6e2ac6d87", new DateTime(2023, 3, 15, 12, 15, 2, 578, DateTimeKind.Local).AddTicks(4242), "AQAAAAEAACcQAAAAEO59SHKSp1KA3muA1ESZW0C2Fb32iLyjF/LYqCtiOpQNZOOShr3EyIhDNdpa0aUNLQ==", "b97b6b0a-b67e-475f-bcc8-65a91c865fc2" });
        }
    }
}
