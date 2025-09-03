using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Tracker_TaskHistory
    {
        public Tracker_TaskHistory()
        {
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("TaskId")]
        public Guid TaskId { get; set; }
        public Tracker_Task Task { get; set; }


        [Column(TypeName = "nvarchar(20)")]
        public string ActionType { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string EntityName { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string UserCreated { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
