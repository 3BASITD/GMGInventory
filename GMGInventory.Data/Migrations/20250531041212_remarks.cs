using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class remarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PurchaseOrderNews_Currencies_CurrencyId",
            //    table: "PurchaseOrderNews");

            //migrationBuilder.DropIndex(
            //    name: "IX_PurchaseOrderNews_CurrencyId",
            //    table: "PurchaseOrderNews");

            //migrationBuilder.DropColumn(
            //    name: "CurrencyId",
            //    table: "PurchaseOrderNews");

            //migrationBuilder.DropColumn(
            //    name: "CurrencyRate",
            //    table: "PurchaseOrderNews");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "TransferItems",
                type: "nvarchar(501)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "SalesReturns",
                type: "nvarchar(501)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "SalesOrders",
                type: "nvarchar(501)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(501)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseReturns",
                type: "nvarchar(501)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseReturnNews",
                type: "nvarchar(501)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseRequests",
                type: "nvarchar(501)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseReceivings",
                type: "nvarchar(501)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseOrderNews",
                type: "nvarchar(501)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PhysicalCountHistories",
                type: "nvarchar(501)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Packs",
                type: "nvarchar(501)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Invoices",
                type: "nvarchar(501)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Customers",
                type: "nvarchar(501)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CustomerPayments",
                type: "nvarchar(501)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "2f00c8da-eea4-4c33-a5eb-4d07c6362fb9", "AQAAAAIAAYagAAAAEMlyIkGsEk+WULHxl5zYelaMft/T2NppiWoVkFGBFg8ygBCsFIg/I7v/ckRo+MlVsQ==", "a84b4081-ad37-4652-8d41-f3088c353cf7" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "2a9903ce-c1d0-4704-bb5c-799e9024518d", "AQAAAAIAAYagAAAAELtOjBHZ3EdtHHc9ehavYdUhFDKv+5ErALTlF1eou2gFcGxTfxnVSus1axdhOM3feA==", "3ba31eb4-4eb6-435c-a438-0dce229b823f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "TransferItems",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "SalesReturns",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "SalesOrders",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseReturns",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseReturnNews",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseRequests",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseReceivings",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseOrderNews",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "PurchaseOrderNews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrencyRate",
                table: "PurchaseOrderNews",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PhysicalCountHistories",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Packs",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Invoices",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Customers",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "CustomerPayments",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(501)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d3e79f54-e3f2-4d24-954d-b5c6c8f2e67c", "AQAAAAIAAYagAAAAEEx7y1c1yMNgRjr0nSodHxfluAc4I9MX5Lz/68Ov63tZryN7DzlUf8hi5DrCJUsaOA==", "02ff0ac7-ba07-4049-814a-d03e045936c4" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "3d04e281-3249-4742-b846-c9ec50c5c11e", "AQAAAAIAAYagAAAAEHiFiXyWc5qtCow545QwTeYlT8uJV+dK6PecfTX/AjcUYRdMMuf42DK7eF5jfjARGQ==", "16e91e32-dd7c-4311-86fa-05c8f5e4aa8b" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_CurrencyId",
                table: "PurchaseOrderNews",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_Currencies_CurrencyId",
                table: "PurchaseOrderNews",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
