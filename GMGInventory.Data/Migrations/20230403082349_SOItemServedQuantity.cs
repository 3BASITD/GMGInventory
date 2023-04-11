using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SOItemServedQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityServed",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "QuantityUnserved",
                table: "SalesOrders");

            migrationBuilder.AddColumn<int>(
                name: "ServedQuantity",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnservedQuantity",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServedQuantity",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "UnservedQuantity",
                table: "SalesOrderLineItems");

            migrationBuilder.AddColumn<int>(
                name: "QuantityServed",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityUnserved",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "636ee7e9-2c37-435e-bc25-d25197737df7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "0673f746-9f09-4594-8aac-230356e1a8ea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "afd08cde-f2df-418b-85b7-5c24eb3fd44d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "1ed629a5-2705-43b5-ae67-04aa450d855e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "bc94f43d-0a2d-4d32-b119-a6209af4e845");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "370e6b59-43b7-454a-a4f6-45197273087e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce0a7925-b830-4087-9690-8152bfad39dc", new DateTime(2023, 4, 3, 15, 59, 15, 503, DateTimeKind.Local).AddTicks(6812), "AQAAAAEAACcQAAAAEF/2XinCnlY8CW/4iuuuVoxvHxZTUck5vAz0H7Cl0aavE9eE4jfYX7J7MIDSaoAUhw==", "78833e97-a76e-4b67-9a7f-e270c6ff7c63" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cb109aa-a0dc-4384-a89e-d0625157f561", new DateTime(2023, 4, 3, 15, 59, 15, 496, DateTimeKind.Local).AddTicks(3233), "AQAAAAEAACcQAAAAEHtzoqLjHFc8mPfhAO3q1PXZLHLvVGAlKeeqxj49RPtOl/NKQ3MEktMa5hnwYJgnnQ==", "54957602-b773-4b14-a122-aa59e7aeb68a" });
        }
    }
}
