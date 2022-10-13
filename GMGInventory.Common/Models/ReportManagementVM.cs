using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class ReportManagementCheckRegisterRVM
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

    public class ReportManagementRegisterSummaryRVM
    {
        [Display(Name = "Check Date")]
        public string CheckDate => CheckDateD.ToString(SystemFormat.StringDate);
        public DateTime CheckDateD { get; set; }

        [Display(Name = "Number of Checks")]
        public int NumberOfChecks { get; set; }

        [Display(Name = "Check Amount")]
        public string CheckAmount => string.Format(SystemFormat.StringDecimalComma, CheckAmountD);
        public decimal CheckAmountD { get; set; }
    }
}