using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class CustomerEmailNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "a767ebac-b479-4940-8acf-2f3a2913d3fa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "84032a14-13f1-4668-a481-986b1fb20022");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "0364a9a9-6a03-4025-ad09-b1a61cb125d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "4ef7caa2-55f1-4a04-8cd6-c775f3937371");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "5486b36b-43fb-4534-abf5-c33fa7d73ef5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "4d312e95-8ce2-4199-8e19-ec438219dd15");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e87ccbb3-4408-4739-9130-453f17047ae5", new DateTime(2022, 9, 29, 11, 14, 24, 329, DateTimeKind.Local).AddTicks(6010), "AQAAAAEAACcQAAAAEIF1LYy56s1sBZzfkfrEzLZbhRQFZvUYequ3sYvgYCE8/UpsUABho3gb2sBr1lGzng==", "5f94a673-5910-45f1-b50c-75b4517c0dfa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b96c4be8-1284-4a07-9234-85c54ef68b0f", new DateTime(2022, 9, 29, 11, 14, 24, 321, DateTimeKind.Local).AddTicks(4779), "AQAAAAEAACcQAAAAEN3QaRxHe6Lpo73oShuj6e5FftMJenTAJq6QRybxRke8g0z+xdcKBVT4DnMYAVVTug==", "07dcee4e-a0df-41c6-b3a0-48b550cc8dd6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "beb636e7-b741-4078-a8b4-75dc2eab35bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "6b9c79d2-470b-4359-9a04-936e65744497");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "f158df76-c47e-4a41-9a37-7f5f9d0157c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "1230ff73-db2e-4200-9e68-349208e89ac8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "e28cac65-56b7-445d-a491-7dbd47ae1936");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "ec7388b8-9839-4aa8-95ab-15b715ff61a8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab40e118-01d1-4786-a903-e586ad1f6bc6", new DateTime(2022, 9, 29, 11, 12, 6, 684, DateTimeKind.Local).AddTicks(7939), "AQAAAAEAACcQAAAAEFS0F312o4B9tva76L2uQPTrQNs3TkAIlY69rd/5iacag9EXqAefnkQ6TdIVoPmfaA==", "c99c324c-d277-4a99-b678-c6dff9898d3e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79ee2996-effd-48f8-965c-76dfe1e9dfbe", new DateTime(2022, 9, 29, 11, 12, 6, 677, DateTimeKind.Local).AddTicks(4969), "AQAAAAEAACcQAAAAEMTvfoNveMHqjoVJlla2VREMDtHZb4fS+vy/fqs2wlYBXp+7zUzFfP3BSxYztHmYOw==", "80051471-61cc-4a5a-ad9b-40ef522b0367" });
        }
    }
}
