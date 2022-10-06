using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class CreditDebitMemoRenameNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceNumber",
                table: "CreditDebitMemos",
                newName: "MemoNumber");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "2c15db9e-c22a-4059-b2ab-b6ff9aa9ddcc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "62484b1c-987c-4ce1-94ef-2563d8b9e4b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "6ab2d21e-9b64-48ab-9eeb-26a0e0154ec3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "b8ffcdfc-3d50-4c1e-88f3-ccf558d5f73b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "62d3c01c-d3a2-4107-bdc9-179da465722b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "6c3976f4-4286-4d54-bba6-37b398be01fc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b16c8d6-6bd3-44e2-b6e4-fbfa1c0906a0", new DateTime(2022, 10, 4, 22, 0, 16, 215, DateTimeKind.Local).AddTicks(3486), "AQAAAAEAACcQAAAAEEMaaD1Iuuqfr0ZaUgq8ieum/lvdh/gzoMkR3F27p++bsVvrBdHd1ns2N8DdVgbAdA==", "48709665-a320-44d3-9cde-b755380f22fb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0bf3f61-da85-4bad-9d0c-a693f3e74f5d", new DateTime(2022, 10, 4, 22, 0, 16, 207, DateTimeKind.Local).AddTicks(3101), "AQAAAAEAACcQAAAAEMW92ca9GDi/sjK7iIzINnZY7XTwx+Oa/z1eGjWHY/GWlrCSqr+hPWbWbGGHQqBZPw==", "97a20169-338a-4b2b-ad0d-82e77c019c7f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemoNumber",
                table: "CreditDebitMemos",
                newName: "ReferenceNumber");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "3d6e0718-b9b5-4c76-b45c-db41985ae4b6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "f0d1aa2c-8230-4866-a0ff-1473707ff2d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "0243b126-ca37-44fb-9559-d29991f081b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "efc1fbc1-3fd7-45c4-a93f-7580ac14c37f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "dabffe87-1a47-4792-bb96-7858b0007890");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "cb4a3e4d-adb3-46d0-bc93-6fb5843b7eac");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0215a0b5-bffd-4889-892a-c4b03774f4f8", new DateTime(2022, 10, 4, 21, 54, 30, 480, DateTimeKind.Local).AddTicks(2709), "AQAAAAEAACcQAAAAEJioeIog7Vr4ccrWglC3mKVCmYS+x8KxTJ79MbA0rEwDQVIDnw7Lx1EvLArZefcO6Q==", "0e90f027-d74b-4d74-b1a6-974689301fd0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38a785cd-d467-427b-bf64-a42062e82a74", new DateTime(2022, 10, 4, 21, 54, 30, 472, DateTimeKind.Local).AddTicks(2333), "AQAAAAEAACcQAAAAELRPTVexnQsSlVwuzM7KVlaPyhriNGK7tvcpYo31txNYGKP63zCnjaefjyaCSYVWQA==", "aee7259e-d5b7-4a36-b32d-2f3b322b6488" });
        }
    }
}
