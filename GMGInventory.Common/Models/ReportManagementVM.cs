using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class CheckRegisterRVM
    {
        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Check Date")]
        public string CheckDate => CheckDateD.ToString(SystemFormat.StringDate);
        public DateTime CheckDateD { get; set; }

        [Display(Name = "Bank Details")]
        public string BankDetails { get; set; }

        [Display(Name = "Check Number")]
        public string CheckNumber { get; set; }

        [Display(Name = "Check Amount")]
        public string CheckAmount => string.Format(SystemFormat.StringDecimalComma, CheckAmountD);
        public decimal CheckAmountD { get; set; }
    }

    public class CheckRegisterPVM
    {
        public IList<CheckRegisterRVM> Registers { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Customer { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class RegisterSummaryRVM
    {
        [Display(Name = "Check Date")]
        public string CheckDate => CheckDateD.ToString(SystemFormat.StringDate);
        public DateTime CheckDateD { get; set; }

        [Display(Name = "Number of Checks")]
        public string NumberOfChecks => string.Format(SystemFormat.StringIntegerComma, NumberOfChecksD);
        public int NumberOfChecksD { get; set; }

        [Display(Name = "Check Amount")]
        public string CheckAmount => string.Format(SystemFormat.StringDecimalComma, CheckAmountD);
        public decimal CheckAmountD { get; set; }
    }

    public class RegisterSummaryPVM
    {
        public IList<RegisterSummaryRVM> Registers { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string TotalChecks => string.Format(SystemFormat.StringIntegerComma, TotalChecksD);
        public decimal TotalChecksD { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }
}