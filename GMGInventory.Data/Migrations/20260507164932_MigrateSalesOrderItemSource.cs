using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateSalesOrderItemSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesOrderItemSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceOrderNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SalesOrderId = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderItemSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderItemSources_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesOrderItemSources_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0fcbe40-0582-4675-afbc-fc8760409e09", "AQAAAAIAAYagAAAAEImTrEnDXlBNxOrukdttP5HYTDybEZbTBQFO24XxCcZcRB9fwTh8vexKaXTItHmaxA==", "b3c83572-94cc-4db4-b367-d6c40d0fa879" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf63aa7a-a885-4ba4-824a-5032a15eff63", "AQAAAAIAAYagAAAAEGqMjKqRcJLFs46l5fyytwliNU//I3sYoX9kuWZyqUwK7mfpp+/pM6fdkaB5630zWg==", "e2ff93dc-88f9-488a-9527-1bbfc33ec4d7" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItemSources_ItemId",
                table: "SalesOrderItemSources",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItemSources_SalesOrderId",
                table: "SalesOrderItemSources",
                column: "SalesOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesOrderItemSources");

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
        }
    }
}
