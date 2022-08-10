using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class TransferWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "8b0ff6a1-98f7-4003-ab07-dd491eed350b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "24070e9d-0a79-4156-aa28-838112315894");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "faf0b48b-01d1-40b9-936b-660ac080a292");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "e9084a37-84eb-46fa-ae83-4f7064fb6cd0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "5af6f1d3-56a7-4f6e-9254-3d00bc1c786a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "5aceed09-5d9d-4d7e-98eb-06dbb3122d5d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a124545c-068c-498f-b60c-cf3bddd707c4", new DateTime(2022, 8, 8, 14, 39, 31, 443, DateTimeKind.Local).AddTicks(7952), "AQAAAAEAACcQAAAAEJRygvI5JjEPtGmRnpTkvB4g5zz8wNKmXhDbmvfnsbBFO53t/ya+yTVfmnqMV4BS1A==", "439f9e64-99dc-40f9-8b22-1b52d55ddb53" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "992cf948-9696-47f7-beb2-0906dceaa7c4", new DateTime(2022, 8, 8, 14, 39, 31, 431, DateTimeKind.Local).AddTicks(7097), "AQAAAAEAACcQAAAAELesioYmbCzCAfCY/pm/Za8q9BboNT2X/izLm9rFInpvY7sUR4XowIWjVAZkOI/s+g==", "9dcba104-b70f-4c22-8b71-88b1a994dd89" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "9f23738f-1754-42c2-8ca0-6c8dc6a67225");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "6bc3e246-607e-467c-8e35-29802f8b6ed0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "fad13fc5-a388-4fdf-bf94-eac68289300f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "b8a6f1c7-4b57-4379-9995-4bcaa774322f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "c0d4cb73-7a24-4a78-b761-ac0d23449167");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "50e3dce6-ad19-459b-8dbf-08c5a7b3aef2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1abd1d0-f9d6-4765-99d3-3b8ea8a20ef4", new DateTime(2022, 8, 3, 23, 58, 16, 526, DateTimeKind.Local).AddTicks(4877), "AQAAAAEAACcQAAAAEN/9WQl4P/m0CYj/23DdxLjD5KGCeaW7obyZ7tM5xEYJqFLrg2W+CtI+O1I+5yw3jQ==", "a9aea224-c8e7-473a-b34f-f4e0450d3419" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff2bbd28-1a1f-46db-8deb-6380bd094614", new DateTime(2022, 8, 3, 23, 58, 16, 517, DateTimeKind.Local).AddTicks(8686), "AQAAAAEAACcQAAAAEMnIibVyVhgrYgHa5l3IZwrKbeOQf/b4O+AcEBlrEIPX+co196zh2zbuL6RrRh3Nog==", "14649c4d-b133-4c96-91bd-ef6098f39d9e" });
        }
    }
}
