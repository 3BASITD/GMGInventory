using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Tracker_Comment: BaseGuidEntity
    {
        public Tracker_Comment()
        {
        }

        [ForeignKey("TaskId")]
        public Guid TaskId { get; set; }
        public Tracker_Task Task { get; set; }

        public string Content { get; set; }
    }
}
