using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Tracker_Task : BaseGuidEntity
    {
        public Tracker_Task()
        {
            Comments = new HashSet<Tracker_Comment>();
            Issues = new HashSet<Tracker_Issue>();
            Histories = new HashSet<Tracker_TaskHistory>();
        }

        public int? Number { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Module { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Summary { get; set; }
        public string Function { get; set; }
        public string? Behavior { get; set; }
        public string AccessControl { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Status { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Priority { get; set; }

        [ForeignKey("AssigneeId")]
        public string AssigneeId { get; set; }
        public User Assignee { get; set; }
        public DateTime? DueDate { get; set; }

        [ForeignKey("ApprovedById")]
        public string? ApprovedById { get; set; }
        public User ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }

        [ForeignKey("DisapprovedById")]
        public string? DisapprovedById { get; set; }
        public User DisapprovedBy { get; set; }
        public DateTime? DateDisapproved { get; set; }
        public string? DisapproveReason { get; set; }
        public ICollection<Tracker_Comment> Comments { get; set; }
        public ICollection<Tracker_Issue> Issues { get; set; }
        public ICollection<Tracker_TaskHistory> Histories { get; set; }
    }
}
