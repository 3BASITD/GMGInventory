using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class BusinessStyle2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_BusinessStyle_BusinessStyleId",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessStyle",
                table: "BusinessStyle");

            migrationBuilder.RenameTable(
                name: "BusinessStyle",
                newName: "BusinessStyles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessStyles",
                table: "BusinessStyles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24dd9dd7-ff55-4710-a93e-f13847d787fd", new DateTime(2024, 9, 24, 15, 8, 17, 808, DateTimeKind.Local).AddTicks(4654), "AQAAAAIAAYagAAAAEHBpGExfpyVcNH2CPSKtXR82NPqMmCrtU0YOKhuuYGZnhzNz/XtlAWy9AjrAJ0pnoA==", "87e9c421-5b82-4f11-b426-988b0c0eb16a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7139ee1-95ca-4120-be38-2cca2217509f", new DateTime(2024, 9, 24, 15, 8, 17, 676, DateTimeKind.Local).AddTicks(9873), "AQAAAAIAAYagAAAAEPApT+7R3SL506vYUrfYf7s1A+yjI7/tD12hAXQHN0n5nlPuJuL8K2uoM0MFgcQpxg==", "728d4639-e6c2-4b26-989d-c0c42276293f" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 9, 24, 15, 8, 17, 580, DateTimeKind.Local).AddTicks(9301));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 9, 24, 15, 8, 17, 580, DateTimeKind.Local).AddTicks(9319));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 9, 24, 15, 8, 17, 580, DateTimeKind.Local).AddTicks(9321));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 9, 24, 15, 8, 17, 580, DateTimeKind.Local).AddTicks(9323));

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_BusinessStyles_BusinessStyleId",
                table: "Suppliers",
                column: "BusinessStyleId",
                principalTable: "BusinessStyles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_BusinessStyles_BusinessStyleId",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessStyles",
                table: "BusinessStyles");

            migrationBuilder.RenameTable(
                name: "BusinessStyles",
                newName: "BusinessStyle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessStyle",
                table: "BusinessStyle",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_BusinessStyle_BusinessStyleId",
                table: "Suppliers",
                column: "BusinessStyleId",
                principalTable: "BusinessStyle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
