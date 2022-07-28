using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class ForwarderVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        [MinLength(5)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(20)]
        [MinLength(5)]
        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

        [Required]
        [Display(Name = "Customer")]

        public int CustomerId { get; set; } 
        public SelectList? Customers { get; set; }

    }

        public class ForwarderViewVM : GenericAuditVM
    {

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

        [Display(Name = "Customer")]
        public CustomerVM Customer { get; set; }


    }


}
