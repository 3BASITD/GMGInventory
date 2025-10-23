using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class UnservedItemVM
    {
        public int Id { get; set; }

        [Display(Name = "Item Code")]
        public string Item { get; set; }

        [Display(Name = "Old SKU")]
        public string? OldSKU { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Order Date")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDate);
        public DateTime OrderDateD { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SalesOrderNumber { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Unit Price")]
        public string UnitPrice => string.Format(SystemFormat.StringDecimalComma, UnitPriceD);
        public decimal UnitPriceD { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Packed Quantity")]
        public string PackedQuantity => string.Format(SystemFormat.StringIntegerComma, PackedQuantityD);
        public int PackedQuantityD { get; set; }

        [Display(Name = "Unserved Quantity")]
        public string UnservedQuantity => string.Format(SystemFormat.StringIntegerComma, UnservedQuantityD);
        public int UnservedQuantityD { get; set; }

        [Display(Name = "Quantity on Hand")]
        public string QuantityOnHand => string.Format(SystemFormat.StringIntegerComma, QuantityOnHandD);
        public int QuantityOnHandD { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D);
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
        public int? Region { get; set; }
    }
}
