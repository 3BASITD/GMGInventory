using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class UserCreatedFK_Undo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Warehouses",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Warehouses",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "TransferItems",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "TransferItems",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "TransferItemDetails",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "TransferItemDetails",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Suppliers",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Suppliers",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "StockCards",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "StockCards",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "SOACRs",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "SOACRs",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "SOACRLineItems",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "SOACRLineItems",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "SOACRChecks",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "SOACRChecks",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "ShippingAddresses",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "ShippingAddresses",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "SalesReturnTotalReturnAmounts",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "SalesReturnTotalReturnAmounts",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "SalesReturns",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "SalesReturns",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "SalesReturnLineItems",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "SalesReturnLineItems",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "SalesReturnAppliedAmounts",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "SalesReturnAppliedAmounts",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "SalesOrders",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "SalesOrders",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "SalesOrderLineItems",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "SalesOrderLineItems",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "SalesOrderDeliveryMonitorings",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "SalesOrderDeliveryMonitorings",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "PurchaseReturns",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "PurchaseReturns",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "PurchaseReturnLineItems",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "PurchaseReturnLineItems",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "PurchaseOrders",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "PurchaseOrders",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "PurchaseOrderLineItems",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "PurchaseOrderLineItems",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "PhysicalCountHistories",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "PhysicalCountHistories",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Payments",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Payments",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "PaymentDetails",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "PaymentDetails",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Packs",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Packs",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "PackReturns",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "PackReturns",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "PackReturnLineItems",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "PackReturnLineItems",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "PackLineItems",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "PackLineItems",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "ItemTypes",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "ItemTypes",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Items",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Items",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "ItemPrices",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "ItemPrices",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "ItemInventories",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "ItemInventories",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Invoices",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Invoices",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "InvoicePayments",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "InvoicePayments",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "InvoiceLineItems",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "InvoiceLineItems",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Forwarders",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Forwarders",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Customers",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Customers",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "CustomerPayments",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "CustomerPayments",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "CustomerPaymentDetails",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "CustomerPaymentDetails",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "CreditTerms",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "CreditTerms",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "CreditDebitMemos",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "CreditDebitMemos",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Categories",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Categories",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Brands",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Brands",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Areas",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Areas",
                newName: "UserCreated");

            migrationBuilder.RenameColumn(
                name: "UserModifiedId",
                table: "Agents",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UserCreatedId",
                table: "Agents",
                newName: "UserCreated");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "55f270b4-4118-49cf-a6b5-aca55e6edbe6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "ad668d30-298e-4f65-b672-28aee2fba64a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "27ec3132-7299-46c8-b7f3-8c75fb60d6c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "0059da36-42fa-485f-81c8-e2cc7e9456e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "022ea87e-d58a-47f6-89bc-054dacfb8074");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "5f2823b4-2c31-41e7-a0d9-43dcac26bde4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a68e52f9-0bfd-484e-9eeb-583f06b4e478", new DateTime(2022, 7, 29, 15, 1, 5, 582, DateTimeKind.Local).AddTicks(4877), "AQAAAAEAACcQAAAAEMLGh21uda7ccr5QD78FQ1grCBvqVlgT+YW0wh9Xo3eSINgZZil+IRsS55jHkseojA==", "b62eb847-2a77-4781-a5c9-ad77f99b4ae5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "808a0bde-9500-418e-87d1-0c65793fd685", new DateTime(2022, 7, 29, 15, 1, 5, 573, DateTimeKind.Local).AddTicks(8975), "AQAAAAEAACcQAAAAEOlHWZJWiNaxRZfIOFKLeNssRA/lsx1ZOT7w0pAK3oi6MxXUzAprFzPNnv5vlh4dCw==", "3545c44f-59dd-46ff-a9b6-9e8bf4ad476c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Warehouses",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Warehouses",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "TransferItems",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "TransferItems",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "TransferItemDetails",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "TransferItemDetails",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Suppliers",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Suppliers",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "StockCards",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "StockCards",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "SOACRs",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "SOACRs",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "SOACRLineItems",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "SOACRLineItems",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "SOACRChecks",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "SOACRChecks",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "ShippingAddresses",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "ShippingAddresses",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "SalesReturnTotalReturnAmounts",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "SalesReturnTotalReturnAmounts",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "SalesReturns",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "SalesReturns",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "SalesReturnLineItems",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "SalesReturnLineItems",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "SalesReturnAppliedAmounts",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "SalesReturnAppliedAmounts",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "SalesOrders",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "SalesOrders",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "SalesOrderLineItems",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "SalesOrderLineItems",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "SalesOrderDeliveryMonitorings",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "SalesOrderDeliveryMonitorings",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "PurchaseReturns",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "PurchaseReturns",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "PurchaseReturnLineItems",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "PurchaseReturnLineItems",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "PurchaseOrders",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "PurchaseOrders",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "PurchaseOrderLineItems",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "PurchaseOrderLineItems",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "PhysicalCountHistories",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "PhysicalCountHistories",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Payments",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Payments",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "PaymentDetails",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "PaymentDetails",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Packs",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Packs",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "PackReturns",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "PackReturns",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "PackReturnLineItems",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "PackReturnLineItems",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "PackLineItems",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "PackLineItems",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "ItemTypes",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "ItemTypes",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Items",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Items",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "ItemPrices",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "ItemPrices",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "ItemInventories",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "ItemInventories",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Invoices",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Invoices",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "InvoicePayments",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "InvoicePayments",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "InvoiceLineItems",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "InvoiceLineItems",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Forwarders",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Forwarders",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Customers",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Customers",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "CustomerPayments",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "CustomerPayments",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "CustomerPaymentDetails",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "CustomerPaymentDetails",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "CreditTerms",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "CreditTerms",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "CreditDebitMemos",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "CreditDebitMemos",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Categories",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Categories",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Brands",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Brands",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Areas",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Areas",
                newName: "UserCreatedId");

            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "Agents",
                newName: "UserModifiedId");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "Agents",
                newName: "UserCreatedId");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");

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
                principalColumn: "Id");
        }
    }
}
