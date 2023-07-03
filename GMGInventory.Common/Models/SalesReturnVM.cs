using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class SalesReturnVM
    {
        public int Id { get; set; }

        [Display(Name = "Sales Return Number")]
        public string SRNumber { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Return Date")]
        public string ReturnDate => ReturnDateD.ToString(SystemFormat.StringDate);
        public DateTime ReturnDateD { get; set; }

        [Display(Name = "Return By")]
        public string ReturnBy { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }

    public class SalesReturnItemVM
    {
        public int Id { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Defective")]
        public bool IsDefective { get; set; }

        [Display(Name = "Quantity to Return")]
        public int Quantity { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Selling Price")]
        public string SellingPrice => string.Format(SystemFormat.StringDecimalComma, SellingPriceD);
        public decimal SellingPriceD { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Gross Price")]
        public string GrossPrice => string.Format(SystemFormat.StringDecimalComma, GrossPriceD);
        public decimal GrossPriceD => Quantity * (((100m - Discount1D - Discount2D) * SellingPriceD) / 100m);

        [Display(Name = "Reference Number")]
        public string ReferenceNumber { get; set; }
    }

    public class SalesReturnDetailPVM
    {
        public IList<SalesReturnItemVM> Contents { get; set; }
        public SalesReturnVM Details { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringIntegerComma, TotalQuantityD);
        public int TotalQuantityD => Contents.Sum(s => s.Quantity);
        public string TotalGrossPrice => string.Format(SystemFormat.StringDecimalComma, TotalGrossPriceD);
        public decimal TotalGrossPriceD => Contents.Sum(s => s.GrossPriceD);

    }

    public class SalesReturnFormVM
    {
        public int? Id { get; set; }
        public int Customer { get; set; }
        public string Remarks { get; set; }
        public IList<SalesReturnFormItemVM> Items { get; set; }
    }

    public class SalesReturnFormItemVM
    {
        public int Item { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Discount1 { get; set; }
        public decimal Discount2 { get; set; }
        public bool IsDefective { get; set; }
        public int? Invoice { get; set; }
    }
}
