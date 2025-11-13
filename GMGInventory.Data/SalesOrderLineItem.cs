using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesOrderLineItem : BaseEntity
    {
        public SalesOrderLineItem()
        {
        }

        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        public int LineNumber { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SellingPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount1 { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount2 { get; set; }

        [ForeignKey("CustomerBrandDiscountId")]
        public int? CustomerBrandDiscountId { get; set; }
        public CustomerBrandDiscount CustomerBrandDiscount { get; set; }

        public decimal? Discount3 { get; set; }

        public decimal? Discount4 { get; set; }

        public decimal? Discount5 { get; set; }

        [ForeignKey("ItemPromoId")]
        public int? ItemPromoId { get; set; }
        public ItemPromo ItemPromo { get; set; }


    }
}
