using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class AddUserIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "956e89e8-65b6-4c28-a768-701212ca538f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "2d5443b8-f940-413b-9ca1-d21250292b4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "b9df9a49-d70b-4883-9fa7-e7650a21634a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "22e49468-8c1c-4378-b705-5a1fe0b5a03a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "dbf9b5b4-175d-4d33-aa69-cd4c7bec7c2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "9c0dd93a-9d63-4992-906a-55f9024d62e0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7bded874-8615-4f58-b2b6-8cb3de499a21", new DateTime(2022, 7, 18, 12, 1, 33, 826, DateTimeKind.Local).AddTicks(7801), "AQAAAAEAACcQAAAAECmjJviGIXTNUwbCY6oCqbX2OCzOoNuhlnCBLK1n5cD7sRLgqmxpSUJMXqrMXsso1Q==", "dc0d2b28-73fc-4dd6-9519-f2e5cb619a2c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "be6cf854-9be8-4f4a-ab02-6a668c7eb14e", new DateTime(2022, 7, 18, 12, 1, 33, 819, DateTimeKind.Local).AddTicks(6275), "AQAAAAEAACcQAAAAEL4gYECeqa0qU5fjpW5aMHrX/g9QhAT88oeO4JZyzYs3aOcI853XmynSi4wx7ERFpw==", "3e75d585-749d-49df-9ebc-c8ffb9b85b30" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "da655b23-7ad7-42de-8fe1-b8e54930b326");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "a78ef78b-cbcc-43ab-91c5-51033438380e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "486695b7-ae34-4cdf-b9f5-7189dee5105e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "5708573b-29e6-4594-bf4f-d1de97192f13");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "a2d1e99a-0c2b-4638-9265-614d6f9dfd4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "e74e991f-3012-47dc-bbc3-111247555fe8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c7f3042-273a-48f3-885f-f4de59ebe8d6", new DateTime(2022, 7, 16, 21, 58, 5, 78, DateTimeKind.Local).AddTicks(273), "AQAAAAEAACcQAAAAED8CtlZZugeNUJCMkR4W/QBxw5Dyt2jI/OG8jOrn4bWHG/qIuc8K8yTA7FfUyANj5A==", "e9fc32d7-f4d4-42b8-9cc8-1fd0100e27ee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f84e5fc0-ab38-4a17-86aa-2919b2b2e800", new DateTime(2022, 7, 16, 21, 58, 5, 70, DateTimeKind.Local).AddTicks(5476), "AQAAAAEAACcQAAAAEFewMli0ulhJX1uZkEhvFF++cSq5/KsUdXwFCrjDOjFQ7qUoonDwLatu6bYha4zGcA==", "dc81ea89-067b-4bd4-bc17-3958cd3767f3" });
        }
    }
}
