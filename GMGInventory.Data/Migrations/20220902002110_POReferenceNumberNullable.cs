using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class POReferenceNumberNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "PurchaseOrders",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "49237c0e-6922-4157-97b4-8bb7ae75f4e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "b5cd02ec-d17c-4223-a056-58c1b33e5c3f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "1e872984-779f-4a2e-94f0-ca163bae6cec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "d55e7086-d075-4990-a197-e4f4c4090c8d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "9c82f84b-0250-4820-a497-f0861ecf8851");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "eae5273d-f2b7-422e-ac69-c129d7b719f4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edecf6c7-39c7-4dc7-b7c4-0829eaa84fe9", new DateTime(2022, 9, 2, 8, 21, 9, 776, DateTimeKind.Local).AddTicks(3409), "AQAAAAEAACcQAAAAEPi0zkrZozJ9HF/iBlYdFlpRoZc3n1whUAF7LXJ02damgwvqP8VxXaCzTseFypD+IA==", "fe8891c8-e928-4dd1-854a-0c5a2ecccaec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b15d311-e9b1-4e33-b006-096e50c03ed1", new DateTime(2022, 9, 2, 8, 21, 9, 769, DateTimeKind.Local).AddTicks(3078), "AQAAAAEAACcQAAAAEGTU+ywuZ+gxQW1TaES43ZaLVg4hLwIbq+/SVxT/FYghSm9YqsX+OUvIyfXiMjALVQ==", "37e96513-03ac-4b20-99d7-8caf438dfd99" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                table: "PurchaseOrders",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "264ce80f-4121-4169-94f0-30ef43b8ba0c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "569b38d2-2af7-4a8f-8822-1e9bccd53d47");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "d927e31c-47b8-451b-a37b-004f275d1c82");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "89eaeed8-e460-4d80-848e-7b3c9cb68f71");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "a5f56381-776f-4532-bc00-c09bdb54c6c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "71997481-1db3-402a-9dd7-1d79f50ab0ba");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ed0f4d3-a59a-4ede-a83e-839ecb400d9c", new DateTime(2022, 8, 31, 18, 16, 27, 516, DateTimeKind.Local).AddTicks(2241), "AQAAAAEAACcQAAAAEKSnGmlPG94eBaKmgRZZkxsWBwIE+dIBQ+cHOora24p6l3GoT3Z+oAlDYISAwYe3Eg==", "8df0200a-9340-4b83-9d03-01de704618a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a017ef78-1f91-4508-85ed-8a32e3ed0f17", new DateTime(2022, 8, 31, 18, 16, 27, 509, DateTimeKind.Local).AddTicks(285), "AQAAAAEAACcQAAAAEKuaL9ZS2lSzd07kFrPqrqZUfCfMzbSomdrjPxQPOgZG0CjPlLhmmXgYguPku0J/Jw==", "15518c9d-5ecc-49c1-80b5-f08e991181ca" });
        }
    }
}
