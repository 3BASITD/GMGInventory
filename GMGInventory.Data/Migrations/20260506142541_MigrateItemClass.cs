using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateItemClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemClassId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemClasses", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d18695b-be6d-45cc-9758-0096087e96b9", "AQAAAAIAAYagAAAAEOeQ0fiQqq3s+Demed7MV3070RV68R6ljDBPSqx5lmAHesMIxQQCpXdMgN7njrv27Q==", "4a2c419c-82a5-4c52-b095-e3ebd26a5ef9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4cd5e72d-6165-4e47-a330-06fd372c0ce2", "AQAAAAIAAYagAAAAENm5IFhP44tDkRlOb9XHvVVYY9vCIEQa0c0oWRXVGfBXLQGDKwhlhxENG/HwzgVwwA==", "6455e60c-e18d-44a6-90d4-ac36edace26d" });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemClassId",
                table: "Items",
                column: "ItemClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemClasses_ItemClassId",
                table: "Items",
                column: "ItemClassId",
                principalTable: "ItemClasses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemClasses_ItemClassId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemClasses");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemClassId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemClassId",
                table: "Items");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de18ebf0-eeab-4793-9551-9a0b6648806b", "AQAAAAIAAYagAAAAEP+JGeCWnNnIVEW3OYclKbv9jGRyoIBN3j024x7MPpInrf/6/BphW+xZvTLJIkqA0w==", "abda63c5-f9e8-448b-a2ca-4da498b33c6d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "890ff2b3-d704-4c9e-925f-6b0e80b56635", "AQAAAAIAAYagAAAAEBVRn3gUxUDnzxrrnniuWUAcyTu6o9nD+tGMHwlP68sqJUsZm8ZdTcG9u7KeGKgEjg==", "c8bfdc49-77e5-4dc6-a25c-d8a5234e772b" });
        }
    }
}
