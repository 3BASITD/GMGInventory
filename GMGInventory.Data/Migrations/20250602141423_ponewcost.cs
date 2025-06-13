using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ponewcost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseOrderNewLineItemNewCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemId = table.Column<int>(type: "int", nullable: false),
                    PreviousCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderNewLineItemNewCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNewLineItemNewCosts_PurchaseOrderNewLineItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "PurchaseOrderNewLineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "8a376d31-865c-4c71-b347-90348f41a132", "AQAAAAIAAYagAAAAEKwUI0DseZGzPltrSk1Bm3c8wMHFS60O/DQ6+KecqDW+vFeMeC4BML6gxqERJFbXrA==", "d7eea664-934c-447f-9662-c4ea9cf07c56" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f9a8c560-2173-491a-8157-ffa799d190dc", "AQAAAAIAAYagAAAAEIaOHcttCQKBq0+LNc1erp7nSiluN54LWd23pUfqiUQyaxQIBJUBsVg7027UJfOqiw==", "17407a54-10a0-4aaf-bb2a-2cfa8b0bb38f" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNewLineItemNewCosts_OrderItemId",
                table: "PurchaseOrderNewLineItemNewCosts",
                column: "OrderItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrderNewLineItemNewCosts");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "38a85769-c5ca-4f92-ac58-27f6befe72b5", "AQAAAAIAAYagAAAAEDMpkj6V9SEBJwWTzxH8ZD6RdS0UR5fIkWT0KdTKg3BEUZmvEYPQ8futIjiofHtzqg==", "961a42c7-096e-4d8f-a95c-1c1cee058f1e" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "316143f5-48aa-4373-a00e-0b43cab2c6af", "AQAAAAIAAYagAAAAEG8wSxdTke+bThxhjpuZtWJ8cSILRS4BkIGBp8ULOpEwNO7EEN2Vbwd1syUuWvEWdg==", "c9938fbc-7d6f-4275-8910-a861e81e4084" });
        }
    }
}
