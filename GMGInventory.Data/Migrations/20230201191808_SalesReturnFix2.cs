using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesReturnFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "SalesReturns",
                newName: "SalesReturnNumber");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "d03a60f7-cd52-4333-8ad4-a7bb208ea80e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "9b7e2b1a-67c4-44b1-a3c4-39117274e89b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "270c846c-abfc-473c-b910-19fade8f0fa1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "27c1dada-6fb0-4396-9bdb-5ea51d9d006f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "90ff41b9-c5ed-4cb3-8728-a027bb2185e5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "c715aa25-3fd2-45dc-81af-def0180f339c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23de67d9-5721-476f-a74e-5fb9c9945255", new DateTime(2023, 2, 2, 3, 18, 8, 120, DateTimeKind.Local).AddTicks(2569), "AQAAAAEAACcQAAAAEM3I/uD5I3RiGHNB9yKvO7b9mCrtWEAyCTpHZon8cFvS0LCE7tlVGPjAKoCcDCRLnw==", "61368116-0cab-43f8-92e8-79953ab6fa58" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc5bf729-69df-4a78-8072-d349ad322516", new DateTime(2023, 2, 2, 3, 18, 8, 113, DateTimeKind.Local).AddTicks(1618), "AQAAAAEAACcQAAAAEFxL6EgJ0fGhQTLAJuIJX8sQ8Frt8pB710NO6nOaK5gFAxVWlbRTwgZoKR3Wzxoimw==", "67675e50-8e66-440d-928e-58ae9d947667" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalesReturnNumber",
                table: "SalesReturns",
                newName: "Number");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "ef0b0d44-ef4b-4610-94d7-aa9cce55120a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "8284c6a3-6f67-434b-993c-e2c6df098e0b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "165db290-9a5a-4cb0-a595-9006d25e071a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "b66e0de4-b6e2-4dc1-a2af-696389289547");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "6bb583e5-0cc1-4579-98e6-9551f6d882d0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "502648ab-f13a-4609-8eb7-d5a9958b2c22");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93e06e76-0b06-42c3-b160-a0a0ec3d62f0", new DateTime(2023, 2, 2, 3, 5, 37, 718, DateTimeKind.Local).AddTicks(7394), "AQAAAAEAACcQAAAAEKjVW9H4eNREIqN8gGeRlQZuyN3h0hn9H7kvAO2SCK90fc4oanGRW65wkr5rMY39vQ==", "9f039fd1-d3a4-4d50-9899-8c1e420879f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4cd415bc-b967-4228-94a7-068cff4a2b7a", new DateTime(2023, 2, 2, 3, 5, 37, 711, DateTimeKind.Local).AddTicks(5224), "AQAAAAEAACcQAAAAECYYooKqUNll3NNRCVnfLvdpPq55iAycLi9r6E/QH7TCH0EhD4gJPAD+mWh3hFEOeQ==", "2ced34bc-6b40-4992-bf1f-9931076f6fd0" });
        }
    }
}
