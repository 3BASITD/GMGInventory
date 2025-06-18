using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ponewquantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseOrderNewLineItemNewQuantities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemId = table.Column<int>(type: "int", nullable: false),
                    PreviousQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderNewLineItemNewQuantities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderNewLineItemNewQuantities_PurchaseOrderNewLineItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "PurchaseOrderNewLineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d070506-4719-4ad5-aeee-0b03cddff36e", "AQAAAAIAAYagAAAAEJp7JFhmeOPr4Og+1a15jHioudfThq6dRDeoZJdv/uUcWuO8wYWxQvQbHHuZy6i2lg==", "91d9df42-02c9-4f5f-b897-cd39b2bbe07d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f587165-8826-4196-97f6-7f2f1efdb128", "AQAAAAIAAYagAAAAEAXeqkKQ+f463dJ3xQKQvEUB2GSBDcb7xJ5vmHRAhccMADglba1J+1iZFboqnEuMWA==", "b6a6c3d3-4ff8-4a93-871c-bc0f551bd0ed" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNewLineItemNewQuantities_OrderItemId",
                table: "PurchaseOrderNewLineItemNewQuantities",
                column: "OrderItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrderNewLineItemNewQuantities");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a376d31-865c-4c71-b347-90348f41a132", "AQAAAAIAAYagAAAAEKwUI0DseZGzPltrSk1Bm3c8wMHFS60O/DQ6+KecqDW+vFeMeC4BML6gxqERJFbXrA==", "d7eea664-934c-447f-9662-c4ea9cf07c56" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f9a8c560-2173-491a-8157-ffa799d190dc", "AQAAAAIAAYagAAAAEIaOHcttCQKBq0+LNc1erp7nSiluN54LWd23pUfqiUQyaxQIBJUBsVg7027UJfOqiw==", "17407a54-10a0-4aaf-bb2a-2cfa8b0bb38f" });
        }
    }
}
