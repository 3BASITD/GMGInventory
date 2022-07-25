using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace GMGInventory.Common.Models
{
    public class AgentVM 
    {

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Area")]
        public int AreaId { get; set; }
        public SelectList? Areas { get; set; }

    }

    public class AgentViewVM : GenericAuditVM
    {
        [Display(Name="Area")]
        public AreaVM Area { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


    }

}
