using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class SupplierVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Supplier Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        public SelectList? Areas { get; set; }

        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        public SelectList? Regions { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        public SelectList? Currencies { get; set; }

        [Required]
        [Display(Name = "Credit Limit")]
        public decimal CreditLimit { get; set; }

        [Required]
        [MaxLength(250)]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Required]
        [MaxLength(250)]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [MaxLength(250)]
        [Display(Name = "City/ Municipality")]
        public string City { get; set; }

        [MaxLength(250)]
        [Display(Name = "Barangay")]
        public string? Barangay { get; set; }

        [MaxLength(5)]
        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "Contact Person")]
        public string? ContactPerson { get; set; }

        [MaxLength(50)]
        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        [MaxLength(50)]
        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }

        public bool IsActive { get; set; }
        public bool IsForeign { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class SupplierViewVM : GenericAuditVM
    {
        [Display(Name = "Area")]
        public AreaVM Area { get; set; }

        [Display(Name = "Region")]
        public RegionVM Region { get; set; }

        [Display(Name = "Currency")]
        public CurrencyVM Currency { get; set; }


        [Display(Name = "Credit Limit")]
        public decimal CreditLimit { get; set; }


        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }


        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }


        [Display(Name = "Barangay")]
        public string Barangay { get; set; }


        [Display(Name = "City/ Municipality")]
        public string City { get; set; }


        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public bool IsForeign { get; set; }
    }

}
