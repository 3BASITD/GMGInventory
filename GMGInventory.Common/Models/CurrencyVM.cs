using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class CurrencyVM : GenericAuditVM
    {
        public string Rate => string.Format(SystemFormat.StringDecimalComma, RateD);
        public decimal RateD { get; set; }
    }
}
