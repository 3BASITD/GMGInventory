using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PackVM
    {
        public int Id { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }
        public int SalesOrderId { get; set; }

        [Display(Name = "Pack Number")]
        public string PackNumber { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Packed By")]
        public string PackedBy { get; set; }

        [Display(Name = "Packed Date")]
        public string PackedDate => PackedDateD.ToString(SystemFormat.StringDate);
        public DateTime PackedDateD { get; set; }

        [Display(Name = "Customer Priority")]
        public string Priority => SystemFormat.CustomerPriority(PriorityD);
        public int PriorityD { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }

    public class PackDetailsVM
    {
        [Display(Name = "Pack Number")]
        public int Id { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }
        public int SalesOrderId { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }
        public int CustomerrId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Priority")]
        public string Priority => SystemFormat.CustomerPriority(PriorityD);
        public int PriorityD { get; set; }

        [Display(Name = "Branch")]
        public string? Branch { get; set; }

        [Display(Name = "Forwarder")]
        public string? Forwarder { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Sales Order Date")]
        public string SalesOrderDate => SalesOrderDateD.ToString(SystemFormat.StringDate);
        public DateTime SalesOrderDateD { get; set; }

        [Display(Name = "Packed Date")]
        public string PackDate => PackDateD.ToString(SystemFormat.StringDate);
        public DateTime PackDateD { get; set; }

        [Display(Name = "Packed By")]
        public string PackBy { get; set; }

        [Display(Name = "Agent")]
        public string Agent { get; set; }
    }

    public class PackItemVM
    {
        public int Id { get; set; }
        public int PackId { get; set; }

        [Display(Name = "Package Number")]
        public int BoxNumber { get; set; }

        [Display(Name = "Package Type")]
        public int PackType { get; set; }
        public string PackTypeName { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Part Number")]
        public string ItemPartNumber { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Selling Price")]
        public string SellingPrice => string.Format(SystemFormat.StringDecimalComma, SellingPriceD);
        public decimal SellingPriceD { get; set; }

        public string SellingPriceType { get; set; }

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
        public decimal ExtendedPriceD => Math.Round(Quantity * SellingPriceD * (100 - Discount1D) / 100 * (100 - Discount2D) / 100, 2);

        public int SalesOrderLineItemId { get; set; }
    }

    public class PackFormVM : PackFormAPIVM
    {
        public int? Id { get; set; }
        public string SavingAs { get; set; }
    }

    public class PackFormAPIVM
    {
        public int? SalesOrderId { get; set; }
        public string? SalesOrderNumber { get; set; }
        public IList<PackFormItemVM> Items { get; set; }
    }

    public class PackFormItemVM
    {
        public int? ItemId { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public int Quantity { get; set; }
        public int BoxNumber { get; set; }
        public int? PackTypeId { get; set; }
        public string? PackTypeName { get; set; }
    }

    public class PackDetailsPVM
    {
        public IList<PackItemVM> Contents { get; set; }
        public PackDetailsVM Details { get; set; }
    }
}
