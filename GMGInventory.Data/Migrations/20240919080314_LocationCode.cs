using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class LocationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationCodes",
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
                    table.PrimaryKey("PK_LocationCodes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8950ca25-7ac3-4450-b976-89ee4aac3395", new DateTime(2024, 9, 19, 16, 3, 13, 477, DateTimeKind.Local).AddTicks(3444), "AQAAAAIAAYagAAAAEK/yU5uHpbm2VwWip0jlt6Cup9SJ/wbn9HM7l5jgcimCE9yZlqFSmFDalVYLudXxMw==", "939ede76-8f6f-452e-b96c-b573594a8d8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e7c67b1-f7c4-4c45-937c-f76ed61d6018", new DateTime(2024, 9, 19, 16, 3, 13, 373, DateTimeKind.Local).AddTicks(2114), "AQAAAAIAAYagAAAAEMWBG2/3FEvgNMtWhpZQyONxvNfRZ0uEj6xrCYlpHx/C4dRla/yCBp+HYHTIKh+oWA==", "77f5445b-5491-4f45-a6d8-7697f440dc53" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationCodes");

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
    }
}
