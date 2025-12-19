using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PurchaseRequestFilter
    {
        public int? TypeId { get; set; }
        public int? PurposeId { get; set; }
        public int? RegionId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? StatusId { get; set; }
        public string? FiledBy { get; set; }
    }

    public class PurchaseRequestVM
    {
        public int Id { get; set; }

        [Display(Name = "Doc Number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }
        public int TypeId { get; set; }

        [Display(Name = "Purpose")]
        public string Purpose { get; set; }
        public int? PurposeId { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }
        public int? RegionId { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Requested")]
        public string RequestDate => RequestDateD.ToString(SystemFormat.StringDate);
        public string RequestDate3 => RequestDateD.ToString(SystemFormat.StringDate3);
        public DateTime RequestDateD { get; set; }
        public string? Remarks { get; set; }
    }

    public class PurchaseRequestDetailedVM : PurchaseRequestVM
    {

        [Display(Name = "Approved By")]
        public string? ApprovedBy1 { get; set; }

        //[Display(Name = "Approved By #2")]
        //public string? ApprovedBy2 { get; set; }

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

        public IList<PurchaseRequestLineItemVM> LineItems { get; set; }
    }

    public class PurchaseRequestSaveVM
    {
        public int TypeId { get; set; }
        public int? PurposeId { get; set; }
        public string? PurposeDefinition { get; set; }
        public DateTime DateRequested { get; set; }
        public string? Remarks { get; set; }
        public string Status { get; set; }
        public int? ResubmitId { get; set; }
        public PurchaseRequestLineItemVM Items { get; set; }
    }

    public class PurchaseRequestLineItemVM
    {
        public int Id { get; set; }
        public int PurchaseRequestId { get; set; }

        [Display(Name = "Line Number")]
        public int LineNumber { get; set; }
        public int? SalesOrderId { get; set; }

        [Display(Name = "SO Number")]
        public string? SalesOrderNumber { get; set; }

        [Display(Name = "PO Number")]
        public string? PONumber { get; set; }

        [Display(Name = "Customer")]
        public string? Customer { get; set; }
        public int? CustomerId { get; set; }
        public int? ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string? ItemCode { get; set; }

        [Display(Name = "Part Number")]
        public string ItemPartNumber { get; set; }

        [Display(Name = "Item Name")]
        public string ItemDescription { get; set; }

        [Display(Name = "Stock")]
        public string StockOnHand => string.Format("{0:#,##0}", StockOnHandD);
        public int StockOnHandD { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format("{0:#,##0}", QuantityD);
        public int QuantityD { get; set; }
        public int MaxQuantityD { get; set; }

        public int UnitId { get; set; }

        [Display(Name = "UOM")]
        public string UnitCode { get; set; }

        [Display(Name = "Purpose")]
        public string? Purpose { get; set; }

        [Display(Name = "Have Stock")]
        public bool? HaveStock { get; set; }
    }

    public class ItemFromSalesOrderVM
    {
        public int Id { get; set; }

        public int LineNumber { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }

        [Display(Name = "Stock on Hand")]
        public string InventoryCount => string.Format(SystemFormat.StringIntegerComma, InventoryCountD);
        public int InventoryCountD { get; set; }

        [Display(Name = "Ordered Quantity")]
        public string OrderedQuantity => string.Format(SystemFormat.StringIntegerComma, OrderedQuantityD);
        public int OrderedQuantityD { get; set; }

        [Display(Name = "Unserved Quantity")]
        public string RemainingQuantity => string.Format(SystemFormat.StringIntegerComma, RemainingQuantityD);
        public int RemainingQuantityD { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }
        public int UnitId { get; set; }

        public bool ToRequest { get; set; } = false;

        public int RequestQuantityD { get; set; } = 0;
    }
}
