using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class UnservedItemVM
    {
        public UnservedItemVM()
        {
            Item = string.Empty;
            OrderDate = string.Empty;
            SalesOrderNumber = string.Empty;
            Customer = string.Empty;
            UnitPrice = string.Empty;
            Quantity = string.Empty;
            PackedQuantity = string.Empty;
            UnservedQuantity = string.Empty;
            Discount1 = string.Empty;
            Discount2 = string.Empty;
            Agent = string.Empty;
            CreatedBy = string.Empty;
        }

        public int Id { get; set; }

        [Display(Name = "Item")]
        public string Item { get; set; }

        [Display(Name = "Order Date")]
        public string OrderDate { get; set; }
        public DateTime OrderDateDT { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SalesOrderNumber { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Unit Price")]
        public string UnitPrice { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity { get; set; }

        [Display(Name = "Packed Quantity")]
        public string PackedQuantity { get; set; }

        [Display(Name = "Unserved Quantity")]
        public string UnservedQuantity { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 { get; set; }

        [Display(Name = "Agent")]
        public string Agent { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }

    public class UnservedItemFilter
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public int? CategoryId { get; set; }
        public int? ItemTypeId { get; set; }
        public int? BrandId { get; set; }
    }
}
