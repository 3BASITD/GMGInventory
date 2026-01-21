using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class SalesReturnVM
    {
        public int Id { get; set; }

        [Display(Name = "Sales Return Number")]
        public string SRNumber { get; set; }

        [Display(Name = "Sales Order Number")]
        public string? SONumber { get; set; }
        public int? SOId { get; set; }

        public int? CustomerId { get; set; }

        [Display(Name = "Customer Code")]
        public string? CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string? CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        public string? CustomerAddress { get; set; }

        [Display(Name = "Return Date")]
        public string ReturnDate => ReturnDateD.ToString(SystemFormat.StringDate);
        public string ReturnDate3 => ReturnDateD.ToString(SystemFormat.StringDate3);
        public DateTime ReturnDateD { get; set; }

        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }

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
    }

    public class SalesReturnItemVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Defective")]
        public string ReasonOfReturn { get; set; }

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
        public decimal GrossPriceD => Math.Round(Quantity * SellingPriceD * (100 - Discount1D) / 100 * (100 - Discount2D) / 100, 2);

        [Display(Name = "Reference Number")]
        public string ReferenceNumber { get; set; }
    }

    public class SalesReturnDetailPVM
    {
        public IList<SalesReturnItemVM> Contents { get; set; }
        public SalesReturnVM Details { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringIntegerComma, TotalQuantityD);
        public int TotalQuantityD => Contents.Count() > 0 ? Contents.Sum(s => s.Quantity) : 0;
        public string TotalGrossPrice => string.Format(SystemFormat.StringDecimalComma, TotalGrossPriceD);
        public decimal TotalGrossPriceD => Contents.Count() > 0 ? Contents.Sum(s => s.GrossPriceD) : 0;

    }

    public class SalesReturnFormVM
    {
        public int? Id { get; set; }
        public string ReturnNumber { get; set; }
        public int? SalesOrder { get; set; }
        public int? Customer { get; set; }
        public string Remarks { get; set; }
        public DateTime ReturnDate { get; set; }
        public IList<SalesReturnFormItemVM> Items { get; set; }
    }

    public class SalesReturnFormItemVM
    {
        public int Item { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Discount1 { get; set; }
        public decimal Discount2 { get; set; }
        public int ReasonOfReturn { get; set; }
        public int? Invoice { get; set; }
        public int? ItemPromoId { get; set; }
    }
}
