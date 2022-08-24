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

        [Display(Name = "Purchase Order Number")]
        public string PONumber { get; set; }

        [Display(Name = "Purchase Date")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime OrderDateD { get; set; }

        [Display(Name = "Purchase By")]
        public string OrderBy { get; set; }

        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Display(Name = "Total Purchase Amount")]
        public string TotalAmount => string.Format("{0:#,##0.00}", TotalAmountD);
        public decimal TotalAmountD { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
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
        public string CurrencyRate => string.Format("{0:#,##0.00}", CurrencyRateD);
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
        public string UnitPrice => string.Format("{0:#,##0.00}", UnitPriceD);
        public decimal UnitPriceD { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format("{0:#,##0.00}", Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format("{0:#,##0.00}", Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Gross Price")]
        public string GrossPrice => string.Format("{0:#,##0.00}", GrossPriceD);
        //public decimal GrossPriceD => UnitPriceD - (Discount1D == 0 ? 0 : ((Discount1D / 100) * UnitPriceD)) - (Discount2D == 0 ? 0 : ((Discount2D / 100) * UnitPriceD));
        public decimal GrossPriceD => UnitPriceD - ((Discount1D / 100) * UnitPriceD) - ((Discount2D / 100) * UnitPriceD);
    }
}
