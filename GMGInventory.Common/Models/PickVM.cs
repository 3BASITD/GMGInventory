using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PickVM
    {
        public int Id { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SalesOrderNumber { get; set; }
        public int SalesOrderId { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Picked Date")]
        public string PickedDate => PickedDateD.ToString(SystemFormat.StringDate);
        public DateTime PickedDateD { get; set; }

        [Display(Name = "Sync Date")]
        public string SyncDate => SyncDateD.ToString(SystemFormat.StringDate);
        public DateTime SyncDateD { get; set; }
    }
}
