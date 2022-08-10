using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class AuditLogVM
    {
        public int Id { get; set; }

        [Display(Name = "User Nickname")]
        public string User { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Table Name")]
        public string TableName { get; set; }

        [Display(Name = "Date & Time")]
        public string DateTime => DateTimeD.ToString(SystemFormat.StringDateTime);
        public DateTime DateTimeD { get; set; }

        [Display(Name = "Primary Key")]
        public string PrimaryKey { get; set; }

        [Display(Name = "Old Values")]
        public string OldValues { get; set; }

        [Display(Name = "New Values")]
        public string NewValues { get; set; }
    }
}
