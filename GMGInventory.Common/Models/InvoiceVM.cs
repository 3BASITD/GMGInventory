using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace GMGInventory.Common.Models
{
    public class InvoiceVM : APIHM_VM
    {
        public int Id { get; set; }

        [Display(Name = "Invoice Date")]
        public string InvoiceDate => InvoiceDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoiceDateD { get; set; }

        [Display(Name = "Invoice Due Date")]
        public string InvoiceDueDate => InvoiceDueDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoiceDueDateD { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "BIR Invoice Number")]
        public string BIRInvoiceNumber { get; set; }

        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [Display(Name = "Pack Number")]
        public int PackNumber { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Customer Priority")]
        public string Priority => SystemFormat.CustomerPriority(PriorityD);
        public int PriorityD { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }
        public int SalesOrderId { get; set; }

        [Display(Name = "Total Amount")]
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "TBR")]
        public string TBR => string.Format(SystemFormat.StringDecimalComma, TBRD);

        public decimal? TBRD { get; set; }

        [Display(Name = "Credit")]
        public string Credit => string.Format(SystemFormat.StringDecimalComma, CreditD);

        public decimal? CreditD { get; set; }

        [Display(Name = "Debit")]
        public string Debit => string.Format(SystemFormat.StringDecimalComma, DebitD);

        public decimal? DebitD { get; set; }

        [Display(Name = "Paid Amount")]
        public string PaidAmount => string.Format(SystemFormat.StringDecimalComma, PaidAmountD);

        public decimal? PaidAmountD { get; set; }

        [Display(Name = "Freight Charge")]
        public string FreightCharge => string.Format(SystemFormat.StringDecimalComma, FreightChargeD);

        public decimal? FreightChargeD { get; set; }

        [Display(Name = "Balance")]
        public string Balance => string.Format(SystemFormat.StringDecimalComma, BalanceD);
        //public decimal? BalanceD { get; set; }
        public decimal BalanceD => TotalAmountD - (TBRD ?? 0) - (CreditD ?? 0) + (DebitD ?? 0) - (PaidAmountD ?? 0m) + (FreightChargeD ?? 0);

   
    }

    public class InvoiceDetailsVM : APIHM_VM
    {
        public int Id { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "BIR Invoice Number")]
        public string ControlNumber { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        public string? CustomerAddress { get; set; }

        [Display(Name = "TIN")]
        public string? TIN { get; set; }

        [Display(Name = "Payment Terms")]
        public string? PaymentTerms { get; set; }

        [Display(Name = "Agent")]
        public string Agent { get; set; }
        public int AgentId { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }
        public int SalesOrderId { get; set; }

        [Display(Name = "Purchase Order Number")]
        public string? PONumber { get; set; }

        [Display(Name = "Pack Number")]
        public int PackNumber { get; set; }

        [Display(Name = "Customer Priority")]
        public string Priority => SystemFormat.CustomerPriority(PriorityD);
        public int PriorityD { get; set; }

        [Display(Name = "Customer Branch")]
        public string? Branch { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Invoice Date")]
        public string InvoiceDate1 => InvoiceDateD.ToString(SystemFormat.StringDate);
        public string InvoiceDate3 => InvoiceDateD.ToString(SystemFormat.StringDate3);
        public DateTime InvoiceDateD { get; set; }

        [Display(Name = "Invoice Due Date")]
        public string? InvoiceDueDate1 => InvoiceDueDateD == null ? null : InvoiceDueDateD.Value.ToString(SystemFormat.StringDate);
        public string? InvoiceDueDate3 => InvoiceDueDateD == null ? null : InvoiceDueDateD.Value.ToString(SystemFormat.StringDate3);
        public DateTime? InvoiceDueDateD { get; set; }

        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }

        public int InvoiceTypeId { get; set; }

        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [Display(Name = "Created By")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }

        [Display(Name = "Modified By")]
        public string? UserModified { get; set; }

        [Display(Name = "Date Modified")]
        public string? DateModified => DateModifiedD?.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }

        public string LessVat => string.Format(SystemFormat.StringDecimalComma, LessVatD);
        public decimal LessVatD { get; set; }
        public string AmountNetVat => string.Format(SystemFormat.StringDecimalComma, AmountNetVatD);
        public decimal AmountNetVatD { get; set; }
        public string WithHoldingTax => string.Format(SystemFormat.StringDecimalComma, WithHoldingTaxD);
        public decimal WithHoldingTaxD { get; set; }
        public string AddVat => string.Format(SystemFormat.StringDecimalComma, AddVatD);
        public decimal AddVatD { get; set; }

        public string TotalAmoutDue => string.Format(SystemFormat.StringDecimalComma, TotalAmoutDueD);
        public decimal TotalAmoutDueD { get; set; }
    }

    public class InvoiceDetailsWithItemsVM : InvoiceDetailsVM
    {
        public IList<InvoiceItemVM> Items { get; set; }

        [Display(Name = "Total")]
        public string Total => string.Format(SystemFormat.StringDecimalComma, TotalD);
        public decimal TotalD { get; set; }

        [Display(Name = "Discount %")]
        public string DiscountPercent => string.Format(DiscountPercentD % 1 > 0 ? SystemFormat.StringDecimalComma : SystemFormat.StringIntegerComma, DiscountPercentD) + "%";

        public decimal DiscountPercentD { get; set; }

        [Display(Name = "Discount Amount")]
        public string DiscountAmount => string.Format(SystemFormat.StringDecimalComma, DiscountAmountD);
        public decimal DiscountAmountD => TotalD - DiscountedTotalD;

        [Display(Name = "Discounted Total")]
        public string DiscountedTotal => string.Format(SystemFormat.StringDecimalComma, DiscountedTotalD);
        public decimal DiscountedTotalD { get; set; }

        [Display(Name = "VAT Exclusive")]
        public string VATExclusive => string.Format(SystemFormat.StringDecimalComma, VATExclusiveD);
        //public decimal VATExclusiveD => DiscountedTotalD * 0.88m;
        public decimal VATExclusiveD => DiscountedTotalD / 1.12m;

        [Display(Name = "VAT Amount")]
        public string VATAmount => string.Format(SystemFormat.StringDecimalComma, VATAmountD);
        //public decimal VATAmountD => DiscountedTotalD * 0.12m;
        public decimal VATAmountD => VATExclusiveD * 0.12m;

        [Display(Name = "Total Amount")]
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }

    }

    public class InvoiceItemVM
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Old Item Code")]
        public string OldItemCode { get; set; }

        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }
        public int QuantityOfPack { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Net Price")]
        public string NetPrice => string.Format(SystemFormat.StringDecimalComma, NetPriceD);
        public decimal NetPriceD { get; set; }
        public string SellingPriceType { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format(Discount1D % 1 > 0 ? SystemFormat.StringDecimalComma : SystemFormat.StringIntegerComma, Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format(Discount2D % 1 > 0 ? SystemFormat.StringDecimalComma : SystemFormat.StringIntegerComma, Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Gross Price")]
        public string GrossPrice => string.Format(SystemFormat.StringDecimalComma, GrossPriceD);
        public decimal GrossPriceD => Math.Round(QuantityD * NetPriceD * (100 - Discount1D) / 100 * (100 - Discount2D) / 100, 2);

        [Display(Name = "Gross Price (No Discount)")]
        public string GrossPriceNoDiscount => string.Format(SystemFormat.StringDecimalComma, GrossPriceNoDiscountD);
        public decimal GrossPriceNoDiscountD => QuantityD * NetPriceD;
        public string ReferenceNumber { get; set; }
        public int? ItemPromoId { get; set; }
        public bool? IsPromo { get; set; }
    }

    //public class InvoiceFormUpdateVM
    //{
    //    public int Id { get; set; }
    //    public string BIRInvoiceNumber { get; set; }
    //    public int InvoiceType { get; set; }
    //    public DateTime InvoiceDate { get; set; }
    //    public string Remarks { get; set; }
    //    public string SavingAs { get; set; }
    //    public IList<InvoiceFormItemVM> Items { get; set; }

    //}

    public class InvoiceFormVM
    {
        public int? Id { get; set; }
        public int PackId { get; set; }
        public string BIRInvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int InvoiceType { get; set; }
        public string Remarks { get; set; }
        public int Agent { get; set; }
        public string SavingAs { get; set; }
        public IList<InvoiceFormItemVM> Items { get; set; }
    }

    public class InvoiceFormItemVM
    {
        public int Item { get; set; }
        public int Quantity { get; set; }
        public bool? isPromo { get; set; }
    }
}