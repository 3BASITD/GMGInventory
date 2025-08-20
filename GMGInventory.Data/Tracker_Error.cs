using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Tracker_Error: BaseEntity
    {
        public Tracker_Error()
        {
            Attachments = new HashSet<Tracker_Attachment>();
        }

        [ForeignKey("TaskId")]
        public int TaskId { get; set; }
        public Tracker_Task Task { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Severity { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Status { get; set; }
        public DateTime? FixedAt { get; set; }

        public ICollection<Tracker_Attachment> Attachments { get; set; }
    }
}
