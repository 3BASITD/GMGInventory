using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class CustomerBrandDiscount : BaseEntity
    {
        public CustomerBrandDiscount()
        {
        }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("BrandId")]
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }

    }
}
