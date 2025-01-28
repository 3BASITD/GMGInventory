using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class shipping4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShippingLineItems");

            migrationBuilder.DropTable(
                name: "Shippings");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "ShippingAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SalesOrderId",
                table: "SalesReturns",
                type: "int",
                nullable: false,
                defaultValue: 297126);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "15281c46-51ad-46b6-b8ce-d7382e66a195", "AQAAAAIAAYagAAAAEOjXTlhhICi+7ZAAA86cL8AFoJo1UIFVVlt0/961GtrTimkRdn1ThqRbPzwxC9HgPA==", "e1089ed4-991e-4bf1-9e5a-0ed0b506ad37" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "09382787-3ed9-45e6-b766-b1cb986a3976", "AQAAAAIAAYagAAAAEN2UwPbP8DkS4tAvTOlqZ9TMjIDrYlD5H7H6Y7Tq+PHjnO5NAvszh1B2uLS61MaP+w==", "2c541545-e1ef-4f65-ba86-6875f95e85c3" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_SalesOrderId",
                table: "SalesReturns",
                column: "SalesOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_SalesOrders_SalesOrderId",
                table: "SalesReturns",
                column: "SalesOrderId",
                principalTable: "SalesOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_SalesOrders_SalesOrderId",
                table: "SalesReturns");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturns_SalesOrderId",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "SalesOrderId",
                table: "SalesReturns");

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesOrderId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shippings_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceLineItemId = table.Column<int>(type: "int", nullable: false),
                    ShippingId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingLineItems_InvoiceLineItems_InvoiceLineItemId",
                        column: x => x.InvoiceLineItemId,
                        principalTable: "InvoiceLineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShippingLineItems_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "652369ec-c4c7-444f-95ac-80dd4d68e3f0", "AQAAAAIAAYagAAAAEPJLya1xuDxeSQ0hn5nzH6hFNN9yF64sW5JinUHPo2VSRCLejTU7TPINP0gX5ilpnA==", "3e833254-366b-4629-946d-c2a5a928e02e" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "8f0f50e0-d13f-4c56-a33b-0cf2cc498004", "AQAAAAIAAYagAAAAEL4mNjWibZvH6jjLfLBHKmP2qjXgORPfM905sn4j1Psh38DOkk0ygQnVAkD6eH4P6Q==", "89acd5d5-60ce-4ef6-af1d-b5c41e222e4d" });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingLineItems_InvoiceLineItemId",
                table: "ShippingLineItems",
                column: "InvoiceLineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingLineItems_ShippingId",
                table: "ShippingLineItems",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_SalesOrderId",
                table: "Shippings",
                column: "SalesOrderId");
        }
    }
}
