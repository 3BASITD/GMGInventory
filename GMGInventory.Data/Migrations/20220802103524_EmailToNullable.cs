using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class EmailToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsrModified",
                table: "AspNetUsers",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UsrCreated",
                table: "AspNetUsers",
                newName: "UserCreated");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "2d547f2b-f2d5-4577-8c3b-1b57f4afa924");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "30c99aab-0546-4f57-aace-57a4899008d8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "9321739f-a7e1-427d-8f0c-17667ed4d0ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "93f67217-7e59-44f1-9cd3-4802e66a7e31");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "b94b7e72-11cd-437c-a4a1-7b0c9975e93b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "63609f27-f022-450f-bd60-3ab8354ec91d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b9b0510-7950-4788-b945-86b516c90038", new DateTime(2022, 8, 2, 18, 35, 23, 512, DateTimeKind.Local).AddTicks(5692), "AQAAAAEAACcQAAAAEOgFcVQH3PfohIgmTaz9Hr6J5f6TVnOKVZrsZ1o+MVJpQMERtS6nvl+oIzdD6dGAhw==", "c6d5e89c-bbdf-44c8-bb32-7f0267002616" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08e79fc1-800d-433d-b09e-106c6044ec39", new DateTime(2022, 8, 2, 18, 35, 23, 505, DateTimeKind.Local).AddTicks(4606), "AQAAAAEAACcQAAAAEIJnlu/m5G8r1q5uxXBjw9XO+sH5DA1a4eXB9s0UlFNRduwi1T3LTJ+/nLHreYVJNQ==", "3b79e260-3d69-412b-88bf-dc4e5c38f06a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "AspNetUsers",
                newName: "UsrModified");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "AspNetUsers",
                newName: "UsrCreated");

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
                value: "cff12848-0200-4147-bd80-151ee526d321");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "a7ec9c03-4c1f-4f72-953b-dd1e68168e0b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "204903fb-a41e-4d64-8c5f-e3ff8f5f0605");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "f23c75db-4418-478a-a5cf-ca1b6b1de998");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "8b3063d5-99a6-4cfd-ae76-dba73d25241b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "a61aed0b-9891-4831-b597-e473047740e0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6a62cce-a65c-4ecf-8adf-d031f79e320a", new DateTime(2022, 7, 31, 16, 7, 43, 901, DateTimeKind.Local).AddTicks(7099), "AQAAAAEAACcQAAAAEDHsbemGjRTW/2gURYY5UwiU3wM9lDqDK/ruDv8VGagTeVgtfGMojBemzapCFZRxow==", "6b7c07c5-103c-4647-b877-bc3d8dcbe7ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b0c8844-066e-419d-aa7a-b722d8327b03", new DateTime(2022, 7, 31, 16, 7, 43, 894, DateTimeKind.Local).AddTicks(5469), "AQAAAAEAACcQAAAAEIlS/JEW0EsG8Qfbmt4qRrKEo+C9+FqoF8Rgk9Zplay5AArgFWJO/ZJOTwEG+OKhjg==", "5a899ac2-7d94-4c8b-b93e-fbee239674eb" });
        }
    }
}
