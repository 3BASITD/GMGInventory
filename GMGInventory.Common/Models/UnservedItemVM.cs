using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class UnservedItemVM
    {
        public int Id { get; set; }

        [Display(Name = "Item")]
        public string Item { get; set; }

        [Display(Name = "Order Date")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime OrderDateD { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SalesOrderNumber { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Unit Price")]
        public string UnitPrice => string.Format("{0:#,##0.00}", UnitPriceD);
        public decimal UnitPriceD { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Packed Quantity")]
        public int PackedQuantity { get; set; }

        [Display(Name = "Unserved Quantity")]
        public int UnservedQuantity => Quantity - PackedQuantity;

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format("{0:#,##0.00}", Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format("{0:#,##0.00}", Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Agent")]
        public string Agent { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }

    public class UnservedItemFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CategoryId { get; set; }
        public int? ItemTypeId { get; set; }
        public int? BrandId { get; set; }
    }
}
