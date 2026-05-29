using System.ComponentModel.DataAnnotations;
using GMGInventory.Common.Constants;

namespace GMGInventory.Common.Models
{
    public class ChequeVM : GenericAuditVM
    {
        [Required]
        [Display(Name = "Collect Date")]
        public string CollectDate => CollectDateD.ToString(SystemFormat.StringDate3);
        public DateTime CollectDateD { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Ref No")]
        public string       RefNo { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [StringLength(50)]
        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Check Date")]
        public string CheckDate => CheckDateD == null ? "-" : CheckDateD.Value.ToString(SystemFormat.StringDate3);
        public DateTime? CheckDateD { get; set; }

        [StringLength(10)]
        public string Check { get; set; }

        [StringLength(50)]
        public string? Branch { get; set; }

        [StringLength(10)]
        [Display(Name = "Check No")]
        public string? CheckNo { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
        public int? StatusId { get; set; }

        [Display(Name = "Status Code")]
        public string StatusCode { get; set; }

        [Display(Name = "Status Name")]
        public string StatusName { get; set; }

        public string ActionStatus { get; set; }
    }

    public class ChequePVM
    {
        public IList<ChequeVM> Cheques { get; set; }
        //public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        //public DateTime DateFromD { get; set; }
        //public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        //public DateTime DateToD { get; set; }
        //public string Customer { get; set; }
        //public string Status { get; set; }

    }
}
