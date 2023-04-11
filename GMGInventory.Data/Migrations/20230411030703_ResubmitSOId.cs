using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ResubmitSOId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResubmittedSalesOrderId",
                table: "SalesOrders",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResubmittedSalesOrderId",
                table: "SalesOrders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "cbcbd7ff-38b2-4db5-95f2-f614f828b569");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "3a26a502-994d-4b45-aae7-92290a5a3384");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "d0f3f688-ad52-444f-b6c4-e91329deb6ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "eb809739-93a3-42ac-8611-d29291c8a6a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "44aeea54-600a-4c4c-9d35-092428249aa2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "ec558585-9c39-420f-bbd7-f6a54e90eb5a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25492ae0-c970-41fe-8690-aa9fff89c9da", new DateTime(2023, 4, 3, 16, 23, 48, 410, DateTimeKind.Local).AddTicks(3932), "AQAAAAEAACcQAAAAEDE5FkvTxYO+nlG7UPfI1OmvF329BhSzoo3bQ21x9EpviDqLi+6lyciuPtrhfQpzdg==", "ff1c7a09-f349-46f9-a0b6-5aa977fd2892" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42393637-5efe-4028-881c-f447e527dc43", new DateTime(2023, 4, 3, 16, 23, 48, 401, DateTimeKind.Local).AddTicks(390), "AQAAAAEAACcQAAAAEFkGRhbUvM75Ygh001DytTT/k/B06H1NcQ9c3blgPcjBluo/MCvf8XkGjSYhY+yAfg==", "821af640-2754-4242-b9d8-9f39c4ec9b8a" });
        }
    }
}
