using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class SupplierVM : GenericAuditVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Credit Limit")]
        public decimal CreditLimit { get; set; }
    }
}
