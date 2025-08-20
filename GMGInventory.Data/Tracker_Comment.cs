using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Tracker_Comment: BaseEntity
    {
        public Tracker_Comment()
        {
            Attachments = new HashSet<Tracker_Attachment>();
        }

        [ForeignKey("TaskId")]
        public int TaskId { get; set; }
        public Tracker_Task Task { get; set; }

        public string Content { get; set; }
        public ICollection<Tracker_Attachment> Attachments { get; set; }
    }
}
