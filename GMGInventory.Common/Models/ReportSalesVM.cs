using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class AgentSalesPerItemRVM
    {
        [Display(Name = "Agent")]
        public string Agent { get; set; }

        public int AgentId { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Price")]
        public string Price => string.Format(SystemFormat.StringDecimalComma, PriceD);
        public decimal PriceD { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD => QuantityD * PriceD;

        [Display(Name = "Date Invoice")]
        public string DateInvoice => DateInvoiceD.ToString(SystemFormat.StringDate);
        public DateTime DateInvoiceD { get; set; }

        [Display(Name = "BIR Invoice Number")]
        public string ControlNumber { get; set; }

        public int InvoiceId { get; set; }
    }

    public class AgentSalesPerItemPVM
    {
        public IList<AgentSalesPerItemRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Agent { get; set; }
        public string Brand { get; set; }
        public string Item { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringIntegerComma, TotalQuantityD);
        public int TotalQuantityD { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class BrandSalesRVM
    {
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Agent Code")]
        public string AgentCode { get; set; }

        public int AgentId { get; set; }

        [Display(Name = "Agent Name")]
        public string AgentName { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
    }

    public class BrandSalesPVM
    {
        public IList<BrandSalesRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Agent { get; set; }
        public string Brand { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class BestSellingItemRVM
    {
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Current Quantity")]
        public string CurrentQuantity => string.Format(SystemFormat.StringIntegerComma, CurrentQuantityD);
        public int CurrentQuantityD { get; set; }

        [Display(Name = "Unit Price")]
        public string UnitPrice => string.Format(SystemFormat.StringDecimalComma, UnitPriceD);
        public decimal UnitPriceD { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
    }

    public class BestSellingItemPVM
    {
        public IList<BestSellingItemRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Basis { get; set; }
        public string Brand { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringDecimalComma, TotalQuantityD);
        public decimal TotalQuantityD { get; set; }
        public string TotalCurrentQuantity => string.Format(SystemFormat.StringDecimalComma, TotalCurrentQuantityD);
        public decimal TotalCurrentQuantityD { get; set; }
        public string TotalUnitPrice => string.Format(SystemFormat.StringDecimalComma, TotalUnitPriceD);
        public decimal TotalAmountD { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalUnitPriceD);
        public decimal TotalUnitPriceD { get; set; }
    }

    public class CommissionRVM
    {
        public int AgentId { get; set; }

        [Display(Name = "Invoice Date")]
        public string InvoiceDate => InvoiceDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoiceDateD { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        public int InvoiceId { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "Agent")]
        public string Agent { get; set; }

        [Display(Name = "Area")]
        public string Area { get; set; }

        [Display(Name = "Agent Code")]
        public string AgentCode { get; set; }

        [Display(Name = "Commission Rate")]
        public string CommissionRate => string.Format(SystemFormat.StringDecimalComma, CommissionRateD) + "%";
        public decimal CommissionRateD { get; set; }

        [Display(Name = "Invoice Amount")]
        public string InvoiceAmount => string.Format(SystemFormat.StringDecimalComma, InvoiceAmountD);
        public decimal InvoiceAmountD { get; set; }

        [Display(Name = "Commission Amount")]
        public string CommissionAmount => string.Format(SystemFormat.StringDecimalComma, CommissionAmountD);
        public decimal CommissionAmountD => (CommissionRateD * InvoiceAmountD) / 100;

        public string CommissionAmount2 => string.Format(SystemFormat.StringDecimalComma, CommissionAmount2D);
        public decimal CommissionAmount2D { get; set; }

        public string AgentTotalInvoice => string.Format(SystemFormat.StringDecimalComma, AgentTotalInvoiceD);
        public decimal AgentTotalInvoiceD { get; set; }

        public string AgentCommissionRate => string.Format(SystemFormat.StringDecimalComma, AgentCommissionRateD);
        public decimal AgentCommissionRateD { get; set; }
    }

    public class CommissionPVM
    {
        public IList<CommissionRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Customer { get; set; }

        public string Agent { get; set; }
        public string AgentCode { get; set; }
        public string Area { get; set; }
        public string TotalInvoiceAmount => string.Format(SystemFormat.StringDecimalComma, TotalInvoiceAmountD);
        public decimal TotalInvoiceAmountD { get; set; }
        public string TotalCommissionAmount => string.Format(SystemFormat.StringDecimalComma, TotalCommissionAmountD);
        public decimal TotalCommissionAmountD { get; set; }

        public string AgentTotalInvoice => string.Format(SystemFormat.StringDecimalComma, AgentTotalInvoiceD);
        public decimal AgentTotalInvoiceD { get; set; }


        public string AgentCommissionRate => string.Format(SystemFormat.StringDecimalComma, AgentCommissionRateD);
        public decimal AgentCommissionRateD { get; set; }


    }

    public class CustomerSalesRankingRVM
    {
        [Display(Name = "Rank")]
        public string Rank => string.Format(SystemFormat.StringIntegerComma, RankD);
        public int RankD { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "Total Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
    }

    public class CustomerSalesRankingPVM
    {
        public IList<CustomerSalesRankingRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Region { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class CustomerSalesRVM
    {
        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
    }

    public class CustomerSalesPVM
    {
        public IList<CustomerSalesRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Customer { get; set; }
        public string Brand { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class CustomerSalesComparePVM
    {
        public CustomerSalesPVM CustomerSales1 { get; set; }
        public CustomerSalesPVM CustomerSales2 { get; set; }
    }

    public class DailySalesRVM
    {
        [Display(Name = "Invoice Date")]
        public string InvoiceDate => InvoiceDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoiceDateD { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        public int InvoiceId { get; set; }

        [Display(Name = "BIR Invoice Number")]
        public string BIRInvoiceNumber { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Agent")]
        public string Agent { get; set; }

        public int AgentId { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }

        [Display(Name = "Percent")]
        public string Percent => string.Format(SystemFormat.StringIntegerComma, Math.Round(PercentD));
        public decimal PercentD { get; set; }

        //[Display(Name = "Commission Rate")]
        //public string CommissionRate => string.Format(SystemFormat.StringDecimalComma, CommissionRateD);
        //public decimal CommissionRateD { get; set; }

        [Display(Name = "Total Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
    }

    public class DailySalesPVM
    {
        public IList<DailySalesRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string InvoiceNumberFrom { get; set; }
        public string InvoiceNumberTo { get; set; }
        public string Status { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class SalesInvoiceRVM
    {
        public int Id { get; set; }
        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Control Number")]
        public string ControlNumber { get; set; }

        [Display(Name = "Invoice Date")]
        public string InvoiceDate => InvoiceDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoiceDateD { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "Agent")]
        public string Agent { get; set; }
        public string AgentCode { get; set; }

        public int AgentId { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Trans Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
    }

    public class SalesInvoicePVM
    {
        public IList<SalesInvoiceRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Customer { get; set; }
        public string Agent { get; set; }
        public string Status { get; set; }
        public string Region { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class SalesAgentAreaRVM
    {
        public int AgentId { get; set; }

        [Display(Name = "Agent Code")]
        public string AgentCode { get; set; }

        [Display(Name = "Agent Name")]
        public string AgentName { get; set; }

        [Display(Name = "Area")]
        public string Area { get; set; }

        [Display(Name = "Sales")]
        public string Sales => string.Format(SystemFormat.StringDecimalComma, SalesD);
        public decimal SalesD { get; set; }

        [Display(Name = "Percent of Sales")]
        public string PercentSales => string.Format(SystemFormat.StringDecimalComma, PercentSalesD) + "%";
        public decimal PercentSalesD { get; set; }

        public string AgentTotalSales => string.Format(SystemFormat.StringDecimalComma, AgentTotalSalesD);
        public decimal AgentTotalSalesD { get; set; }
    }

    public class SalesAgentAreaPVM
    {
        public IList<SalesAgentAreaRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Agent { get; set; }
        public string Area { get; set; }
        public string TotalSales => string.Format(SystemFormat.StringDecimalComma, TotalSalesD);
        public decimal TotalSalesD { get; set; }
    }

    public class SalesReportBasisRVM
    {
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public int Id { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
    }

    public class SalesReportBasisPVM
    {
        public IList<SalesReportBasisRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Basis { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class SalesReturnRVM
    {
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Sales Return Number")]
        public string SRNumber { get; set; }

        public int SRId { get; set; }

        [Display(Name = "Return Date")]
        public FormattedDate ReturnDate => new FormattedDate(ReturnDateD);
        public DateTime ReturnDateD { get; set; }

        [Display(Name = "Order Date")]
        public FormattedDate? OrderDate => OrderDateD == null ? null : new FormattedDate(OrderDateD.Value);
        public DateTime? OrderDateD { get; set; }

        [Display(Name = "Delivery Date")]
        public FormattedDate? DeliveryDate => DeliveryDateD == null ? null : new FormattedDate(DeliveryDateD.Value);
        public DateTime? DeliveryDateD { get; set; }

        [Display(Name = "Pickup Date")]
        public FormattedDate? PickupDate => PickupDateD == null ? null : new FormattedDate(PickupDateD.Value);
        public DateTime? PickupDateD { get; set; }

        [Display(Name = "Reason of Return")]
        public string ReturnReason { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "Agent")]
        public string Agent { get; set; }

        public int AgentId { get; set; }

        [Display(Name = "Sales Invoice Number")]
        public string? SINumber { get; set; }

        public int? SIId { get; set; }

        [Display(Name = "Sales Order Number")]
        public string? SONumber { get; set; }

        public int? SOId { get; set; }

        [Display(Name = "Control Number")]
        public string ControlNumber { get; set; }

        [Display(Name = "Quantity Returned")]
        public string QuantityReturned => string.Format(SystemFormat.StringIntegerComma, QuantityReturnedD);
        public int QuantityReturnedD { get; set; }

        [Display(Name = "Price Returned")]
        public string PriceReturned => string.Format(SystemFormat.StringDecimalComma, PriceReturnedD);
        public decimal PriceReturnedD { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD => QuantityReturnedD * PriceReturnedD;
    }

    public class SalesReturnPVM
    {
        public IList<SalesReturnRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Customer { get; set; }
        public string Item { get; set; }
        public string TotalQuantityReturned => string.Format(SystemFormat.StringDecimalComma, TotalQuantityReturnedD);
        public decimal TotalQuantityReturnedD { get; set; }
        public string TotalPriceReturned => string.Format(SystemFormat.StringDecimalComma, TotalPriceReturnedD);
        public decimal TotalPriceReturnedD { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class UnservedItemRVM
    {
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Old Code")]
        public string OldCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Pending")]
        public string Pending => string.Format(SystemFormat.StringIntegerComma, PendingD);
        public int PendingD { get; set; }

        [Display(Name = "Current")]
        public string Current => string.Format(SystemFormat.StringIntegerComma, CurrentD);
        public int CurrentD { get; set; }

        [Display(Name = "Last Replenish Date")]
        public string ReplenishDate => ReplenishDateD.ToString(SystemFormat.StringDate);
        public DateTime ReplenishDateD { get; set; }
    }

    public class UnservedItemPVM
    {
        public IList<UnservedItemRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Brand { get; set; }
        public string TotalPending => string.Format(SystemFormat.StringIntegerComma, TotalPendingD);
        public int TotalPendingD { get; set; }
        public string TotalCurrent => string.Format(SystemFormat.StringIntegerComma, TotalCurrentD);
        public int TotalCurrentD { get; set; }
    }

    public class OutboundRVM
    {
        [Display(Name = "Old SKU / Code")]
        public string? OldSkuCode { get; set; }

        [Display(Name = "Invoice Date")]
        public string InvoiceDate => InvoiceDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoiceDateD { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Gross Pince")]
        public string GrossPrince => string.Format(SystemFormat.StringIntegerComma, GrossPrinceD);
        public decimal GrossPrinceD { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SalesOrderNumber { get; set; }

        public int SalesOrderId { get; set; }

        [Display(Name = "BIR Invoice Number")]
        public string ControlNumber { get; set; }

        public int InvoiceId { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "Area")]
        public string Area { get; set; }

    }

    public class OutboundPVM
    {
        public IList<OutboundRVM> Outbounds { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Brand { get; set; }
        public string Region { get; set; }
        public string Area { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringIntegerComma, TotalQuantityD);
        public int TotalQuantityD { get; set; }

    }
}