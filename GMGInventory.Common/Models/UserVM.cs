using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class UserInformationVM
    {
        public string? UserId { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [StringLength(50)]
        [MinLength(2)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [StringLength(50)]
        [MinLength(2)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(2)]
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }

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

        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "User Created")]
        public string? UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }

        [Display(Name = "User Modified")]
        public string? UserModified { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }

        public string[] Role { get; set; }
    }
    public class UserInformationForUpdateVM
    {
        public string? UserId { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(5)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [StringLength(50)]
        [MinLength(2)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [StringLength(50)]
        [MinLength(2)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(2)]
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }
        
        public string[] Role { get; set; }
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

    public class UserResetPasswordVM
    {
        public string Id { get; set; }

        public string Username { get; set; }

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
