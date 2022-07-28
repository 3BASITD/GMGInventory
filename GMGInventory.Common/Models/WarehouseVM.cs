using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class WarehouseVM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        [Display(Name = "Address Line #1")]
        public string AddressLine1 { get; set; }

        [MaxLength(250)]
        [Display(Name = "Address Line #2")]
        public string? AddressLine2 { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
    }

    public class WarehouseFormVM : WarehouseVM
    {
    }

    public class WarehouseViewVM : WarehouseVM
    {
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated { get; set; }

        [Display(Name = "User Modified")]
        public string UserModified { get; set; }

        [Display(Name = "Date Modified")]
        public string DateModified { get; set; }
    }
}
