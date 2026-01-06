using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class CustomerBrandDiscount : BaseEntity
    {
        public CustomerBrandDiscount()
        {
            SalesReturnLineItems = new HashSet<SalesReturnLineItem>();
        }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("BrandId")]
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }
        
        public bool IsActive { get; set; }


        public ICollection<SalesReturnLineItem> SalesReturnLineItems { get; set; }
    }
}
