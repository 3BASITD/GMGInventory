using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class UserCreatedFK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_AspNetUsers_UserModifiedId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Areas_AspNetUsers_UserModifiedId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_AspNetUsers_UserModifiedId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_UserModifiedId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditDebitMemos_AspNetUsers_UserModifiedId",
                table: "CreditDebitMemos");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserModifiedId",
                table: "CreditTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPaymentDetails_AspNetUsers_UserModifiedId",
                table: "CustomerPaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPayments_AspNetUsers_UserModifiedId",
                table: "CustomerPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserModifiedId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Forwarders_AspNetUsers_UserModifiedId",
                table: "Forwarders");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineItems_AspNetUsers_UserModifiedId",
                table: "InvoiceLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePayments_AspNetUsers_UserModifiedId",
                table: "InvoicePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_UserModifiedId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemInventories_AspNetUsers_UserModifiedId",
                table: "ItemInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_AspNetUsers_UserModifiedId",
                table: "ItemPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_UserModifiedId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTypes_AspNetUsers_UserModifiedId",
                table: "ItemTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PackLineItems_AspNetUsers_UserModifiedId",
                table: "PackLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PackReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PackReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PackReturns_AspNetUsers_UserModifiedId",
                table: "PackReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_Packs_AspNetUsers_UserModifiedId",
                table: "Packs");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_AspNetUsers_UserModifiedId",
                table: "PaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_UserModifiedId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalCountHistories_AspNetUsers_UserModifiedId",
                table: "PhysicalCountHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_AspNetUsers_UserModifiedId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturns_AspNetUsers_UserModifiedId",
                table: "PurchaseReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_AspNetUsers_UserModifiedId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLineItems_AspNetUsers_UserModifiedId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_AspNetUsers_UserModifiedId",
                table: "SalesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnAppliedAmounts_AspNetUsers_UserModifiedId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_AspNetUsers_UserModifiedId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_AspNetUsers_UserModifiedId",
                table: "SalesReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnTotalReturnAmounts_AspNetUsers_UserModifiedId",
                table: "SalesReturnTotalReturnAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_UserModifiedId",
                table: "ShippingAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRChecks_AspNetUsers_UserModifiedId",
                table: "SOACRChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRLineItems_AspNetUsers_UserModifiedId",
                table: "SOACRLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRs_AspNetUsers_UserModifiedId",
                table: "SOACRs");

            migrationBuilder.DropForeignKey(
                name: "FK_StockCards_AspNetUsers_UserModifiedId",
                table: "StockCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_AspNetUsers_UserModifiedId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_AspNetUsers_UserModifiedId",
                table: "TransferItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_AspNetUsers_UserModifiedId",
                table: "TransferItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_AspNetUsers_UserModifiedId",
                table: "Warehouses");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Warehouses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "TransferItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "TransferItemDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Suppliers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "StockCards",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SOACRs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SOACRLineItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SOACRChecks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "ShippingAddresses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturnTotalReturnAmounts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturns",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturnLineItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturnAppliedAmounts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesOrders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesOrderLineItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseReturns",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseReturnLineItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseOrders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseOrderLineItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PhysicalCountHistories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PaymentDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Packs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PackReturns",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PackReturnLineItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PackLineItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "ItemTypes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "ItemPrices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "ItemInventories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "InvoicePayments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "InvoiceLineItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Forwarders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "CustomerPayments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "CustomerPaymentDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "CreditTerms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "CreditDebitMemos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Brands",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Areas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Agents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "048412e3-89c4-4d7d-8943-34a3a7772a9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "1fdf0e16-f7dd-4fbe-8b20-f94881310256");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "66724a78-7194-4692-9b6a-5255adcf9d23");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "cb28f942-5c93-4122-ac32-39a43cd58837");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "f244541d-d360-4ac9-8d0d-64a8d1143312");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "eaf8bc9b-b5da-44da-81ae-0525e0880307");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2eaf77ee-10a1-4feb-aefd-6f771945d24d", new DateTime(2022, 7, 29, 14, 25, 12, 215, DateTimeKind.Local).AddTicks(8757), "AQAAAAEAACcQAAAAEH8Q7FTMesdrdcvpTyFe5dZW+Wxvk6oswrpAS0CMK0H0M4KzCZam/5WL/rQtPAt5dw==", "8e5eee7b-bd68-414b-9740-e632d4f2b822" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e6cc88a-9952-40be-ac35-87801072c133", new DateTime(2022, 7, 29, 14, 25, 12, 208, DateTimeKind.Local).AddTicks(7633), "AQAAAAEAACcQAAAAEJbZsuGumRQv58G7NtPXOtRwTge0K9A9ykD3tVFNmt38UPf+QOPT6cNfegHRvUpxuQ==", "57daf31b-47d0-4862-9788-4dedb942cc8c" });

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_AspNetUsers_UserModifiedId",
                table: "Agents",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_AspNetUsers_UserModifiedId",
                table: "Areas",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_AspNetUsers_UserModifiedId",
                table: "Brands",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_UserModifiedId",
                table: "Categories",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditDebitMemos_AspNetUsers_UserModifiedId",
                table: "CreditDebitMemos",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserModifiedId",
                table: "CreditTerms",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPaymentDetails_AspNetUsers_UserModifiedId",
                table: "CustomerPaymentDetails",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPayments_AspNetUsers_UserModifiedId",
                table: "CustomerPayments",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserModifiedId",
                table: "Customers",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Forwarders_AspNetUsers_UserModifiedId",
                table: "Forwarders",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineItems_AspNetUsers_UserModifiedId",
                table: "InvoiceLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayments_AspNetUsers_UserModifiedId",
                table: "InvoicePayments",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_UserModifiedId",
                table: "Invoices",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInventories_AspNetUsers_UserModifiedId",
                table: "ItemInventories",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_AspNetUsers_UserModifiedId",
                table: "ItemPrices",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_UserModifiedId",
                table: "Items",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTypes_AspNetUsers_UserModifiedId",
                table: "ItemTypes",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PackLineItems_AspNetUsers_UserModifiedId",
                table: "PackLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PackReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PackReturnLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PackReturns_AspNetUsers_UserModifiedId",
                table: "PackReturns",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packs_AspNetUsers_UserModifiedId",
                table: "Packs",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_AspNetUsers_UserModifiedId",
                table: "PaymentDetails",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_UserModifiedId",
                table: "Payments",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalCountHistories_AspNetUsers_UserModifiedId",
                table: "PhysicalCountHistories",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseOrderLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_AspNetUsers_UserModifiedId",
                table: "PurchaseOrders",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseReturnLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturns_AspNetUsers_UserModifiedId",
                table: "PurchaseReturns",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_AspNetUsers_UserModifiedId",
                table: "SalesOrderDeliveryMonitorings",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLineItems_AspNetUsers_UserModifiedId",
                table: "SalesOrderLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_AspNetUsers_UserModifiedId",
                table: "SalesOrders",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnAppliedAmounts_AspNetUsers_UserModifiedId",
                table: "SalesReturnAppliedAmounts",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnLineItems_AspNetUsers_UserModifiedId",
                table: "SalesReturnLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_AspNetUsers_UserModifiedId",
                table: "SalesReturns",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnTotalReturnAmounts_AspNetUsers_UserModifiedId",
                table: "SalesReturnTotalReturnAmounts",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_UserModifiedId",
                table: "ShippingAddresses",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRChecks_AspNetUsers_UserModifiedId",
                table: "SOACRChecks",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRLineItems_AspNetUsers_UserModifiedId",
                table: "SOACRLineItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRs_AspNetUsers_UserModifiedId",
                table: "SOACRs",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockCards_AspNetUsers_UserModifiedId",
                table: "StockCards",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_AspNetUsers_UserModifiedId",
                table: "Suppliers",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemDetails_AspNetUsers_UserModifiedId",
                table: "TransferItemDetails",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItems_AspNetUsers_UserModifiedId",
                table: "TransferItems",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_AspNetUsers_UserModifiedId",
                table: "Warehouses",
                column: "UserModifiedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_AspNetUsers_UserModifiedId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Areas_AspNetUsers_UserModifiedId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_AspNetUsers_UserModifiedId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_UserModifiedId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditDebitMemos_AspNetUsers_UserModifiedId",
                table: "CreditDebitMemos");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditTerms_AspNetUsers_UserModifiedId",
                table: "CreditTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPaymentDetails_AspNetUsers_UserModifiedId",
                table: "CustomerPaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPayments_AspNetUsers_UserModifiedId",
                table: "CustomerPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserModifiedId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Forwarders_AspNetUsers_UserModifiedId",
                table: "Forwarders");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineItems_AspNetUsers_UserModifiedId",
                table: "InvoiceLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePayments_AspNetUsers_UserModifiedId",
                table: "InvoicePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_UserModifiedId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemInventories_AspNetUsers_UserModifiedId",
                table: "ItemInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_AspNetUsers_UserModifiedId",
                table: "ItemPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_UserModifiedId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTypes_AspNetUsers_UserModifiedId",
                table: "ItemTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PackLineItems_AspNetUsers_UserModifiedId",
                table: "PackLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PackReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PackReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PackReturns_AspNetUsers_UserModifiedId",
                table: "PackReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_Packs_AspNetUsers_UserModifiedId",
                table: "Packs");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_AspNetUsers_UserModifiedId",
                table: "PaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_UserModifiedId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalCountHistories_AspNetUsers_UserModifiedId",
                table: "PhysicalCountHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_AspNetUsers_UserModifiedId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturns_AspNetUsers_UserModifiedId",
                table: "PurchaseReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_AspNetUsers_UserModifiedId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLineItems_AspNetUsers_UserModifiedId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_AspNetUsers_UserModifiedId",
                table: "SalesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnAppliedAmounts_AspNetUsers_UserModifiedId",
                table: "SalesReturnAppliedAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_AspNetUsers_UserModifiedId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_AspNetUsers_UserModifiedId",
                table: "SalesReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnTotalReturnAmounts_AspNetUsers_UserModifiedId",
                table: "SalesReturnTotalReturnAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_UserModifiedId",
                table: "ShippingAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRChecks_AspNetUsers_UserModifiedId",
                table: "SOACRChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRLineItems_AspNetUsers_UserModifiedId",
                table: "SOACRLineItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SOACRs_AspNetUsers_UserModifiedId",
                table: "SOACRs");

            migrationBuilder.DropForeignKey(
                name: "FK_StockCards_AspNetUsers_UserModifiedId",
                table: "StockCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_AspNetUsers_UserModifiedId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemDetails_AspNetUsers_UserModifiedId",
                table: "TransferItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItems_AspNetUsers_UserModifiedId",
                table: "TransferItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_AspNetUsers_UserModifiedId",
                table: "Warehouses");

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Warehouses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "TransferItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "TransferItemDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Suppliers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "StockCards",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SOACRs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SOACRLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SOACRChecks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "ShippingAddresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturnTotalReturnAmounts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturnLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesReturnAppliedAmounts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesOrderLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseReturns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseReturnLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PurchaseOrderLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PhysicalCountHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PaymentDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Packs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PackReturns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PackReturnLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "PackLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "ItemTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "ItemPrices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "ItemInventories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "InvoicePayments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "InvoiceLineItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Forwarders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "CustomerPayments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "CustomerPaymentDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "CreditTerms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "CreditDebitMemos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Brands",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Areas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserModifiedId",
                table: "Agents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14fe3830-d43e-4bc2-ac2e-f8b0a273d5d1", new DateTime(2022, 7, 29, 6, 57, 43, 445, DateTimeKind.Local).AddTicks(4693), "AQAAAAEAACcQAAAAEFFTc5548OkbxxYkoKIxa7pirAfCEzTVxxCcbJ1bPMkUIhrEkgmNNauY0hQ5BFTVWQ==", "7c61a63c-3390-4306-a101-71b96e7e58e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "acb78d30-3680-4425-80ae-cfd91afe37ab", new DateTime(2022, 7, 29, 6, 57, 43, 435, DateTimeKind.Local).AddTicks(8787), "AQAAAAEAACcQAAAAEBFXMn3CM9l5on1F1fhlyx9yMF8ostt3CsFqBTN1t79cSVe93mDwehalpefjHOO22w==", "8295cda3-40a9-4777-b383-0c8a47b61120" });

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_AspNetUsers_UserModifiedId",
                table: "Agents",
                column: "UserModifiedId",
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
                name: "FK_Brands_AspNetUsers_UserModifiedId",
                table: "Brands",
                column: "UserModifiedId",
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
                name: "FK_CreditDebitMemos_AspNetUsers_UserModifiedId",
                table: "CreditDebitMemos",
                column: "UserModifiedId",
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
                name: "FK_CustomerPaymentDetails_AspNetUsers_UserModifiedId",
                table: "CustomerPaymentDetails",
                column: "UserModifiedId",
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
                name: "FK_Customers_AspNetUsers_UserModifiedId",
                table: "Customers",
                column: "UserModifiedId",
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
                name: "FK_InvoiceLineItems_AspNetUsers_UserModifiedId",
                table: "InvoiceLineItems",
                column: "UserModifiedId",
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
                name: "FK_Invoices_AspNetUsers_UserModifiedId",
                table: "Invoices",
                column: "UserModifiedId",
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
                name: "FK_ItemPrices_AspNetUsers_UserModifiedId",
                table: "ItemPrices",
                column: "UserModifiedId",
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
                name: "FK_ItemTypes_AspNetUsers_UserModifiedId",
                table: "ItemTypes",
                column: "UserModifiedId",
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
                name: "FK_PackReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PackReturnLineItems",
                column: "UserModifiedId",
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
                name: "FK_Packs_AspNetUsers_UserModifiedId",
                table: "Packs",
                column: "UserModifiedId",
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
                name: "FK_Payments_AspNetUsers_UserModifiedId",
                table: "Payments",
                column: "UserModifiedId",
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
                name: "FK_PurchaseOrderLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseOrderLineItems",
                column: "UserModifiedId",
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
                name: "FK_PurchaseReturnLineItems_AspNetUsers_UserModifiedId",
                table: "PurchaseReturnLineItems",
                column: "UserModifiedId",
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
                name: "FK_SalesOrderDeliveryMonitorings_AspNetUsers_UserModifiedId",
                table: "SalesOrderDeliveryMonitorings",
                column: "UserModifiedId",
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
                name: "FK_SalesOrders_AspNetUsers_UserModifiedId",
                table: "SalesOrders",
                column: "UserModifiedId",
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
                name: "FK_SalesReturnLineItems_AspNetUsers_UserModifiedId",
                table: "SalesReturnLineItems",
                column: "UserModifiedId",
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
                name: "FK_SalesReturnTotalReturnAmounts_AspNetUsers_UserModifiedId",
                table: "SalesReturnTotalReturnAmounts",
                column: "UserModifiedId",
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
                name: "FK_SOACRChecks_AspNetUsers_UserModifiedId",
                table: "SOACRChecks",
                column: "UserModifiedId",
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
                name: "FK_SOACRs_AspNetUsers_UserModifiedId",
                table: "SOACRs",
                column: "UserModifiedId",
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
                name: "FK_Suppliers_AspNetUsers_UserModifiedId",
                table: "Suppliers",
                column: "UserModifiedId",
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
                name: "FK_TransferItems_AspNetUsers_UserModifiedId",
                table: "TransferItems",
                column: "UserModifiedId",
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
    }
}
