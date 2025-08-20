using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Tracker_TaskHistory: BaseEntity
    {
        public Tracker_TaskHistory()
        {
        }

        [ForeignKey("TaskId")]
        public int TaskId { get; set; }
        public Tracker_Task Task { get; set; }


        [Column(TypeName = "nvarchar(20)")]
        public string OldStatus { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string NewStatus { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string OldPriority { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string NewPriority { get; set; }
    }
}
