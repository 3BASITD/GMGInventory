using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace GMGInventory.Common.Models
{

    public class AgentFilterVM
    {
        [Display(Name = "Region")]
        public int? RegionId { get; set; }
        public SelectList? Regions { get; set; }
    }

    public class AgentVM : GenericAuditVM
    {

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => FirstName + " " + MiddleName + " " + LastName;

        [Display(Name = "AreaId")]
        public int AreaId { get; set; }
        public SelectList? Areas { get; set; }

        [Display(Name = "Area")]
        public string Area { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Customer Contact Number")]
        public string CustomerContactNo { get; set; }


    }

    public class AgentViewVM : GenericAuditVM
    {
        [Display(Name="Area")]
        public string Area { get; set; }

        [Display(Name = "AreaId")]
        public int AreaId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }


    }

}
