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
        public string MemoNumber { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
    }
}