using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class UserInformationVM
    {
        [Required]
        public string Id { get; set; }

        [StringLength(50)]
        [MinLength(2)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [StringLength(50)]
        [MinLength(2)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }
    }

    public class UserPasswordVM
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50)]
        [MinLength(7)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50)]
        [MinLength(7)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50)]
        [MinLength(7)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "New password did not match")]
        public string ConfirmPassword { get; set; }
    }
}
