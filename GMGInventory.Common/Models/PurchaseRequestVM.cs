using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Common.Models
{
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

        [Display(Name = "Area")]
        public string Area { get; set; }
        public int? AreaId { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Requested")]
        public string RequestDate => RequestDateD.ToString(SystemFormat.StringDate);
        public string RequestDate3 => RequestDateD.ToString(SystemFormat.StringDate3);
        public DateTime RequestDateD { get; set; }
    }

    public class PurchaseRequestDetailedVM : PurchaseRequestVM
    {
        public string Remarks { get; set; }
        public string PreparedBy { get; set; }
        public string CheckedBy { get; set; }
        public string? ApprovedBy1 { get; set; }
        public string? ApprovedBy2 { get; set; }
        public string? DisapprovedBy { get; set; }
        public string? DisapprovedReason { get; set; }
        public int? ResubmitId { get; set; }
        public string UserCreated { get; set; }
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }
        public string? UserModified { get; set; }
        public string DateModified => DateModifiedD == null ? "-" : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }

        public IList<PurchaseRequestLineItemVM> LineItems { get; set; }
    }

    public class PurchaseRequestSaveVM
    {
        public int TypeId { get; set; }
        public int? PurposeId { get; set; }
        public string? PurposeDefinition { get; set; }
        public int? AreaId { get; set; }
        public DateTime DateRequested { get; set; }
        public string Remarks { get; set; }
        public string? PreparedBy { get; set; }
        public string? CheckedBy { get; set; }
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
        public int? ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string? ItemCode { get; set; }

        [Display(Name = "Part Number")]
        public string ItemPartNumber { get; set; }

        [Display(Name = "Item Description")]
        public string ItemDescription { get; set; }

        [Display(Name = "Stock on Hand")]
        public int StockOnHandD { get; set; }
        public string StockOnHand => string.Format("{0:#,##0}", StockOnHandD);

        [Display(Name = "Quantity")]
        public int QuantityD { get; set; }
        public string Quantity => string.Format("{0:#,##0}", QuantityD);
        public int UnitId { get; set; }

        [Display(Name = "Unit of Measurement")]
        public string UnitCode { get; set; }

        [Display(Name = "Purpose")]
        public string? Purpose { get; set; }
    }
}
