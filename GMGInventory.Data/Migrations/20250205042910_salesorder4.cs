using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesorder4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_PriceTypes_PriceTypeId",
                table: "SalesOrders");

            migrationBuilder.DropTable(
                name: "PriceTypes");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_PriceTypeId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "PriceTypeId",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ForwarderId",
                table: "SalesOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Waybill",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.AlterColumn<string>(
                name: "Representative",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "1df09f6a-434d-48d6-90e9-050a478ebf1b", "AQAAAAIAAYagAAAAEORO7y6jtTr9/wGmdsFB59e4yyWiFtDTPpWogxu57OgFHQcK8DRE2jJDzpEgwP8S7g==", "cb2cdadf-782b-4385-87e9-13d66677dc95" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "82371061-ea2e-45f6-8f8b-d70cbaca9b53", "AQAAAAIAAYagAAAAEOLXeOvlfSsKKGy+nRXqOZ4K0bm8B3pgrREYhUYznuaXkpo7ZVlq+XnXnh0W+qeUuw==", "fb4cff94-37d9-479d-8196-1243cdcf8ddd" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders",
                column: "ForwarderId",
                principalTable: "Forwarders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ForwarderId",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceTypeId",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Waybill",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Representative",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PriceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTypes", x => x.Id);
                });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "9a73d8d1-f827-466a-87c5-db88133eb49a", "AQAAAAIAAYagAAAAEPGsjlt3n8Rh6ZdmySXBFOR9MPJhN7Qgk9X+c5o1p2KHW30W7Ft1lWYsDaLp3b6QAw==", "6c1064df-d341-41b8-96ec-ac78ef344c26" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e2e149ed-ef3a-4154-a59b-97a6cf06a865", "AQAAAAIAAYagAAAAEDxAXyGo9R2G6nH3FrQnwc1gZ+DOUvpj74MQKYJ54z9qI0UWQ9VYXKvPwWz3Epi93g==", "81aa3360-67ba-4fff-82f5-f402f25cc021" });

            migrationBuilder.InsertData(
                table: "PriceTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "GROSS", "GROSS PAYMENT" },
                    { 2, "NET", "NET PAYMENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_PriceTypeId",
                table: "SalesOrders",
                column: "PriceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders",
                column: "ForwarderId",
                principalTable: "Forwarders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_PriceTypes_PriceTypeId",
                table: "SalesOrders",
                column: "PriceTypeId",
                principalTable: "PriceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
