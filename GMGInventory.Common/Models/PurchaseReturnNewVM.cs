using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PurchaseReturnFilter
    {
        public int? SupplierId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? StatusId { get; set; }
    }

    public class PurchaseReturnNewVM
    {
        public int Id { get; set; }

        [Display(Name = "Document Number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "PO Number")]
        public string PONumber { get; set; }

        [Display(Name = "RR Number")]
        public string RRNumber { get; set; }

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }

        [Display(Name = "Warehouse")]
        public string Warehouse { get; set; }

        [Display(Name = "DR Number")]
        public string DRNumber { get; set; }

        [Display(Name = "SI Number")]
        public string SINumber { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Returned")]
        public string ReturnDate => ReturnDateD.ToString(SystemFormat.StringDate);
        public string ReturnDate3 => ReturnDateD.ToString(SystemFormat.StringDate3);
        public DateTime ReturnDateD { get; set; }
    }

    public class PurchaseReturnNewDetailedVM :PurchaseReturnNewVM
    {

        [Display(Name = "Currency")]
        public string Currency { get; set; }
        public int CurrencyId { get; set; }

        [Display(Name = "Currency Rate")]
        public string CurrencyRate => string.Format(SystemFormat.StringDecimalComma, CurrencyRateD);
        public decimal CurrencyRateD { get; set; }


        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Canvas Summary Reference")]
        public string Reference { get; set; }

        [Display(Name = "Approved By")]
        public string? ApprovedBy { get; set; }

        [Display(Name = "Disapproved By")]
        public string? DisapprovedBy { get; set; }

        [Display(Name = "Disapproved Reason")]
        public string? DisapprovedReason { get; set; }
        public int? ResubmitId { get; set; }
        public int? ResubmitFromId { get; set; }

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

        public IList<PurchaseReturnNewSaveVM> LineItems { get; set; }
    }

    public class PurchaseReturnNewSaveVM
    {
        public int ReceivingId { get; set; }
        public DateTime DateReturn { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int? ResubmitId { get; set; }
        public PurchaseReturnNewLineItemVM Items { get; set; }
    }

    public class PurchaseReturnNewLineItemVM
    {
        public int Id { get; set; }
        public bool ToReturn { get; set; }
        public int PurchaseReturnId { get; set; }

        [Display(Name = "Line Number")]
        public int LineNumber { get; set; }

        public int ReceivingItemId { get; set; }

        public int? ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string? ItemCode { get; set; }

        [Display(Name = "Part Number")]
        public string? ItemPartNumber { get; set; }

        [Display(Name = "Item Name")]
        public string ItemDescription { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format("{0:#,##0}", QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Received Quantity")]
        public string ReceivedQuantity => string.Format("{0:#,##0}", ReceivedQuantityD);
        public int ReceivedQuantityD { get; set; }

        public string ServedQuantity => string.Format("{0:#,##0}", ServedQuantityD);
        public int ServedQuantityD { get; set; }

        [Display(Name = "To Return Quantity")]
        public string ToReturnQuantity => string.Format("{0:#,##0}", ToReturnQuantityD);
        public int ToReturnQuantityD => ReceivedQuantityD - ServedQuantityD;

        [Display(Name = "UOM")]
        public string UnitCode { get; set; }
        public int UnitId { get; set; }

        [Display(Name = "Unit Cost")]
        public string UnitCost => string.Format("{0:#,##0.00}", UnitCostD);
        public decimal UnitCostD { get; set; }

        [Display(Name = "Total Amount")]
        public string TotalAmount => string.Format("{0:#,##0.00}", TotalAmountD);
        public decimal TotalAmountD => QuantityD * UnitCostD;
    }
}
