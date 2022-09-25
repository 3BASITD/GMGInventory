using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class CustomerPurchaseVM
    {
        public int Id { get; set; }

        [Display(Name = "Invoice Date")]
        public string InvoiceDate => InvoiceDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoiceDateD { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Control Number")]
        public string ControlNumber { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Selling Price")]
        public string SellingPrice => string.Format(SystemFormat.StringDecimalComma, SellingPriceD);
        public decimal SellingPriceD { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Extended Price")]
        public string ExtendedPrice => string.Format(SystemFormat.StringDecimalComma, ExtendedPriceD);
        public decimal ExtendedPriceD => Math.Round((SellingPriceD * Quantity * (100m - Discount1D - Discount2D)) / 100m, 2);



    }

    public class CustomerPurchaseFilterVM : ListFilterCommonModel
    {
        public int? CustomerId { get; set; }
        public int? ItemId { get; set; }
        public int? CategoryId { get; set; }
        public int? ItemTypeId { get; set; }
        public int? BrandId { get; set; }
    }
}
