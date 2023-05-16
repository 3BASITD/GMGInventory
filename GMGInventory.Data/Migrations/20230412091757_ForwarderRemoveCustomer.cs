using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ForwarderRemoveCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forwarders_Customers_CustomerId",
                table: "Forwarders");

            migrationBuilder.DropIndex(
                name: "IX_Forwarders_CustomerId",
                table: "Forwarders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Forwarders");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Forwarders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "cd7a1f58-dea1-483f-92b8-287e08401c82");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "f10f7913-d0ae-4026-ae54-80579edd1cfd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "e46e9db5-b671-4138-8b1a-4488654ba746");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "791cb249-6e7a-47c3-863c-574d3d1c4e98");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "5a6b911d-be0a-41d4-af53-b775941c98fc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "6f3da51b-a3fc-45d5-beb9-1b0d6aa9427e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b6db95e-6aa2-4082-8365-d423c7117179", new DateTime(2023, 4, 11, 11, 7, 2, 820, DateTimeKind.Local).AddTicks(3930), "AQAAAAEAACcQAAAAEFIH+ynfCECKDyXAHWh+m5BDqu7Spz7WU7omYQ+FIdRDVcmzyW3rFLL0lYRshIPkEQ==", "4160f787-17f6-408e-9a43-76ed7e5f70ae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b53d949-5423-4099-befb-ef1d655cc135", new DateTime(2023, 4, 11, 11, 7, 2, 813, DateTimeKind.Local).AddTicks(3181), "AQAAAAEAACcQAAAAEHmgbwSdDqmm4NLk4mUMBq68KaJQoMdbnLGypHt755eZSce3LdJfVaapyE8kARLulQ==", "2c419f57-4058-47d0-a680-49dde50b1f7d" });

            migrationBuilder.CreateIndex(
                name: "IX_Forwarders_CustomerId",
                table: "Forwarders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forwarders_Customers_CustomerId",
                table: "Forwarders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
