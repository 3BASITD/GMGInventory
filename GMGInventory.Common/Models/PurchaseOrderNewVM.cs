using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PurchaseOrderNewVM
    {
        public int Id { get; set; }

        [Display(Name = "Doc Number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }

        [Display(Name = "Term")]
        public string Term { get; set; }

        [Display(Name = "Delivery Mode")]
        public string DeliveryMode { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Date Ordered")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime OrderDateD { get; set; }
    }

    public class PurchaseOrderNewDetailedVM : PurchaseOrderNewVM
    {

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

    public class PurchaseOrderSaveVM
    {
        public int TypeId { get; set; }
        public int SupplierId { get; set; }
        public int TermsId { get; set; }
        public int CurrencyId { get; set; }
        public decimal CurrencyRate { get; set; }
        public int DeliveryModeId { get; set; }
        public int DeliveryAddressId { get; set; }
        public DateTime DateOrdered { get; set; }
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
        public int? ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string? ItemCode { get; set; }

        [Display(Name = "Part Number")]
        public string ItemPartNumber { get; set; }

        [Display(Name = "Item Name")]
        public string ItemDescription { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format("{0:#,##0}", QuantityD);
        public int QuantityD { get; set; }
        public int UnitId { get; set; }

        [Display(Name = "UOM")]
        public string UnitCode { get; set; }

        [Display(Name = "Unit Cost")]
        public string UnitCost => string.Format("{0:#,##0.00}", UnitCostD);
        public decimal UnitCostD { get; set; }

        [Display(Name = "Total Amount")]
        public string TotalAmount => string.Format("{0:#,##0.00}", TotalAmountD);
        public decimal TotalAmountD => QuantityD * UnitCostD;
    }
}
