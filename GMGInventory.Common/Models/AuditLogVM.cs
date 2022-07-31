using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class AuditLogVM
    {
        public AuditLogVM()
        {
            User = string.Empty;
            Type = string.Empty;
            TableName = string.Empty;
            DateTime = string.Empty;
            PrimaryKey = string.Empty;
            OldValues = string.Empty;
            NewValues = string.Empty;
        }

        public int Id { get; set; }

        [Display(Name = "User Nickname")]
        public string User { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Table Name")]
        public string TableName { get; set; }

        [Display(Name = "Date & Time")]
        public string DateTime { get; set; }

        [Display(Name = "Primary Key")]
        public string PrimaryKey { get; set; }

        [Display(Name = "Old Values")]
        public string OldValues { get; set; }

        [Display(Name = "New Values")]
        public string NewValues { get; set; }
    }
}
