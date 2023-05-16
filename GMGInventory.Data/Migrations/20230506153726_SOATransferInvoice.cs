using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SOATransferInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "CustomerPayments",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "799d680f-53ec-400e-80cd-d50f595dd029");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "955566b5-3748-44d2-a738-03b2ab10998d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "07bc5426-c8b8-45f6-be4b-8e12d23cedc6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "16e82ea6-6304-4db5-a89d-d1f4d55eeea3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "8330696a-5ec8-4a82-a6c4-86e511f88f7a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "01deaa74-bab3-473d-83d9-6d13994eeda9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80db24bd-ec9c-45f6-9008-4281df82149e", new DateTime(2023, 5, 6, 23, 37, 25, 287, DateTimeKind.Local).AddTicks(447), "AQAAAAEAACcQAAAAEAp7Ey7Cc0xM1Bu3pcZ3LMw/K4enlIwBpH4xiwcimSYevYkkuATMOPrhbACYp1N35Q==", "d244ffa2-634c-4d7d-9465-f4d70593bc3a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d33f7c3-e283-40f8-b8de-0405a325003a", new DateTime(2023, 5, 6, 23, 37, 25, 279, DateTimeKind.Local).AddTicks(3546), "AQAAAAEAACcQAAAAEAv+wkoFNaO43Ak0JK+rfMEGwjWQW0u9uSGhHlAhDUhAAjdz1EQ3HlTQQA8DI/ps0Q==", "916f12dc-435f-449a-a747-9293153f2f13" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "CustomerPayments");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "06a3fa94-3e88-4686-9091-08af4a0e2e19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "f9ea35a8-09c6-4195-aa23-e3487bf7176b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "30df9b2e-80ff-4839-b319-247471ee8b09");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "c2d12976-5958-467c-9c04-1b976e4bae57");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "4165ebb0-06b8-4e26-8c0b-e2a967a40a8d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "93095415-2627-40ab-baff-3164a243ff63");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "def173ee-6f7c-4c7c-b861-bd0edba81ea3", new DateTime(2023, 4, 12, 17, 17, 56, 864, DateTimeKind.Local).AddTicks(3326), "AQAAAAEAACcQAAAAEJY4wnK9UBuFcPRzwOQIYIE7VWNl76JoEZ+vvIrTRIpTJzhQrfHBbSXZk3dd7gckhA==", "3267c0dd-6e68-41b8-8b36-5b6068a1a45b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51a17303-46d7-44f7-9a54-5113c70c9318", new DateTime(2023, 4, 12, 17, 17, 56, 856, DateTimeKind.Local).AddTicks(8852), "AQAAAAEAACcQAAAAEJ0nm5wKyhTL3yPzlGtqDWC/6uMS7I7jDeVJlNKJqFGGMbYoN7hBcKsTdDQoYIlsUQ==", "5e506ef8-5cd4-4af1-8bb0-96cb5177f6ce" });
        }
    }
}
