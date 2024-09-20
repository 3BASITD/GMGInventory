using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ReasonOfReturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReasonOfReturns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonOfReturns", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc0d7dad-88ca-437e-8dc6-2a29f88a577a", new DateTime(2024, 9, 18, 15, 33, 52, 100, DateTimeKind.Local).AddTicks(207), "AQAAAAIAAYagAAAAEK/XIBdSr7gEJ0EBvaSNkk/8hsEerbC3Yah92ItUeCHbNeRYb+xx8QXNyaZs24N4NA==", "3cb9c4c1-b093-426a-becd-a5b5f49cdc02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "193c513b-b5b7-408f-b096-87d66ed414f1", new DateTime(2024, 9, 18, 15, 33, 51, 976, DateTimeKind.Local).AddTicks(5280), "AQAAAAIAAYagAAAAEJ38SOD1QTOVWdy/2R6LF/jdnzy+KfYaHFOcnb+C/OgggoLVAGYaePTEt9sQsLCEiA==", "8724d9ab-63e9-453f-a843-16f2edab9f7e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReasonOfReturns");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "6ff86e3d-135e-4d71-8248-6d26d194b249");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "7b4f8169-7140-409e-90c0-f0b94bbfd7f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "a3c1a8fd-3c30-4bd5-a3d1-6170fc4e2b15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "57b40d43-3693-43e2-8e6d-4443037b7e27");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "3937c0c1-f063-4a0b-bae6-7d85b321fc73");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "044b59fb-28d0-4e7b-9557-fb5b89374437");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71dace9a-40e0-4ac6-acb8-be5a97034cb7", new DateTime(2023, 6, 16, 23, 0, 51, 489, DateTimeKind.Local).AddTicks(9490), "AQAAAAEAACcQAAAAEIp9WrqO3c0eChus/xoi0QnVUkTcW4r2u22fOkf4WE33luW1rq2jYbJI2ZBdK2FxPQ==", "07d8648a-caa3-4322-aea9-79b0eb0858bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2fb4d776-0ff7-41ea-98eb-f26e2e5806f1", new DateTime(2023, 6, 16, 23, 0, 51, 480, DateTimeKind.Local).AddTicks(9571), "AQAAAAEAACcQAAAAEAY1Ne58aMrbytcvJ4Ifrh02c6BdodnwODCSRm/+Lbqg4m3Kx3unG1sJPgEQyxF8XA==", "e2bffb79-2376-4955-a9aa-6b61507231c1" });
        }
    }
}
