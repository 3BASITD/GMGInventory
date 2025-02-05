using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class CreditDebitMemoRVM
    {
        [Display(Name = "Memo Date")]
        public string MemoDate => MemoDateD.ToString(SystemFormat.StringDate);
        public DateTime MemoDateD { get; set; }

        [Display(Name = "Memo Number")]
        public string? MemoNumber { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
    }

    public class CreditDebitMemoPVM
    {
        public IList<CreditDebitMemoRVM> Memos { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Customer { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }
}