using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class CustomerVM
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

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [MaxLength(50)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        public SelectList? Regions { get; set; }

        [Required]
        [Display(Name = "Customer Type")]
        public int CustomerTypeId { get; set; }
        public SelectList? CustomerTypes { get; set; }

        [Required]
        [Display(Name = "Credit Term")]
        public int CreditTermId { get; set; }
        public SelectList? CreditTerms { get; set; }

        [Required]
        [Display(Name = "Agent")]
        public int AgentId { get; set; }
        public SelectList? Agents { get; set; }

        [Required]
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        public SelectList? Areas { get; set; }

        [MaxLength(50)]
        [MinLength(3)]
        [Display(Name = "Contact Person")]
        public string? ContactPerson { get; set; }

        [MaxLength(11)]
        [MinLength(5)]
        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        [MaxLength(20)]
        [MinLength(3)]
        [Display(Name = "Fax Number")]
        public string? FaxNumber { get; set; }

        [MaxLength(20)]
        [MinLength(3)]
        [Display(Name = "TIN #")]
        public string? TIN { get; set; }

        [Required]
        [Display(Name = "Credit Limit")]
        public decimal CreditLimit { get; set; }

        [MaxLength(100)]
        [MinLength(3)]
        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }

        [Display(Name = "Outstanding Balance")]
        public decimal? OutstandingBalance { get; set; }
    }

    public class CustomerViewVM : GenericAuditVM
    {
        
        [Display(Name = "Region")]
        public RegionVM Region { get; set; }
        
        [Display(Name = "Customer Type")]
        public CustomerTypeVM CustomerType { get; set; }

        
        [Display(Name = "Credit Term")]
        public CreditTermVM CreditTerm { get; set; }


        [Display(Name = "Agent")]
        public AgentVM Agent { get; set; }

        
        [Display(Name = "Area")]
        public AreaVM Area { get; set; }

        [Display(Name = "Outstanding Balance")]
        public decimal? OutstandingBalance { get; set; }

        
        [Display(Name = "Credit Limit")]
        public decimal CreditLimit { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

   
        [Display(Name = "Address")]
        public string Address { get; set; }


        [Display(Name = "Email")]
        public string? Email { get; set; }

       
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

    
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

       
        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

       
        [Display(Name = "TIN #")]
        public string TIN { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }


    }


}
