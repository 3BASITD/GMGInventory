using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PackVM
    {
        public int Id { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }

        [Display(Name = "Pack Number")]
        public string PackNumber { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Packed By")]
        public string PackedBy { get; set; }

        [Display(Name = "Packed Date")]
        public string PackedDate => PackedDateD.ToString(SystemFormat.StringDate);
        public DateTime PackedDateD { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }

    public class PackDetailsVM
    {

    }
}
