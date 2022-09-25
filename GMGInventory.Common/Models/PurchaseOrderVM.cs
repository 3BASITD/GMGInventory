using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PurchaseOrderVM
    {
        public int Id { get; set; }

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }

        [Display(Name = "PO Number")]
        public string PONumber { get; set; }

        [Display(Name = "Purchase Date")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDate);
        public DateTime OrderDateD { get; set; }

        [Display(Name = "Purchase By")]
        public string OrderBy { get; set; }

        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Display(Name = "Total Purchase Amount")]
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }
        public string DateModified => DateModifiedD == null ? "-" : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }
    }

    public class PurchaseOrderDetailsVM : PurchaseOrderVM
    {
        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Currency Rate")]
        public string CurrencyRate => string.Format(SystemFormat.StringDecimalComma, CurrencyRateD);
        public decimal CurrencyRateD { get; set; }

        [Display(Name = "Reference Number")]
        public string ReferenceNumber { get; set; }
    }

    public class PurchaseOrderItemVM
    {
        public int Id { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Unit Cost")]
        public string UnitPrice => string.Format(SystemFormat.StringDecimalComma, UnitPriceD);
        public decimal UnitPriceD { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Gross Price")]
        public string NetPrice => string.Format(SystemFormat.StringDecimalComma, NetPriceD);
        public decimal NetPriceD => Quantity * (((100m - Discount1D - Discount2D) * UnitPriceD) / 100m);
    }

    public class PurchaseOrderFormVM
    {
        public int? Id { get; set; }
        public int Supplier { get; set; }
        public int Destination { get; set; }
        public DateTime ReplenishDate { get; set; }
        public int Currency { get; set; }
        public decimal CurrencyRate { get; set; }
        public string ReferenceNumber { get; set; }
        public IList<PurchaseOrderFormItemVM> Items { get; set; }
    }

    public class PurchaseOrderFormItemVM
    {
        public int Item { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount1 { get; set; }
        public decimal Discount2 { get; set; }
    }
}
