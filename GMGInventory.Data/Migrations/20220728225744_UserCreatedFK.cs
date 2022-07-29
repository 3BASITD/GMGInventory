using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class UserCreatedFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "TransferItemDetails");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "TransferItemDetails");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "StockCards");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "StockCards");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "SOACRs");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "SOACRs");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "SOACRLineItems");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "SOACRLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "SOACRChecks");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "SOACRChecks");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "SalesReturnTotalReturnAmounts");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "SalesReturnTotalReturnAmounts");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "SalesReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "SalesReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "PurchaseReturns");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "PurchaseReturns");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "PhysicalCountHistories");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "PhysicalCountHistories");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "PackReturns");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "PackReturns");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "PackReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "PackReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "PackLineItems");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "PackLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "ItemInventories");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "ItemInventories");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "InvoiceLineItems");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "InvoiceLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Forwarders");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Forwarders");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "CustomerPayments");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "CustomerPayments");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "CustomerPaymentDetails");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "CustomerPaymentDetails");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "CreditTerms");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "CreditTerms");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "CreditDebitMemos");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "CreditDebitMemos");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Agents");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Warehouses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Warehouses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "TransferItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "TransferItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "TransferItemDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "TransferItemDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Suppliers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Suppliers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "StockCards",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "StockCards",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "SOACRs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "SOACRs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "SOACRLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "SOACRLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "SOACRChecks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "SOACRChecks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "ShippingAddresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "ShippingAddresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "SalesReturnTotalReturnAmounts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturnTotalReturnAmounts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "SalesReturns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "SalesReturnLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturnLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "SalesReturnAppliedAmounts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturnAppliedAmounts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "SalesOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "SalesOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "SalesOrderLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "SalesOrderLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "PurchaseReturns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseReturns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "PurchaseReturnLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseReturnLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "PurchaseOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "PurchaseOrderLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseOrderLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "PhysicalCountHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "PhysicalCountHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "PaymentDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "PaymentDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Packs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Packs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "PackReturns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "PackReturns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "PackReturnLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "PackReturnLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "PackLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "PackLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "ItemTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "ItemTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "ItemPrices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "ItemPrices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "ItemInventories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "ItemInventories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "InvoicePayments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "InvoicePayments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "InvoiceLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "InvoiceLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Forwarders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Forwarders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "CustomerPayments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "CustomerPayments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "CustomerPaymentDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "CustomerPaymentDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "CreditTerms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "CreditTerms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "CreditDebitMemos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "CreditDebitMemos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Brands",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Brands",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuditLogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UsrCreated",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsrModified",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Areas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Areas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreatedId",
                table: "Agents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModifiedId",
                table: "Agents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "33da9de2-d15b-4cf2-b2cd-7ee9ef263664");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "570c993f-e912-4b5d-a13c-d38fc2590425");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "3497a6fc-ff4f-40b6-90ea-c92cd9fba8b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "95dae74e-b6d5-4a7c-bae9-51556dbfe8a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "b5ee04d7-40db-4504-90f4-c2211e38bf6c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "6cc41dc0-e60d-4d79-af4b-55eb79d05a8a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp", "UsrCreated" },
                values: new object[] { "14fe3830-d43e-4bc2-ac2e-f8b0a273d5d1", new DateTime(2022, 7, 29, 6, 57, 43, 445, DateTimeKind.Local).AddTicks(4693), "AQAAAAEAACcQAAAAEFFTc5548OkbxxYkoKIxa7pirAfCEzTVxxCcbJ1bPMkUIhrEkgmNNauY0hQ5BFTVWQ==", "7c61a63c-3390-4306-a101-71b96e7e58e4", "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp", "UsrCreated" },
                values: new object[] { "acb78d30-3680-4425-80ae-cfd91afe37ab", new DateTime(2022, 7, 29, 6, 57, 43, 435, DateTimeKind.Local).AddTicks(8787), "AQAAAAEAACcQAAAAEBFXMn3CM9l5on1F1fhlyx9yMF8ostt3CsFqBTN1t79cSVe93mDwehalpefjHOO22w==", "8295cda3-40a9-4777-b383-0c8a47b61120", "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_UserCreatedId",
                table: "Warehouses",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_UserModifiedId",
                table: "Warehouses",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_UserCreatedId",
                table: "TransferItems",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_UserModifiedId",
                table: "TransferItems",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItemDetails_UserCreatedId",
                table: "TransferItemDetails",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItemDetails_UserModifiedId",
                table: "TransferItemDetails",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_UserCreatedId",
                table: "Suppliers",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_UserModifiedId",
                table: "Suppliers",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCards_UserCreatedId",
                table: "StockCards",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCards_UserModifiedId",
                table: "StockCards",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRs_UserCreatedId",
                table: "SOACRs",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRs_UserModifiedId",
                table: "SOACRs",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRLineItems_UserCreatedId",
                table: "SOACRLineItems",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRLineItems_UserModifiedId",
                table: "SOACRLineItems",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRChecks_UserCreatedId",
                table: "SOACRChecks",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_SOACRChecks_UserModifiedId",
                table: "SOACRChecks",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_UserCreatedId",
                table: "ShippingAddresses",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_UserModifiedId",
                table: "ShippingAddresses",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnTotalReturnAmounts_UserCreatedId",
                table: "SalesReturnTotalReturnAmounts",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnTotalReturnAmounts_UserModifiedId",
                table: "SalesReturnTotalReturnAmounts",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_UserCreatedId",
                table: "SalesReturns",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_UserModifiedId",
                table: "SalesReturns",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLineItems_UserCreatedId",
                table: "SalesReturnLineItems",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLineItems_UserModifiedId",
                table: "SalesReturnLineItems",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnAppliedAmounts_UserCreatedId",
                table: "SalesReturnAppliedAmounts",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnAppliedAmounts_UserModifiedId",
                table: "SalesReturnAppliedAmounts",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_UserCreatedId",
                table: "SalesOrders",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_UserModifiedId",
                table: "SalesOrders",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLineItems_UserCreatedId",
                table: "SalesOrderLineItems",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLineItems_UserModifiedId",
                table: "SalesOrderLineItems",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveryMonitorings_UserCreatedId",
                table: "SalesOrderDeliveryMonitorings",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveryMonitorings_UserModifiedId",
                table: "SalesOrderDeliveryMonitorings",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturns_UserCreatedId",
                table: "PurchaseReturns",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturns_UserModifiedId",
                table: "PurchaseReturns",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnLineItems_UserCreatedId",
                table: "PurchaseReturnLineItems",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnLineItems_UserModifiedId",
                table: "PurchaseReturnLineItems",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_UserCreatedId",
                table: "PurchaseOrders",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_UserModifiedId",
                table: "PurchaseOrders",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLineItems_UserCreatedId",
                table: "PurchaseOrderLineItems",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLineItems_UserModifiedId",
                table: "PurchaseOrderLineItems",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalCountHistories_UserCreatedId",
                table: "PhysicalCountHistories",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalCountHistories_UserModifiedId",
                table: "PhysicalCountHistories",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserCreatedId",
                table: "Payments",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserModifiedId",
                table: "Payments",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_UserCreatedId",
                table: "PaymentDetails",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_UserModifiedId",
                table: "PaymentDetails",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Packs_UserCreatedId",
                table: "Packs",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Packs_UserModifiedId",
                table: "Packs",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_PackReturns_UserCreatedId",
                table: "PackReturns",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PackReturns_UserModifiedId",
                table: "PackReturns",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_PackReturnLineItems_UserCreatedId",
                table: "PackReturnLineItems",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PackReturnLineItems_UserModifiedId",
                table: "PackReturnLineItems",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_PackLineItems_UserCreatedId",
                table: "PackLineItems",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PackLineItems_UserModifiedId",
                table: "PackLineItems",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_UserCreatedId",
                table: "ItemTypes",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_UserModifiedId",
                table: "ItemTypes",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserCreatedId",
                table: "Items",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserModifiedId",
                table: "Items",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_UserCreatedId",
                table: "ItemPrices",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_UserModifiedId",
                table: "ItemPrices",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventories_UserCreatedId",
                table: "ItemInventories",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventories_UserModifiedId",
                table: "ItemInventories",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserCreatedId",
                table: "Invoices",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserModifiedId",
                table: "Invoices",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_UserCreatedId",
                table: "InvoicePayments",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_UserModifiedId",
                table: "InvoicePayments",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_UserCreatedId",
                table: "InvoiceLineItems",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_UserModifiedId",
                table: "InvoiceLineItems",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Forwarders_UserCreatedId",
                table: "Forwarders",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Forwarders_UserModifiedId",
                table: "Forwarders",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserCreatedId",
                table: "Customers",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserModifiedId",
                table: "Customers",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPayments_UserCreatedId",
                table: "CustomerPayments",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPayments_UserModifiedId",
                table: "CustomerPayments",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPaymentDetails_UserCreatedId",
                table: "CustomerPaymentDetails",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPaymentDetails_UserModifiedId",
                table: "CustomerPaymentDetails",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditTerms_UserCreatedId",
                table: "CreditTerms",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditTerms_UserModifiedId",
                table: "CreditTerms",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditDebitMemos_UserCreatedId",
                table: "CreditDebitMemos",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditDebitMemos_UserModifiedId",
                table: "CreditDebitMemos",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserCreatedId",
                table: "Categories",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserModifiedId",
                table: "Categories",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_UserCreatedId",
                table: "Brands",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_UserModifiedId",
                table: "Brands",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_UserCreatedId",
                table: "Areas",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_UserModifiedId",
                table: "Areas",
                column: "UserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_UserCreatedId",
                table: "Agents",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_UserModifiedId",
                table: "Agents",
                column: "UserModifiedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_AspNetUsers_UserCreatedId",
                table: "Agents",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_AspNetUsers_UserModifiedId",
                table: "Agents",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_AspNetUsers_UserCreatedId",
                table: "Areas",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_AspNetUsers_UserModifiedId",
                table: "Areas",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_AspNetUsers_UserId",
                table: "AuditLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_AspNetUsers_UserCreatedId",
                table: "Brands",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_AspNetUsers_UserModifiedId",
                table: "Brands",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_UserCreatedId",
                table: "Categories",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_UserModifiedId",
                table: "Categories",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditDebitMemos_AspNetUsers_UserCreatedId",
                table: "CreditDebitMemos",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditDebitMemos_AspNetUsers_UserModifiedId",
                table: "CreditDebitMemos",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserCreatedId",
                table: "CreditTerms",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserModifiedId",
                table: "CreditTerms",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPaymentDetails_AspNetUsers_UserCreatedId",
                table: "CustomerPaymentDetails",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPaymentDetails_AspNetUsers_UserModifiedId",
                table: "CustomerPaymentDetails",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPayments_AspNetUsers_UserCreatedId",
                table: "CustomerPayments",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPayments_AspNetUsers_UserModifiedId",
                table: "CustomerPayments",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserCreatedId",
                table: "Customers",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserModifiedId",
                table: "Customers",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forwarders_AspNetUsers_UserCreatedId",
                table: "Forwarders",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forwarders_AspNetUsers_UserModifiedId",
                table: "Forwarders",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineItems_AspNetUsers_UserCreatedId",
                table: "InvoiceLineItems",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineItems_AspNetUsers_UserModifiedId",
                table: "InvoiceLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayments_AspNetUsers_UserCreatedId",
                table: "InvoicePayments",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayments_AspNetUsers_UserModifiedId",
                table: "InvoicePayments",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_UserCreatedId",
                table: "Invoices",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_UserModifiedId",
                table: "Invoices",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInventories_AspNetUsers_UserCreatedId",
                table: "ItemInventories",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInventories_AspNetUsers_UserModifiedId",
                table: "ItemInventories",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_AspNetUsers_UserCreatedId",
                table: "ItemPrices",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_AspNetUsers_UserModifiedId",
                table: "ItemPrices",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_UserCreatedId",
                table: "Items",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_UserModifiedId",
                table: "Items",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTypes_AspNetUsers_UserCreatedId",
                table: "ItemTypes",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTypes_AspNetUsers_UserModifiedId",
                table: "ItemTypes",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PackLineItems_AspNetUsers_UserCreatedId",
                table: "PackLineItems",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PackLineItems_AspNetUsers_UserModifiedId",
                table: "PackLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PackReturnLineItems_AspNetUsers_UserCreatedId",
                table: "PackReturnLineItems",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PackReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PackReturnLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PackReturns_AspNetUsers_UserCreatedId",
                table: "PackReturns",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PackReturns_AspNetUsers_UserModifiedId",
                table: "PackReturns",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Packs_AspNetUsers_UserCreatedId",
                table: "Packs",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Packs_AspNetUsers_UserModifiedId",
                table: "Packs",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_AspNetUsers_UserCreatedId",
                table: "PaymentDetails",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_AspNetUsers_UserModifiedId",
                table: "PaymentDetails",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_UserCreatedId",
                table: "Payments",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_UserModifiedId",
                table: "Payments",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalCountHistories_AspNetUsers_UserCreatedId",
                table: "PhysicalCountHistories",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalCountHistories_AspNetUsers_UserModifiedId",
                table: "PhysicalCountHistories",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderLineItems_AspNetUsers_UserCreatedId",
                table: "PurchaseOrderLineItems",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseOrderLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_AspNetUsers_UserCreatedId",
                table: "PurchaseOrders",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_AspNetUsers_UserModifiedId",
                table: "PurchaseOrders",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnLineItems_AspNetUsers_UserCreatedId",
                table: "PurchaseReturnLineItems",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseReturnLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturns_AspNetUsers_UserCreatedId",
                table: "PurchaseReturns",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturns_AspNetUsers_UserModifiedId",
                table: "PurchaseReturns",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_AspNetUsers_UserCreatedId",
                table: "SalesOrderDeliveryMonitorings",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_AspNetUsers_UserModifiedId",
                table: "SalesOrderDeliveryMonitorings",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLineItems_AspNetUsers_UserCreatedId",
                table: "SalesOrderLineItems",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLineItems_AspNetUsers_UserModifiedId",
                table: "SalesOrderLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_AspNetUsers_UserCreatedId",
                table: "SalesOrders",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_AspNetUsers_UserModifiedId",
                table: "SalesOrders",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnAppliedAmounts_AspNetUsers_UserCreatedId",
                table: "SalesReturnAppliedAmounts",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnAppliedAmounts_AspNetUsers_UserModifiedId",
                table: "SalesReturnAppliedAmounts",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnLineItems_AspNetUsers_UserCreatedId",
                table: "SalesReturnLineItems",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnLineItems_AspNetUsers_UserModifiedId",
                table: "SalesReturnLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_AspNetUsers_UserCreatedId",
                table: "SalesReturns",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_AspNetUsers_UserModifiedId",
                table: "SalesReturns",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnTotalReturnAmounts_AspNetUsers_UserCreatedId",
                table: "SalesReturnTotalReturnAmounts",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnTotalReturnAmounts_AspNetUsers_UserModifiedId",
                table: "SalesReturnTotalReturnAmounts",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_UserCreatedId",
                table: "ShippingAddresses",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_UserModifiedId",
                table: "ShippingAddresses",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRChecks_AspNetUsers_UserCreatedId",
                table: "SOACRChecks",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRChecks_AspNetUsers_UserModifiedId",
                table: "SOACRChecks",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRLineItems_AspNetUsers_UserCreatedId",
                table: "SOACRLineItems",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRLineItems_AspNetUsers_UserModifiedId",
                table: "SOACRLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRs_AspNetUsers_UserCreatedId",
                table: "SOACRs",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRs_AspNetUsers_UserModifiedId",
                table: "SOACRs",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockCards_AspNetUsers_UserCreatedId",
                table: "StockCards",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockCards_AspNetUsers_UserModifiedId",
                table: "StockCards",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_AspNetUsers_UserCreatedId",
                table: "Suppliers",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_AspNetUsers_UserModifiedId",
                table: "Suppliers",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemDetails_AspNetUsers_UserCreatedId",
                table: "TransferItemDetails",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemDetails_AspNetUsers_UserModifiedId",
                table: "TransferItemDetails",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_AspNetUsers_UserCreatedId",
                table: "TransferItems",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_AspNetUsers_UserModifiedId",
                table: "TransferItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_AspNetUsers_UserCreatedId",
                table: "Warehouses",
                column: "UserCreatedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_AspNetUsers_UserModifiedId",
                table: "Warehouses",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_AspNetUsers_UserCreatedId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Agents_AspNetUsers_UserModifiedId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Areas_AspNetUsers_UserCreatedId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Areas_AspNetUsers_UserModifiedId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_AspNetUsers_UserId",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_AspNetUsers_UserCreatedId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_AspNetUsers_UserModifiedId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_UserCreatedId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_UserModifiedId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditDebitMemos_AspNetUsers_UserCreatedId",
                table: "CreditDebitMemos");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditDebitMemos_AspNetUsers_UserModifiedId",
                table: "CreditDebitMemos");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserCreatedId",
                table: "CreditTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserModifiedId",
                table: "CreditTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPaymentDetails_AspNetUsers_UserCreatedId",
                table: "CustomerPaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPaymentDetails_AspNetUsers_UserModifiedId",
                table: "CustomerPaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPayments_AspNetUsers_UserCreatedId",
                table: "CustomerPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPayments_AspNetUsers_UserModifiedId",
                table: "CustomerPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserCreatedId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserModifiedId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Forwarders_AspNetUsers_UserCreatedId",
                table: "Forwarders");

            migrationBuilder.DropForeignKey(
                name: "FK_Forwarders_AspNetUsers_UserModifiedId",
                table: "Forwarders");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineItems_AspNetUsers_UserCreatedId",
                table: "InvoiceLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineItems_AspNetUsers_UserModifiedId",
                table: "InvoiceLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePayments_AspNetUsers_UserCreatedId",
                table: "InvoicePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePayments_AspNetUsers_UserModifiedId",
                table: "InvoicePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_UserCreatedId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_UserModifiedId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemInventories_AspNetUsers_UserCreatedId",
                table: "ItemInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemInventories_AspNetUsers_UserModifiedId",
                table: "ItemInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_AspNetUsers_UserCreatedId",
                table: "ItemPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_AspNetUsers_UserModifiedId",
                table: "ItemPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_UserCreatedId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_UserModifiedId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTypes_AspNetUsers_UserCreatedId",
                table: "ItemTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTypes_AspNetUsers_UserModifiedId",
                table: "ItemTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PackLineItems_AspNetUsers_UserCreatedId",
                table: "PackLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PackLineItems_AspNetUsers_UserModifiedId",
                table: "PackLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PackReturnLineItems_AspNetUsers_UserCreatedId",
                table: "PackReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PackReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PackReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PackReturns_AspNetUsers_UserCreatedId",
                table: "PackReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_PackReturns_AspNetUsers_UserModifiedId",
                table: "PackReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_Packs_AspNetUsers_UserCreatedId",
                table: "Packs");

            migrationBuilder.DropForeignKey(
                name: "FK_Packs_AspNetUsers_UserModifiedId",
                table: "Packs");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_AspNetUsers_UserCreatedId",
                table: "PaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_AspNetUsers_UserModifiedId",
                table: "PaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_UserCreatedId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_UserModifiedId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalCountHistories_AspNetUsers_UserCreatedId",
                table: "PhysicalCountHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalCountHistories_AspNetUsers_UserModifiedId",
                table: "PhysicalCountHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderLineItems_AspNetUsers_UserCreatedId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_AspNetUsers_UserCreatedId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_AspNetUsers_UserModifiedId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnLineItems_AspNetUsers_UserCreatedId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturns_AspNetUsers_UserCreatedId",
                table: "PurchaseReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturns_AspNetUsers_UserModifiedId",
                table: "PurchaseReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_AspNetUsers_UserCreatedId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_AspNetUsers_UserModifiedId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLineItems_AspNetUsers_UserCreatedId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLineItems_AspNetUsers_UserModifiedId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_AspNetUsers_UserCreatedId",
                table: "SalesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_AspNetUsers_UserModifiedId",
                table: "SalesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnAppliedAmounts_AspNetUsers_UserCreatedId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnAppliedAmounts_AspNetUsers_UserModifiedId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_AspNetUsers_UserCreatedId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_AspNetUsers_UserModifiedId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_AspNetUsers_UserCreatedId",
                table: "SalesReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_AspNetUsers_UserModifiedId",
                table: "SalesReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnTotalReturnAmounts_AspNetUsers_UserCreatedId",
                table: "SalesReturnTotalReturnAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnTotalReturnAmounts_AspNetUsers_UserModifiedId",
                table: "SalesReturnTotalReturnAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_UserCreatedId",
                table: "ShippingAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_UserModifiedId",
                table: "ShippingAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRChecks_AspNetUsers_UserCreatedId",
                table: "SOACRChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRChecks_AspNetUsers_UserModifiedId",
                table: "SOACRChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRLineItems_AspNetUsers_UserCreatedId",
                table: "SOACRLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRLineItems_AspNetUsers_UserModifiedId",
                table: "SOACRLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRs_AspNetUsers_UserCreatedId",
                table: "SOACRs");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRs_AspNetUsers_UserModifiedId",
                table: "SOACRs");

            migrationBuilder.DropForeignKey(
                name: "FK_StockCards_AspNetUsers_UserCreatedId",
                table: "StockCards");

            migrationBuilder.DropForeignKey(
                name: "FK_StockCards_AspNetUsers_UserModifiedId",
                table: "StockCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_AspNetUsers_UserCreatedId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_AspNetUsers_UserModifiedId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_AspNetUsers_UserCreatedId",
                table: "TransferItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_AspNetUsers_UserModifiedId",
                table: "TransferItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_AspNetUsers_UserCreatedId",
                table: "TransferItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_AspNetUsers_UserModifiedId",
                table: "TransferItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_AspNetUsers_UserCreatedId",
                table: "Warehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_AspNetUsers_UserModifiedId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_UserCreatedId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_UserModifiedId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_TransferItems_UserCreatedId",
                table: "TransferItems");

            migrationBuilder.DropIndex(
                name: "IX_TransferItems_UserModifiedId",
                table: "TransferItems");

            migrationBuilder.DropIndex(
                name: "IX_TransferItemDetails_UserCreatedId",
                table: "TransferItemDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransferItemDetails_UserModifiedId",
                table: "TransferItemDetails");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_UserCreatedId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_UserModifiedId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_StockCards_UserCreatedId",
                table: "StockCards");

            migrationBuilder.DropIndex(
                name: "IX_StockCards_UserModifiedId",
                table: "StockCards");

            migrationBuilder.DropIndex(
                name: "IX_SOACRs_UserCreatedId",
                table: "SOACRs");

            migrationBuilder.DropIndex(
                name: "IX_SOACRs_UserModifiedId",
                table: "SOACRs");

            migrationBuilder.DropIndex(
                name: "IX_SOACRLineItems_UserCreatedId",
                table: "SOACRLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SOACRLineItems_UserModifiedId",
                table: "SOACRLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SOACRChecks_UserCreatedId",
                table: "SOACRChecks");

            migrationBuilder.DropIndex(
                name: "IX_SOACRChecks_UserModifiedId",
                table: "SOACRChecks");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAddresses_UserCreatedId",
                table: "ShippingAddresses");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAddresses_UserModifiedId",
                table: "ShippingAddresses");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturnTotalReturnAmounts_UserCreatedId",
                table: "SalesReturnTotalReturnAmounts");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturnTotalReturnAmounts_UserModifiedId",
                table: "SalesReturnTotalReturnAmounts");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturns_UserCreatedId",
                table: "SalesReturns");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturns_UserModifiedId",
                table: "SalesReturns");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturnLineItems_UserCreatedId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturnLineItems_UserModifiedId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturnAppliedAmounts_UserCreatedId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturnAppliedAmounts_UserModifiedId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_UserCreatedId",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_UserModifiedId",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLineItems_UserCreatedId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLineItems_UserModifiedId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderDeliveryMonitorings_UserCreatedId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderDeliveryMonitorings_UserModifiedId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturns_UserCreatedId",
                table: "PurchaseReturns");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturns_UserModifiedId",
                table: "PurchaseReturns");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnLineItems_UserCreatedId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnLineItems_UserModifiedId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_UserCreatedId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_UserModifiedId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderLineItems_UserCreatedId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderLineItems_UserModifiedId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalCountHistories_UserCreatedId",
                table: "PhysicalCountHistories");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalCountHistories_UserModifiedId",
                table: "PhysicalCountHistories");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UserCreatedId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UserModifiedId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDetails_UserCreatedId",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDetails_UserModifiedId",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_Packs_UserCreatedId",
                table: "Packs");

            migrationBuilder.DropIndex(
                name: "IX_Packs_UserModifiedId",
                table: "Packs");

            migrationBuilder.DropIndex(
                name: "IX_PackReturns_UserCreatedId",
                table: "PackReturns");

            migrationBuilder.DropIndex(
                name: "IX_PackReturns_UserModifiedId",
                table: "PackReturns");

            migrationBuilder.DropIndex(
                name: "IX_PackReturnLineItems_UserCreatedId",
                table: "PackReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PackReturnLineItems_UserModifiedId",
                table: "PackReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PackLineItems_UserCreatedId",
                table: "PackLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PackLineItems_UserModifiedId",
                table: "PackLineItems");

            migrationBuilder.DropIndex(
                name: "IX_ItemTypes_UserCreatedId",
                table: "ItemTypes");

            migrationBuilder.DropIndex(
                name: "IX_ItemTypes_UserModifiedId",
                table: "ItemTypes");

            migrationBuilder.DropIndex(
                name: "IX_Items_UserCreatedId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_UserModifiedId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_ItemPrices_UserCreatedId",
                table: "ItemPrices");

            migrationBuilder.DropIndex(
                name: "IX_ItemPrices_UserModifiedId",
                table: "ItemPrices");

            migrationBuilder.DropIndex(
                name: "IX_ItemInventories_UserCreatedId",
                table: "ItemInventories");

            migrationBuilder.DropIndex(
                name: "IX_ItemInventories_UserModifiedId",
                table: "ItemInventories");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_UserCreatedId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_UserModifiedId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_InvoicePayments_UserCreatedId",
                table: "InvoicePayments");

            migrationBuilder.DropIndex(
                name: "IX_InvoicePayments_UserModifiedId",
                table: "InvoicePayments");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLineItems_UserCreatedId",
                table: "InvoiceLineItems");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLineItems_UserModifiedId",
                table: "InvoiceLineItems");

            migrationBuilder.DropIndex(
                name: "IX_Forwarders_UserCreatedId",
                table: "Forwarders");

            migrationBuilder.DropIndex(
                name: "IX_Forwarders_UserModifiedId",
                table: "Forwarders");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserCreatedId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserModifiedId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPayments_UserCreatedId",
                table: "CustomerPayments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPayments_UserModifiedId",
                table: "CustomerPayments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPaymentDetails_UserCreatedId",
                table: "CustomerPaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPaymentDetails_UserModifiedId",
                table: "CustomerPaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_CreditTerms_UserCreatedId",
                table: "CreditTerms");

            migrationBuilder.DropIndex(
                name: "IX_CreditTerms_UserModifiedId",
                table: "CreditTerms");

            migrationBuilder.DropIndex(
                name: "IX_CreditDebitMemos_UserCreatedId",
                table: "CreditDebitMemos");

            migrationBuilder.DropIndex(
                name: "IX_CreditDebitMemos_UserModifiedId",
                table: "CreditDebitMemos");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserCreatedId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserModifiedId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Brands_UserCreatedId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_UserModifiedId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_Areas_UserCreatedId",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Areas_UserModifiedId",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Agents_UserCreatedId",
                table: "Agents");

            migrationBuilder.DropIndex(
                name: "IX_Agents_UserModifiedId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "TransferItemDetails");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "TransferItemDetails");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "StockCards");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "StockCards");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "SOACRs");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "SOACRs");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "SOACRLineItems");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "SOACRLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "SOACRChecks");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "SOACRChecks");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "SalesReturnTotalReturnAmounts");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "SalesReturnTotalReturnAmounts");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "PurchaseReturns");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "PurchaseReturns");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "PhysicalCountHistories");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "PhysicalCountHistories");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "PackReturns");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "PackReturns");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "PackReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "PackReturnLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "PackLineItems");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "PackLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "ItemInventories");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "ItemInventories");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "InvoiceLineItems");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "InvoiceLineItems");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Forwarders");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Forwarders");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "CustomerPayments");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "CustomerPayments");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "CustomerPaymentDetails");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "CustomerPaymentDetails");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "CreditTerms");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "CreditTerms");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "CreditDebitMemos");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "CreditDebitMemos");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "UsrCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UsrModified",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "Agents");

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Warehouses",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Warehouses",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "TransferItems",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "TransferItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "TransferItemDetails",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "TransferItemDetails",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Suppliers",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Suppliers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "StockCards",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "StockCards",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "SOACRs",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "SOACRs",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "SOACRLineItems",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "SOACRLineItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "SOACRChecks",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "SOACRChecks",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "ShippingAddresses",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "ShippingAddresses",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "SalesReturnTotalReturnAmounts",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "SalesReturnTotalReturnAmounts",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "SalesReturns",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "SalesReturns",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "SalesReturnLineItems",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "SalesReturnLineItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "SalesReturnAppliedAmounts",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "SalesReturnAppliedAmounts",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "SalesOrders",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "SalesOrders",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "SalesOrderLineItems",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "SalesOrderLineItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "PurchaseReturns",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "PurchaseReturns",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "PurchaseReturnLineItems",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "PurchaseReturnLineItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "PurchaseOrders",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "PurchaseOrders",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "PurchaseOrderLineItems",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "PurchaseOrderLineItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "PhysicalCountHistories",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "PhysicalCountHistories",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Payments",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Payments",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "PaymentDetails",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "PaymentDetails",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Packs",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Packs",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "PackReturns",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "PackReturns",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "PackReturnLineItems",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "PackReturnLineItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "PackLineItems",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "PackLineItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "ItemTypes",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "ItemTypes",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Items",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Items",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "ItemPrices",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "ItemPrices",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "ItemInventories",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "ItemInventories",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Invoices",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Invoices",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "InvoicePayments",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "InvoicePayments",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "InvoiceLineItems",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "InvoiceLineItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Forwarders",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Forwarders",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Customers",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Customers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "CustomerPayments",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "CustomerPayments",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "CustomerPaymentDetails",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "CustomerPaymentDetails",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "CreditTerms",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "CreditTerms",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "CreditDebitMemos",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "CreditDebitMemos",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Categories",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Categories",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Brands",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Brands",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Areas",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Areas",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Agents",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Agents",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "319d1663-3d5a-44eb-999d-5330a8be1b4f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "4e09b1aa-7322-4d7b-b64d-845efc6a3eca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "129fa264-e760-4b85-8aa0-4d15ed30d99f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "bc61ae63-3808-4091-9b6a-a451a59d38e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "47a59b68-d308-4986-9d4e-d7d055c23563");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "f4d43e1b-6590-48c5-9be7-21d8567c6941");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp", "UserCreated" },
                values: new object[] { "ac43d4d2-1886-4fc9-8dce-387fe30803f0", new DateTime(2022, 7, 28, 13, 22, 29, 277, DateTimeKind.Local).AddTicks(2870), "AQAAAAEAACcQAAAAEJYUTgtTDzplOxpHDBltscatKNut/1ZTg1vsl4UVNTpwhqffU9CJlNuSFpAKva/wlA==", "3de5635f-d9cf-4709-bc5a-b3860230e640", "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp", "UserCreated" },
                values: new object[] { "01b6d092-028c-4847-b912-2402804a30ad", new DateTime(2022, 7, 28, 13, 22, 29, 266, DateTimeKind.Local).AddTicks(3125), "AQAAAAEAACcQAAAAEFfvBzrpo8O2QL3T8IeoLZGi9xPQBG9OsfwRPsH/0oLL7cUv2hB4lrQIj/WzGRjcyA==", "a539be12-d0aa-4881-ba6f-44ef537f8cd7", "CDE19596-2C9E-4ECE-837C-85950E9E907D" });
        }
    }
}
