using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class CreditTermVM : GenericAuditVM
    {
        public int DaysToPay { get; set; }
    }
}
