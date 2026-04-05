using System.Reflection;

namespace GMGInventory.Common.Constants
{
    public enum PageNames
    {
        [PageNamesString("Customer Purchase History")] CustomerPurchaseHistory = 1,
        [PageNamesString("Packing Listing")] PackingListing,
        [PageNamesString("Sales Invoice")] SalesInvoice,
        [PageNamesString("Sales Order")] SalesOrder,
        [PageNamesString("Sales Order Delivery")] SalesOrderDelivery,
        [PageNamesString("Sales Return")] SalesReturn,
        [PageNamesString("Item")] Item,
        [PageNamesString("Price List")] PriceList,
        [PageNamesString("Price Update")] PriceUpdate,
        [PageNamesString("Purchase Return")] PurchaseReturn, // 10
        [PageNamesString("Replenish Inventory")] ReplenishInventory,
        [PageNamesString("Warehouse")] Warehouse,
        [PageNamesString("Warehouse Transfer")] WarehouseTransfer,
        [PageNamesString("Unserved Item")] UnservedItem,
        [PageNamesString("Purchase Request")] PurchaseRequest,
        [PageNamesString("Purchase Order")] PurchaseOrder,
        [PageNamesString("Purchase Receiving")] PurchaseReceiving,
        [PageNamesString("Purchase Return (New)")] PurchaseReturnNew,
        [PageNamesString("Customer")] Customer,
        [PageNamesString("Payment")] Payment, // 20
        [PageNamesString("SOA / Counter Receipt")] SOACR,
        [PageNamesString("Check / Cheque")] Cheque,
        [PageNamesString("Agents")] Agents,
        [PageNamesString("Accesses")] Accesses,
        [PageNamesString("Areas")] Areas,
        [PageNamesString("Brands")] Brands,
        [PageNamesString("Categories")] Categories,
        [PageNamesString("Forwarders")] Forwarders,
        [PageNamesString("Item Types")] ItemTypes,
        [PageNamesString("Location Codes")] LocationCodes, // 30
        [PageNamesString("Reasons of Return")] ReasonsOfReturn,
        [PageNamesString("Regions")] Regions,
        [PageNamesString("Suppliers")] Suppliers,
        [PageNamesString("Users")] Users,
        [PageNamesString("Home Image")] HomeImage,
        [PageNamesString("Audit Logs")] AuditLogs,
        [PageNamesString("Agent Sales per Item")] AgentSalesPerItemReport,
        [PageNamesString("Brand Sales Report")] BrandSalesReport,
        [PageNamesString("Best Selling Items")] BestSellingItemsReport,
        [PageNamesString("Commision Report")] CommissionReport, // 40
        [PageNamesString("Customer Sales Ranking Report")] CustomerSalesRankingReport,
        [PageNamesString("Customer Sales Report")] CustomerSalesReport,
        [PageNamesString("Daily Sales Report")] DailySalesReport,
        [PageNamesString("Sales Invoice Listing Report")] SalesInvoiceListingReport,
        [PageNamesString("Sales by Agent/Area")] SalesByAgentAreaReport,
        [PageNamesString("Sales Report by Basis")] SalesReportByBasisReport,
        [PageNamesString("Sales Return Listing Report")] SalesReturnListingReport,
        [PageNamesString("Unserved Item Report")] UnservedItemReport,
        [PageNamesString("Below Maintaining Items")] BelowMaintainingItemsReport,
        [PageNamesString("Receiving Listing")] ReceivingListingReport, // 50
        [PageNamesString("Receiving Listing by Supplier")] ReceivingListingBySupplierReport,
        [PageNamesString("Receiving Summary")] ReceivingSummaryReport,
        [PageNamesString("Stock Card")] StockCardReport,
        [PageNamesString("Stock Status Report")] StockStatusReport,
        [PageNamesString("Supplier Purchase History")] SupplierPurchaseHistoryReport,
        [PageNamesString("Credit Memo Listing Report")] CreditMemoListingReport,
        [PageNamesString("Debit Memo Listing Report")] DebitMemoListingReport,
        [PageNamesString("Purchase Order Report")] PurchaseOrderReport,
        [PageNamesString("Purchase Return Report")] PurchaseReturnReport,
        [PageNamesString("Purchase Receiving Report")] PurchaseReceivingReport, // 60
        [PageNamesString("Purchase Receiving Basic")] PurchaseReceivingBasic,
        [PageNamesString("Item Basic")] ItemBasic,
        [PageNamesString("Receptacle")] Receptacle,
        [PageNamesString("Shipping List")] ShippingList,
        [PageNamesString("Tracker Task")] TrackerTask,
        [PageNamesString("Tracker Comment")] TrackerComment,
        [PageNamesString("Tracker Issue")] TrackerIssue,
        [PageNamesString("HashMicro (Invoice)")] HashMicroInvoice,
        [PageNamesString("HashMicro (Bill)")] HashMicroBill,
        [PageNamesString("Driver")] Driver, // 70
        [PageNamesString("Vehicle")] Vehicle,
        [PageNamesString("Currency")] Currency,
        [PageNamesString("Service Level Agreement")] ServiceLevelAgreement,
        [PageNamesString("Sales Order (Special)")] SalesOrderSpecial,
        [PageNamesString("Purchase Request (Automotive)")] PurchaseRequestAutomotive,
        [PageNamesString("Order Queue")] OrderQueue,
        [PageNamesString("Purchase Request (Non-Automotive)")] PurchaseRequestNonAutomotive,
        [PageNamesString("Warehouse Transfer Report")] WarehouseTransferReport,
        [PageNamesString("Daily Outbound Report")] DailyOutboundReport,
        [PageNamesString("Barcotech Sync Status")] BarcotechSync, // 80
        [PageNamesString("Purchase Receiving (Special)")] PurchaseReceivingSpecial,
        [PageNamesString("Barcotech (Stock)")] BarcotechStock
    }

    public static class PageNameExtensions
    {
        public static string ToEnumString(this PageNames enumValue)
        {
            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                var attribute = memberInfo.GetCustomAttribute<PageNamesStringAttribute>();
                if (attribute != null)
                {
                    return attribute.StringValue;
                }
            }

            return "Unknown";
        }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class PageNamesStringAttribute : Attribute
    {
        public string StringValue { get; }

        public PageNamesStringAttribute(string stringValue)
        {
            StringValue = stringValue;
        }
    }
}