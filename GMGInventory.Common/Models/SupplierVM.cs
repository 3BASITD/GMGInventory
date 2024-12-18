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

        [Required]
        [Display(Name = "Term")]
        public int TermId { get; set; }
        public string Term { get; set; }

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
    }

    public class SupplierViewVM : GenericAuditVM
    {
        [StringLength(12)]
        public string TIN { get; set; }

        [StringLength(150)]
        [Display(Name = "Address Line 1")]

        public string AddressLine1 { get; set; }

        [StringLength(150)]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        [Display(Name = "Office Tel No")]
        public string OfficeTelNo { get; set; }

        [StringLength(100)]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        [Display(Name = "Region")]
        public string RegionName { get; set; }
        public RegionVM Region { get; set; }

        [StringLength(100)]
        public string Term { get; set; }


        [Display(Name = "Currency")]
        public int  CurrencyId { get; set; }

        [Display(Name = "Currency")]
        public string CurrencyName { get; set; }
        public CurrencyVM Currency { get; set; }

        [Display(Name = "BusinessStyle")]
        public int BusinessStyleId { get; set; }

        [Display(Name = "Business Style")]
        public string BusinessStyleName { get; set; }
        public BusinessStyleVM BusinessStyle { get; set; }

        [StringLength(12)]
        public string VAT { get; set; }
    }

    public class SupplierDetailPurchaseVM
    {
        public string PurchaseNumber { get; set; }
        public string Warehouse { get; set; }
        public string Item { get; set; }
        public int? ItemId { get; set; }
        public string ItemDescription { get; set; }
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }
        public string UnitCost => string.Format(SystemFormat.StringDecimalComma, UnitCostD);
        public decimal UnitCostD { get; set; }
        public string UnitPrice => string.Format(SystemFormat.StringDecimalComma, UnitPriceD);
        public decimal UnitPriceD { get; set; }
        public string Currency { get; set; }
        public string ConversionFactor => string.Format(SystemFormat.StringDecimalComma, ConversionFactorD);
        public decimal ConversionFactorD { get; set; }
        public string ConvertedUnitPrice => string.Format(SystemFormat.StringDecimalComma, ConvertedUnitPriceD);
        public decimal ConvertedUnitPriceD => Math.Round(UnitPriceD * ConversionFactorD, 2);
        public string ReplenishDate => ReplenishDateD.ToString(SystemFormat.StringDateTime);
        public DateTime ReplenishDateD { get; set; }
        public string ReplenishBy { get; set; }
        public string Status { get; set; }
    }


}
