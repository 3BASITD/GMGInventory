using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PackVM
    {
        public int Id { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }

        [Display(Name = "Pack Number")]
        public string PackNumber { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Packed By")]
        public string PackedBy { get; set; }

        [Display(Name = "Packed Date")]
        public string PackedDate => PackedDateD.ToString(SystemFormat.StringDate);
        public DateTime PackedDateD { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }

    public class PackDetailsVM
    {
        [Display(Name = "Pack Number")]
        public int Id { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Delivery Type")]
        public string DeliveryType { get; set; }

        [Display(Name = "Sales Order Date")]
        public string SalesOrderDate => SalesOrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime SalesOrderDateD { get; set; }

        [Display(Name = "Packed Date")]
        public string PackDate => PackDateD.ToString(SystemFormat.StringDateTime);
        public DateTime PackDateD { get; set; }

        [Display(Name = "Packed By")]
        public string PackBy { get; set; }
    }

    public class PackItemVM
    {
        public int Id { get; set; }

        [Display(Name = "Box Number")]
        public int BoxNumber { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Selling Price")]
        public string SellingPrice => string.Format(SystemFormat.StringDecimalComma, SellingPriceD);
        public decimal SellingPriceD { get; set; }

        [Display(Name = "Gross Price")]
        public string GrossPrice => string.Format(SystemFormat.StringDecimalComma, GrossPriceD);
        public decimal GrossPriceD => SellingPriceD * Quantity;

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Extended Price")]
        public string ExtendedPrice => string.Format(SystemFormat.StringDecimalComma, ExtendedPriceD);
        public decimal ExtendedPriceD => Quantity * (((100m - Discount1D - Discount2D) * SellingPriceD) / 100m);
    }

    public class PackFormVM
    {
        public int? Id { get; set; }
        public int SONumber { get; set; }
        public IList<PackFormItemVM> Items { get; set; }
    }

    public class PackFormItemVM
    {
        public int Item { get; set; }
        public int Quantity { get; set; }
        public int BoxNumber { get; set; }
    }
}
