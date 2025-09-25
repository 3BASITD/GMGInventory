using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class duedatecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "PurchaseOrderNews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "PurchaseOrderNews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Invoices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerBrandDiscount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBrandDiscount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerBrandDiscount_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerBrandDiscount_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3393f162-22d4-49ac-aa47-6775aeda0ba4", "AQAAAAIAAYagAAAAEJCfvX4j/Kc0hZuWAdvG8BOrhD8g52+mplP76SXg5MABB/KoYkdXWBfmKa/wXL+4Sg==", "d25cb937-7672-4556-ba51-7b958b7fafa3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa717c0d-46eb-45fc-bcfb-3651dd149bf6", "AQAAAAIAAYagAAAAEGIdo+ILfQ0Em/1gz5kOl6YuEhs9tOIs6xLC6e6KAFXeTNQ5/r+eESNzUW/Pvw1LVA==", "fc8da272-36bf-407d-9371-8bd56560c266" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBrandDiscount_BrandId",
                table: "CustomerBrandDiscount",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBrandDiscount_CustomerId",
                table: "CustomerBrandDiscount",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerBrandDiscount");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "PurchaseOrderNews");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "PurchaseOrderNews");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26929b92-3d47-403e-9e5f-baebef24b6bf", "AQAAAAIAAYagAAAAEGmx+h6365ajrEXv0w9W0yoeTo3QW3SC+wUxo9x9n7GIp54iCJqirPkPwePfO9q5Ew==", "712905b7-fb5f-400f-a77c-3abd3ebadcb7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bb20b61-dc11-46ab-a501-a6e392930350", "AQAAAAIAAYagAAAAEMjxwfuwjVmvhfixUQ9BlkUxNjYf9mK0Ze4AnAZdUDsZEd5AuuApp8bYrxlfeJ7wcg==", "8a96d5e5-8200-4e0c-b395-adc65ddb4ac6" });
        }
    }
}
