using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class mix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditDebitMemos");

            migrationBuilder.DropTable(
                name: "CreditDebitMemoTypes");

            migrationBuilder.DropColumn(
                name: "OutstandingBalance",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "CreditReferenceNumber",
                table: "SOACRLineItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DebitReferenceNumber",
                table: "SOACRLineItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FreightReferenceNumber",
                table: "SOACRLineItems",
                type: "nvarchar(50)",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "298ad724-cb4e-400b-82d3-299a315c3867", "AQAAAAIAAYagAAAAEP6gFAo72ZcFBwF7GEd6sYkDedHdsd+NwMM32/WtvTB+W00/dh1yLP/eYjQ0CspZnQ==", "8d9e1c84-089d-4971-98d0-7b6a3b4f5388" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "b5db826a-fe61-4cdf-841c-9d924a62fac5", "AQAAAAIAAYagAAAAEG+r+hDsDRpXTXdubzMOs9oVOwUvpyJbRKdMZm74S0Kq+dJrpnV+A6uYjkoDtKEbww==", "046debdb-9f17-43c1-a006-cee0fc43b644" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditReferenceNumber",
                table: "SOACRLineItems");

            migrationBuilder.DropColumn(
                name: "DebitReferenceNumber",
                table: "SOACRLineItems");

            migrationBuilder.DropColumn(
                name: "FreightReferenceNumber",
                table: "SOACRLineItems");

            migrationBuilder.AddColumn<decimal>(
                name: "OutstandingBalance",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "CreditDebitMemoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditDebitMemoTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditDebitMemos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditDebitMemoTypeId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MemoNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditDebitMemos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditDebitMemos_CreditDebitMemoTypes_CreditDebitMemoTypeId",
                        column: x => x.CreditDebitMemoTypeId,
                        principalTable: "CreditDebitMemoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditDebitMemos_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "9ed94c96-7b01-4cbe-a507-2cfb0e17df5b", "AQAAAAIAAYagAAAAEJVyTXLvX3OAmrX25rglcXiaJ/R88oXFD00Ehs49lZoGYQeN30Jj0HW6RlakQp7XbQ==", "fcfd0674-2d40-4d22-82f4-1303a33a3888" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d385a0d9-c69b-4215-8064-3fa50475ed7f", "AQAAAAIAAYagAAAAENPA1H5pl+MYmm7Vu9+trOjT3qL0gYcZ1RIFC49zodK1Xfia1b8B+Hh6jvtWfKsDKg==", "67a20266-c980-4fc7-a83c-b365c034ddd5" });

            migrationBuilder.InsertData(
                table: "CreditDebitMemoTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "CR", "Credit" },
                    { 2, "DB", "Debit" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditDebitMemos_CreditDebitMemoTypeId",
                table: "CreditDebitMemos",
                column: "CreditDebitMemoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditDebitMemos_InvoiceId",
                table: "CreditDebitMemos",
                column: "InvoiceId");
        }
    }
}
