using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class AreaVM 
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }

        public string RegionName { get; set; }
        public SelectList? Regions { get; set; }

        [Display(Name = "Commission Rate")]
        public string CommissionRate => string.Format(SystemFormat.StringDecimalComma, CommissionRateD);
        public decimal CommissionRateD { get; set; }



    }

    public class AreaViewVM : GenericAuditVM
    {
        [Display(Name = "Region")]
        public RegionVM Region { get; set; }

        public string RegionName { get; set; }

        [Display(Name = "Commission Rate")]
        public string CommissionRate => string.Format(SystemFormat.StringDecimalComma, CommissionRateD);
        public decimal CommissionRateD { get; set; }
    }
}
