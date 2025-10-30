using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PurchaseReceiveFilter
    {
        public int? SupplierId { get; set; }
        public int? WarehouseId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? StatusId { get; set; }
    }

    public class PurchaseReceiveVM : APIHM_VM
    {
        public int Id { get; set; }

        [Display(Name = "Doc Number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "PO Number")]
        public string PONumber { get; set; }
        public int POId { get; set; }

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }
        public int SupplierId { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }
        public int TermId { get; set; }

        [Display(Name = "Warehouse")]
        public string Warehouse { get; set; }
        public int WarehouseId { get; set; }

        [Display(Name = "Delivery Receipt Number")]
        public string DRNumber { get; set; }

        [Display(Name = "Sales Invoice Number")]
        public string SINumber { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Discount 3")]
        public string Discount3 => string.Format(SystemFormat.StringDecimalComma, Discount3D);
        public decimal Discount3D { get; set; }

        [Display(Name = "Is VAT Included?")]
        public string IsVATIncluded { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Received")]
        public string ReceiveDate => ReceiveDateD.ToString(SystemFormat.StringDate2);
        public DateTime ReceiveDateD { get; set; }
        public int? ResubmitId { get; set; }
        public int? ResubmitFromId { get; set; }

        public string? DisapprovedReason { get; set; }
        public string? Remarks { get; set; }

    }

    public class PurchaseReceiveDetailsVM : PurchaseReceiveVM
    {
        [Display(Name = "Currency")]
        public string Currency { get; set; }
        public int CurrencyId { get; set; }

        [Display(Name = "Currency Rate")]
        public string CurrencyRate => string.Format("{0:#,##0.00}", CurrencyRateD);
        public decimal CurrencyRateD { get; set; }

        public string PurchaseRequestType { get; set; }
        public string PurchaseOrderType { get; set; }


        [Display(Name = "Approved By")]
        public string? ApprovedBy { get; set; }

        [Display(Name = "Disapproved By")]
        public string? DisapprovedBy { get; set; }



        [Display(Name = "Created By")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }

        [Display(Name = "Modified By")]
        public string? UserModified { get; set; }

        [Display(Name = "Date Modified")]
        public string DateModified => DateModifiedD == null ? "-" : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }
    }

    public class PurchaseReceiveLineItemVM
    {
        public int Id { get; set; }
        public bool ToReceive { get; set; }

        public int PurchaseReceivingId { get; set; }

        [Display(Name = "Line Number")]
        public int LineNumber { get; set; }

        public int OrderItemId { get; set; }
        public int? ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string? ItemCode { get; set; }

        [Display(Name = "Item Part Number")]
        public string? ItemPartNumber { get; set; }

        [Display(Name = "Item Description")]
        public string? ItemDescription { get; set; }

        public int UnitId { get; set; }

        [Display(Name = "UOM")]
        public string UnitCode { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format("{0:#,##0}", QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Order Quantity")]
        public string OrderQuantity => string.Format("{0:#,##0}", OrderQuantityD);
        public int OrderQuantityD { get; set; }

        public string ServedQuantity => string.Format("{0:#,##0}", ServedQuantityD);
        public int ServedQuantityD { get; set; }

        [Display(Name = "To Receive Quantity")]
        public string ToReceiveQuantity => string.Format("{0:#,##0}", ToReceiveQuantityD);
        public int ToReceiveQuantityD => OrderQuantityD - ServedQuantityD;

        [Display(Name = "Unit Cost")]
        public string UnitCost => string.Format("{0:#,##0.00}", UnitCostD);
        public decimal UnitCostD { get; set; }

        [Display(Name = "Total Amount")]
        public string TotalAmount => string.Format("{0:#,##0.00}", TotalAmountD);
        public decimal TotalAmountD => QuantityD * UnitCostD;
    }

    public class PurchaseReceiveSaveVM
    {
        public string DocumentNumber { get; set; }
        public int PurchaseOrderId { get; set; }
        public int SupplierId { get; set; }
        public int TermId { get; set; }
        public int WarehouseId { get; set; }
        public string DeliveryReceiptNumber { get; set; }
        public string SalesInvoiceNumber { get; set; }

        public decimal Discount1 { get; set; }
        public decimal Discount2 { get; set; }

        public DateTime DateReceived { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int? ResubmitId { get; set; }

        public PurchaseReceiveLineItemVM Items { get; set; }
        public int OrderItemId { get; set; }

    }

    public class PurchaseReceiveFormAPIVM
    {
        public int PurchaseOrderId { get; set; }
        public string DeliveryReceiptNumber { get; set; }
        public string SalesInvoiceNumber { get; set; }
        public DateTime DateReceived { get; set; }
        public string Remarks { get; set; }
        public IList<PurchaseReceiveFormItemAPIVM> Items { get; set; }
    }

    public class PurchaseReceiveFormItemAPIVM
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
    }
}
