using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PurchaseOrderFilter
    {
        public int? TypeId { get; set; }
        public int? ModeId { get; set; }
        public int? SupplierId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? StatusId { get; set; }
    }

    public class PurchaseOrderNewVM
    {
        public int Id { get; set; }

        [Display(Name = "Doc Number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }
        public int TypeId { get; set; }

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }
        public int SupplierId { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }
        public int TermId { get; set; }

        [Display(Name = "Delivery Mode")]
        public string DeliveryMode { get; set; }
        public int DeliveryModeId { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Ordered")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDate);
        public string OrderDate3 => OrderDateD.ToString(SystemFormat.StringDate3);
        public DateTime OrderDateD { get; set; }
    }

    public class PurchaseOrderNewDetailedVM : PurchaseOrderNewVM
    {
        public string SupplierContactPerson { get; set; }
        public string SupplierContactNumber { get; set; }
        public string SupplierAddress { get; set; }
        public string RequestType { get; set; }
        public int RequestTypeId { get; set; }

        [Display(Name = "Request Purpose")]
        public string? RequestPurpose { get; set; }
        public int? RequestPurposeId { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }
        public int CurrencyId { get; set; }

        [Display(Name = "Currency Rate")]
        public string CurrencyRate => string.Format(SystemFormat.StringDecimalComma, CurrencyRateD);
        public decimal CurrencyRateD { get; set; }

        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }
        public int DeliveryAddressId { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryContactPerson { get; set; }
        public string DeliveryContactNumber { get; set; }

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

        public string PurchaseRequestNumber { get; set; }
        public string DateReceived => DateReceivedD == null ? "N/A" : DateReceivedD.Value.ToString("MMMM dd, yyyy");
        public string TimeReceived => DateReceivedD == null ? "N/A" : DateReceivedD.Value.ToString("hh:mm tt");
        public DateTime? DateReceivedD { get; set; }

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

        public IList<PurchaseOrderLineItemVM> LineItems { get; set; }
    }

    public class PurchaseOrderSaveVM
    {
        public int TypeId { get; set; }
        public int SupplierId { get; set; }
        public int TermsId { get; set; }
        public int DeliveryModeId { get; set; }
        public int DeliveryAddressId { get; set; }
        public DateTime DateOrdered { get; set; }
        public string Reference { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int? ResubmitId { get; set; }
        public PurchaseOrderLineItemVM Items { get; set; }
    }

    public class PurchaseOrderLineItemVM
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }

        [Display(Name = "Line Number")]
        public int LineNumber { get; set; }
        public int? PurchaseRequestId { get; set; }

        [Display(Name = "Purchase Request Ref #")]
        public string? PurchaseRequestNumber { get; set; }
        public int RequestItemId { get; set; }
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

        [Display(Name = "Unordered Quantity")]
        public string RequestQuantity => string.Format("{0:#,##0}", RequestQuantityD);
        public int RequestQuantityD { get; set; }

        [Display(Name = "Unordered Quantity")]
        public string ServedQuantity => string.Format("{0:#,##0}", ServedQuantityD);
        public int ServedQuantityD { get; set; }

        [Display(Name = "Unordered Quantity")]
        public string UnorderedQuantity => string.Format("{0:#,##0}", UnorderedQuantityD);
        public int UnorderedQuantityD => RequestQuantityD - ServedQuantityD;

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
