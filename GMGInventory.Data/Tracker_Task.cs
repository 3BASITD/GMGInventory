using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Tracker_Task: BaseEntity
    {
        public Tracker_Task()
        {
            Attachments = new HashSet<Tracker_Attachment>();
            Comments = new HashSet<Tracker_Comment>();
            Errors = new HashSet<Tracker_Error>();
            Histories = new HashSet<Tracker_TaskHistory>();
        }


        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Status { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Priority { get; set; }

        [ForeignKey("AssignedToId")]
        public string AssignedToId { get; set; }
        public User AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }

        public ICollection<Tracker_Attachment> Attachments { get; set; }
        public ICollection<Tracker_Comment> Comments { get; set; }
        public ICollection<Tracker_Error> Errors { get; set; }
        public ICollection<Tracker_TaskHistory> Histories { get; set; }
    }
}
