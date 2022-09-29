using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PackLineItemReturnRemoved2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "4d62d793-686a-404d-b2da-627eadb13f2c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "177e76cd-ba7b-4e7e-ab8f-5e89d341fd21");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "24bf4f09-77ec-49a0-917c-1c4c7ab37d96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "4a836f4b-94f4-4b86-b2b5-201f5a5fb654");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "2a5c3aae-c71e-4289-a250-7285b83ee8b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "15e1d482-cd90-4c82-9b65-5fc72b35d99b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d3a8040-d94c-49db-8b18-9002c0cf1b63", new DateTime(2022, 9, 26, 5, 29, 53, 233, DateTimeKind.Local).AddTicks(6321), "AQAAAAEAACcQAAAAEFr9q4ilLyL+7dMTKY/4GttYr41qTTrvEMuBAOf7PpucYGsaYiVMbFwWn6daJYrVwQ==", "c838e51d-e0b8-4be0-aafa-7ba80075a4b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "454ca3f9-322d-48c0-bbf3-5685232a027c", new DateTime(2022, 9, 26, 5, 29, 53, 226, DateTimeKind.Local).AddTicks(4217), "AQAAAAEAACcQAAAAEOaOTUmarLAPBeGNzdWi3qUOa7owOqes17pzS2O95WZt0hMM9YivGgaJ6Rj9cRtNFA==", "63dbc81a-9897-4c25-9db7-83184bbf0b44" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
