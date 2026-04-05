using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Brand : BaseEntity
    {
        public Brand()
        {
            this.Items = new HashSet<Item>();
            this.CustomerBrandDiscounts = new HashSet<CustomerBrandDiscount>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Item> Items { get; set; }
        public ICollection<CustomerBrandDiscount> CustomerBrandDiscounts { get; set; }
    }
}
