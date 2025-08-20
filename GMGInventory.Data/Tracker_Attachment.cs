using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Tracker_Attachment: BaseEntity
    {
        public Tracker_Attachment()
        {
        }

        [ForeignKey("TaskId")]
        public int? TaskId { get; set; }
        public Tracker_Task Task { get; set; }

        [ForeignKey("CommentId")]
        public int? CommentId { get; set; }
        public Tracker_Comment Comment { get; set; }

        [ForeignKey("ErrorId")]
        public int? ErrorId { get; set; }
        public Tracker_Error Error { get; set; }


        [Column(TypeName = "nvarchar(255)")]
        public string FilePath { get; set; }
    }
}
