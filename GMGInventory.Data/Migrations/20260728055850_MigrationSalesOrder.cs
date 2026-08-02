using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrationSalesOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResubmittedBy",
                table: "SalesOrders",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResubmittedDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmittedBy",
                table: "SalesOrders",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1,
                column: "HasUploadDownload",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResubmittedBy",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ResubmittedDate",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "SubmittedBy",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "SubmittedDate",
                table: "SalesOrders");

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1,
                column: "HasUploadDownload",
                value: false);
        }
    }
}
