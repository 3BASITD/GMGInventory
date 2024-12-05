using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Procurement9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNewLineItems_PurchaseRequests_PurchaseRequestId",
                table: "PurchaseOrderNewLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNewLineItems_Units_UnitId",
                table: "PurchaseOrderNewLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivingLineItems_Units_UnitId",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_CreditTerms_TermId",
                table: "PurchaseReceivings");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_PurchaseReceivings_Currencies_CurrencyId",
            //    table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_Suppliers_SupplierId",
                table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNewLineItems_Units_UnitId",
                table: "PurchaseReturnNewLineItems");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_PurchaseReturnNews_Currencies_CurrencyId",
            //    table: "PurchaseReturnNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_PurchaseOrderNews_PurchaseOrderId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_Suppliers_SupplierId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_Warehouses_WarehouseId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnNews_PurchaseOrderId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnNewLineItems_UnitId",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivings_TermId",
                table: "PurchaseReceivings");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivingLineItems_UnitId",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderNewLineItems_UnitId",
                table: "PurchaseOrderNewLineItems");

            migrationBuilder.DropColumn(
                name: "CurrencyRate",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "DeliveryReceiptNumber",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "SalesInvoiceNumber",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "ItemDescription",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.DropColumn(
                name: "LineNumber",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.DropColumn(
                name: "UnitCost",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.DropColumn(
                name: "CurrencyRate",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "Discount1",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "Discount2",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "ItemDescription",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropColumn(
                name: "LineNumber",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropColumn(
                name: "UnitCost",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropColumn(
                name: "ItemDescription",
                table: "PurchaseOrderNewLineItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "PurchaseOrderNewLineItems");

            migrationBuilder.RenameColumn(
                name: "PurchaseRequestId",
                table: "PurchaseOrderNewLineItems",
                newName: "RequestItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderNewLineItems_PurchaseRequestId",
                table: "PurchaseOrderNewLineItems",
                newName: "IX_PurchaseOrderNewLineItems_RequestItemId");

            migrationBuilder.AddColumn<DateTime>(
                name: "SalesOrderDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ReceivingItemId",
                table: "PurchaseReturnNewLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CreditTermId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "PurchaseReceivingLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PackDate",
                table: "Packs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f09c29fa-aa82-4d13-9997-262cb4065296", "AQAAAAIAAYagAAAAEFKnUThPL59RyOFlz5ALji03ZjnFnE2ab7cCTiqvJs+WeTGZcRqxKCpL2gwNwWdGXA==", "e3595460-0a04-49c9-bbe6-581d62c154bd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d7ebd5d-7dc2-447f-980b-af19acdbce2d", "AQAAAAIAAYagAAAAEAj0pw/f5RXSZGOYecwyIwA8utqtkx3fFmhmX8tIaRiLnTZKGT6C7JOhRy/m5yEXYQ==", "9fb71b2c-2d85-4baf-bc4b-92e23969a5ae" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNewLineItems_ReceivingItemId",
                table: "PurchaseReturnNewLineItems",
                column: "ReceivingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnLineItems_PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems",
                column: "PurchaseReceivingLineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_CreditTermId",
                table: "PurchaseReceivings",
                column: "CreditTermId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivingLineItems_OrderItemId",
                table: "PurchaseReceivingLineItems",
                column: "OrderItemId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PurchaseOrderNewLineItems_PurchaseRequestLineItems_RequestItemId",
            //    table: "PurchaseOrderNewLineItems",
            //    column: "RequestItemId",
            //    principalTable: "PurchaseRequestLineItems",
            //    principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivingLineItems_PurchaseOrderNewLineItems_OrderItemId",
                table: "PurchaseReceivingLineItems",
                column: "OrderItemId",
                principalTable: "PurchaseOrderNewLineItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_CreditTerms_CreditTermId",
                table: "PurchaseReceivings",
                column: "CreditTermId",
                principalTable: "CreditTerms",
                principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PurchaseReceivings_Currencies_CurrencyId",
            //    table: "PurchaseReceivings",
            //    column: "CurrencyId",
            //    principalTable: "Currencies",
            //    principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_Suppliers_SupplierId",
                table: "PurchaseReceivings",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnLineItems_PurchaseReceivingLineItems_PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems",
                column: "PurchaseReceivingLineItemId",
                principalTable: "PurchaseReceivingLineItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNewLineItems_PurchaseReceivingLineItems_ReceivingItemId",
                table: "PurchaseReturnNewLineItems",
                column: "ReceivingItemId",
                principalTable: "PurchaseReceivingLineItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_Currencies_CurrencyId",
                table: "PurchaseReturnNews",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_Suppliers_SupplierId",
                table: "PurchaseReturnNews",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_Warehouses_WarehouseId",
                table: "PurchaseReturnNews",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNewLineItems_PurchaseRequestLineItems_RequestItemId",
                table: "PurchaseOrderNewLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivingLineItems_PurchaseOrderNewLineItems_OrderItemId",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_CreditTerms_CreditTermId",
                table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_Currencies_CurrencyId",
                table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_Suppliers_SupplierId",
                table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnLineItems_PurchaseReceivingLineItems_PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNewLineItems_PurchaseReceivingLineItems_ReceivingItemId",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_Currencies_CurrencyId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_Suppliers_SupplierId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_Warehouses_WarehouseId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnNewLineItems_ReceivingItemId",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnLineItems_PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivings_CreditTermId",
                table: "PurchaseReceivings");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivingLineItems_OrderItemId",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropColumn(
                name: "SalesOrderDate",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ReceivingItemId",
                table: "PurchaseReturnNewLineItems");

            migrationBuilder.DropColumn(
                name: "PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropColumn(
                name: "CreditTermId",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "PurchaseReceivingLineItems");

            migrationBuilder.DropColumn(
                name: "PackDate",
                table: "Packs");

            migrationBuilder.RenameColumn(
                name: "RequestItemId",
                table: "PurchaseOrderNewLineItems",
                newName: "PurchaseRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderNewLineItems_RequestItemId",
                table: "PurchaseOrderNewLineItems",
                newName: "IX_PurchaseOrderNewLineItems_PurchaseRequestId");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrencyRate",
                table: "PurchaseReturnNews",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryReceiptNumber",
                table: "PurchaseReturnNews",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SalesInvoiceNumber",
                table: "PurchaseReturnNews",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemDescription",
                table: "PurchaseReturnNewLineItems",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LineNumber",
                table: "PurchaseReturnNewLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitCost",
                table: "PurchaseReturnNewLineItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "PurchaseReturnNewLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrencyRate",
                table: "PurchaseReceivings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount1",
                table: "PurchaseReceivings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount2",
                table: "PurchaseReceivings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemDescription",
                table: "PurchaseReceivingLineItems",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LineNumber",
                table: "PurchaseReceivingLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitCost",
                table: "PurchaseReceivingLineItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "PurchaseReceivingLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemDescription",
                table: "PurchaseOrderNewLineItems",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "PurchaseOrderNewLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58f5d0c6-4d37-401c-9757-c4dc969d617e", "AQAAAAIAAYagAAAAEHyuZYB4zIvCeKDySuyDsWskT6qiy3sWSJifXH9ZxfCdnTom4RFh3u/dDZgGP1r6Cw==", "f18ac569-2c59-448e-8c24-0a04c49f29bf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1a99dda-a967-4fc1-a64a-13cbebc4377c", "AQAAAAIAAYagAAAAEAISUD0UQ78HHgM9LPTbedet7uCQp1qvsUKb7VznFtXS1mMa24hMDjlky5kJDfdX1g==", "f473e7a3-0ece-4f11-8489-6b03320c507e" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_PurchaseOrderId",
                table: "PurchaseReturnNews",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNewLineItems_UnitId",
                table: "PurchaseReturnNewLineItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_TermId",
                table: "PurchaseReceivings",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivingLineItems_UnitId",
                table: "PurchaseReceivingLineItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNewLineItems_UnitId",
                table: "PurchaseOrderNewLineItems",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNewLineItems_PurchaseRequests_PurchaseRequestId",
                table: "PurchaseOrderNewLineItems",
                column: "PurchaseRequestId",
                principalTable: "PurchaseRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNewLineItems_Units_UnitId",
                table: "PurchaseOrderNewLineItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivingLineItems_Units_UnitId",
                table: "PurchaseReceivingLineItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_CreditTerms_TermId",
                table: "PurchaseReceivings",
                column: "TermId",
                principalTable: "CreditTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_Currencies_CurrencyId",
                table: "PurchaseReceivings",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_Suppliers_SupplierId",
                table: "PurchaseReceivings",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNewLineItems_Units_UnitId",
                table: "PurchaseReturnNewLineItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_Currencies_CurrencyId",
                table: "PurchaseReturnNews",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_PurchaseOrderNews_PurchaseOrderId",
                table: "PurchaseReturnNews",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrderNews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_Suppliers_SupplierId",
                table: "PurchaseReturnNews",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_Warehouses_WarehouseId",
                table: "PurchaseReturnNews",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
