using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class AddedAuditLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "66fe2b86-254c-4d96-aa51-052f68ecc3a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "87339da9-24b2-4c2c-96ba-39a5154f8d06");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "813c2e62-9ab0-4a86-a09f-69b5bee34994");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "35ed9899-8963-4b47-8f39-027724190625");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "20ae1ef7-4db6-4483-8bf4-4486e5e7df02");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "d2ce12e4-90f5-4a41-b2e1-9b3d8485c809");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2d2fae9-fb73-43fc-bf36-00c5d7a9cb5d", new DateTime(2022, 7, 14, 14, 45, 41, 255, DateTimeKind.Local).AddTicks(5548), "AQAAAAEAACcQAAAAEMhx5Iaqg4dadgkWCTw/BUCIJ0WTqoWBDhfoLAjuSTG/6pp3CSpIOMDGZGPjKk919Q==", "7ba07b90-12af-4ef5-8581-d05c3a566997" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "160eed35-4015-44bf-819e-a861e4aaa04b", new DateTime(2022, 7, 14, 14, 45, 41, 247, DateTimeKind.Local).AddTicks(8970), "AQAAAAEAACcQAAAAELhWh6ZEH5p4PxN5jz7jmQIFr1XvIJPqBLhrbpu0qaY0rC5H2qPWxmmgSxY1nk6fCw==", "9d34b76f-4153-490a-92df-ea8c537b512c" });
        }
    }
}
