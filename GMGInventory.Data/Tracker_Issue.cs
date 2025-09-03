using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Tracker_Issue: BaseGuidEntity
    {
        public Tracker_Issue()
        {
        }

        [ForeignKey("TaskId")]
        public Guid TaskId { get; set; }
        public Tracker_Task Task { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Replication { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Severity { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Status { get; set; }
        public string? Resolution { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string? FixedBy { get; set; }
        public DateTime? DateFixed { get; set; }
    }
}
