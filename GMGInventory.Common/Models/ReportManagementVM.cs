using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class ReportManagementCheckRegister
    {
        public string Customer { get; set; }
        public string CheckDate => CheckDateD.ToString(SystemFormat.StringDate);
        public DateTime CheckDateD { get; set; }
        public string BankDetails { get; set; }
        public string CheckNumber { get; set; }
        public string CheckAmount => string.Format(SystemFormat.StringDecimalComma, CheckAmountD);
        public decimal CheckAmountD { get; set; }
    }

    public class ReportManagementRegisterSummary
    {
        public string CheckDate => CheckDateD.ToString(SystemFormat.StringDate);
        public DateTime CheckDateD { get; set; }
        public int NumberOfChecks { get; set; }
        public string CheckAmount => string.Format(SystemFormat.StringDecimalComma, CheckAmountD);
        public decimal CheckAmountD { get; set; }
    }
}