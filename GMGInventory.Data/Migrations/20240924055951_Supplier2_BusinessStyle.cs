using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Supplier2_BusinessStyle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Areas_AreaId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CreditLimit",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IsForeign",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Suppliers");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Suppliers",
                newName: "Terms");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Suppliers",
                newName: "PriceList");

            migrationBuilder.RenameColumn(
                name: "Barangay",
                table: "Suppliers",
                newName: "OfficeTelNo");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Suppliers",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_AreaId",
                table: "Suppliers",
                newName: "IX_Suppliers_RegionId");

            migrationBuilder.AddColumn<int>(
                name: "BusinessStyleId",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TIN",
                table: "Suppliers",
                type: "nvarchar(12)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VAT",
                table: "Suppliers",
                type: "nvarchar(12)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BusinessStyle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessStyle", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "149f13c9-3262-4b8a-a4cd-14725bb6cc89", new DateTime(2024, 9, 24, 13, 59, 50, 649, DateTimeKind.Local).AddTicks(5801), "AQAAAAIAAYagAAAAENMoa5JxSWk6DMAZWblr9ZLBC/UTJ7/eA9qgkyuEu7Rm5ZzqXOzy4b6yeiurMaj32g==", "cb6365fc-7552-4ee6-a99a-687c6713200d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26ff7328-5d7f-4671-a8a0-92af4ebcff80", new DateTime(2024, 9, 24, 13, 59, 50, 517, DateTimeKind.Local).AddTicks(3303), "AQAAAAIAAYagAAAAEDtATRiJ87x1qcGerMiHBbP+qhGIcWDjc+ANkkRujwLwxjk2KYmK4XNLZPNfdyJrCg==", "2ae8c9a1-5b51-44b4-89a7-aac6320d460a" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 9, 24, 13, 59, 50, 415, DateTimeKind.Local).AddTicks(3289));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 9, 24, 13, 59, 50, 415, DateTimeKind.Local).AddTicks(3336));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 9, 24, 13, 59, 50, 415, DateTimeKind.Local).AddTicks(3339));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 9, 24, 13, 59, 50, 415, DateTimeKind.Local).AddTicks(3341));

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_BusinessStyleId",
                table: "Suppliers",
                column: "BusinessStyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_BusinessStyle_BusinessStyleId",
                table: "Suppliers",
                column: "BusinessStyleId",
                principalTable: "BusinessStyle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Regions_RegionId",
                table: "Suppliers",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_BusinessStyle_BusinessStyleId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Regions_RegionId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "BusinessStyle");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_BusinessStyleId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "BusinessStyleId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "TIN",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Suppliers");

            migrationBuilder.RenameColumn(
                name: "Terms",
                table: "Suppliers",
                newName: "EmailAddress");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Suppliers",
                newName: "AreaId");

            migrationBuilder.RenameColumn(
                name: "PriceList",
                table: "Suppliers",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "OfficeTelNo",
                table: "Suppliers",
                newName: "Barangay");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_RegionId",
                table: "Suppliers",
                newName: "IX_Suppliers_AreaId");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Suppliers",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CreditLimit",
                table: "Suppliers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Suppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsForeign",
                table: "Suppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Suppliers",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "306da8cf-4a9c-4c8c-b37e-244507d2504e", new DateTime(2024, 9, 20, 10, 2, 33, 363, DateTimeKind.Local).AddTicks(5091), "AQAAAAIAAYagAAAAEN0Sby7eGZ/Q9iqxNKP7JmRwWqliS+OU/hrnWcZN0ohi0NIBnTFWMcCqgqVNTFUGQA==", "37707739-0a92-4b20-bb90-3f5fc1975f77" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cef40c76-a0c1-426e-94ed-5c62393fb816", new DateTime(2024, 9, 20, 10, 2, 33, 278, DateTimeKind.Local).AddTicks(5590), "AQAAAAIAAYagAAAAEOIgOhxZc7SYslNgtyA1hgcY49RvHy0WkdQN99Z1v8sBIz1h/M+cMTSeynQaLAXCKw==", "b940b505-f899-4a80-840d-7733444c3bc8" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 9, 20, 10, 2, 33, 192, DateTimeKind.Local).AddTicks(7168));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 9, 20, 10, 2, 33, 192, DateTimeKind.Local).AddTicks(7187));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 9, 20, 10, 2, 33, 192, DateTimeKind.Local).AddTicks(7190));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 9, 20, 10, 2, 33, 192, DateTimeKind.Local).AddTicks(7192));

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Areas_AreaId",
                table: "Suppliers",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
