using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [StringLength(12)]
        public string TIN { get; set; }

        [StringLength(150)]
        public string AddressLine1 { get; set; }

        [StringLength(150)]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        public string OfficeTelNo { get; set; }

        [StringLength(100)]
        public string ContactPerson { get; set; }

        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public SelectList? Regions { get; set; }

        [StringLength(100)]
        public string Terms { get; set; }

        [StringLength(100)]
        public string PriceList { get; set; }

        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public SelectList? Currencies { get; set; }

        [Display(Name = "BusinessStyle")]
        public int BusinessStyleId { get; set; }
        public string BusinessStyleName { get; set; }
        public SelectList? BusinessStyles { get; set; }

        [StringLength(12)]
        public string VAT { get; set; }
        public bool IsActive { get; set; }


        //[Required]
        //[Display(Name = "Area")]
        //public int AreaId { get; set; }
        //public string AreaName { get; set; }
        //public SelectList? Areas { get; set; }

        //[Required]
        //[Display(Name = "Region")]
        //public int RegionId { get; set; }
        //public SelectList? Regions { get; set; }

        //[Required]
        //[Display(Name = "Currency")]
        //public int CurrencyId { get; set; }
        //public string CurrencyName { get; set; }
        //public SelectList? Currencies { get; set; }

        //[Required]
        //[Display(Name = "Credit Limit")]
        //public string CreditLimit => string.Format(SystemFormat.StringIntegerComma, CreditLimitD);
        //public decimal CreditLimitD { get; set; }

        //[Required]
        //[MaxLength(250)]
        //[Display(Name = "Address Line 1")]
        //public string AddressLine1 { get; set; }

        //[Required]
        //[MaxLength(250)]
        //[Display(Name = "Address Line 2")]
        //public string AddressLine2 { get; set; }

        //[Required]
        //[MaxLength(250)]
        //[Display(Name = "City/ Municipality")]
        //public string City { get; set; }

        //[MaxLength(250)]
        //[Display(Name = "Barangay")]
        //public string? Barangay { get; set; }

        //[MaxLength(5)]
        //[Display(Name = "Zip Code")]
        //public string? ZipCode { get; set; }

        //[MaxLength(50)]
        //[Display(Name = "Contact Person")]
        //public string? ContactPerson { get; set; }

        //[MaxLength(50)]
        //[Display(Name = "Contact Number")]
        //public string? ContactNumber { get; set; }

        //[MaxLength(50)]
        //[Display(Name = "Email Address")]
        //public string? EmailAddress { get; set; }

        //public bool IsActive { get; set; }
        //public bool IsForeign { get; set; }
        //public bool IsDeleted { get; set; }
    }

    public class SupplierViewVM : GenericAuditVM
    {
        [StringLength(12)]
        public string TIN { get; set; }

        [StringLength(150)]
        public string AddressLine1 { get; set; }

        [StringLength(150)]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        public string OfficeTelNo { get; set; }

        [StringLength(100)]
        public string ContactPerson { get; set; }

        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public RegionVM Region { get; set; }

        [StringLength(100)]
        public string Terms { get; set; }

        [StringLength(100)]
        public string PriceList { get; set; }

        [Display(Name = "Currency")]
        public int  CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public CurrencyVM Currency { get; set; }


        [Display(Name = "BusinessStyle")]
        public int BusinessStyleId { get; set; }
        public string BusinessStyleName { get; set; }
        public BusinessStyleVM BusinessStyle { get; set; }

        [StringLength(12)]
        public string VAT { get; set; }

        //[Required]
        //[Display(Name = "Area")]
        //public int AreaId { get; set; }

        //public string AreaName { get; set; }
        //public AreaVM Area { get; set; }

        //[Required]
        //[Display(Name = "Region")]
        //public int RegionId { get; set; }

        //public RegionVM Region { get; set; }
        //public string RegionName { get; set; }

        //[Required]
        //[Display(Name = "Currency")]
        //public int CurrencyId { get; set; }
        //public string CurrencyName { get; set; }
        //public CurrencyVM Currency { get; set; }


        //[Display(Name = "Credit Limit")]
        //public string CreditLimit => string.Format(SystemFormat.StringIntegerComma, CreditLimitD);
        //public decimal CreditLimitD { get; set; }

        //[Display(Name = "Address Line 1")]
        //public string AddressLine1 { get; set; }

        //[Display(Name = "Address Line 2")]
        //public string AddressLine2 { get; set; }

        //[Display(Name = "Barangay")]
        //public string Barangay { get; set; }


        //[Display(Name = "City/ Municipality")]
        //public string City { get; set; }


        //[Display(Name = "Zip Code")]
        //public string ZipCode { get; set; }

        //[Display(Name = "Contact Person")]
        //public string ContactPerson { get; set; }

        //[Display(Name = "Contact Number")]
        //public string ContactNumber { get; set; }

        //[Display(Name = "Email Address")]
        //public string EmailAddress { get; set; }

        //public bool IsForeign { get; set; }
    }

}
