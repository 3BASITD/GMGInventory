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

    public class AgentVM
    {

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        //[Required]
        [StringLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name  = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleName + " "+ LastName;
            }
        }

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

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated { get; set; }
        public DateTime DateCreatedD { get; set; }

        [Display(Name = "User Modified")]
        public string UserModified { get; set; }

        [Display(Name = "Date Modifed")]
        public string DateModified { get; set; }
        public DateTime? DateModifiedD { get; set; }


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

        [Display(Name = "Areas")]
        public AreaVM Areas { get; set; }


    }

}
