using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Common.Models
{
    public class TaskFilter
    {
        public string? Status { get; set; }
        public string? Priority { get;set; }
    }


    public class TaskVM
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string Module { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string AssigneeId { get; set; }
        public string Assignee { get; set; }
        public string Function { get; set; }
        public string? Behavior { get; set; }
        public string AccessControl { get; set; }
        public string? DueDate => DueDateD == null ? null : DueDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DueDateD { get; set; }
        public string? ApprovedBy { get; set; }
        public string? DateApproved => DateApprovedD == null ? null : DateApprovedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateApprovedD { get; set; }
        public string? DisapproveReason { get; set; }
        public string? DisapprovedBy { get; set; }
        public string? DateDisapproved => DateDisapprovedD == null ? null : DateDisapprovedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateDisapprovedD { get; set; }
        public string? ModifyExpiration { get; set; }
        public int IssueCount { get; set; }
        public int CommentCount { get; set; }
    }

    public class TaskDetailsVM : TaskVM
    {
        public Guid PreviousTaskId { get; set; }
        public Guid NextTaskId { get; set; }
        public bool IsAssignee { get; set; }
        public string CreatedBy { get; set; }
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }
        public string? ModifiedBy { get; set; }
        public string? DateModified => DateModifiedD == null ? null : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }
        public IEnumerable<TaskHistoryVM> Histories { get; set; }
        public IEnumerable<TaskCommentVM> Comments { get; set; }
        public IEnumerable<TaskIssueVM> Issues { get; set; }
    }

    public class TaskHistoryVM
    {
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }
    }

    public class TaskCommentVM
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }
        public string? ModifiedBy { get; set; }
        public string? DateModified => DateModifiedD == null ? null : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }
        public string? ModifyExpiration { get; set; }
    }

    public class TaskIssueVM
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Replication { get; set; }
        public string Severity { get; set; }
        public string Status { get; set; }
        public string? Resolution { get; set; }
        public string? FixedBy { get; set; }
        public string? DateFixed => DateFixedD == null ? null : DateFixedD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateFixedD { get; set; }
        public string CreatedBy { get; set; }
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }
        public string? ModifiedBy { get; set; }
        public string? DateModified => DateModifiedD == null ? null : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }
        public string? ModifyExpiration { get; set; }
    }

    public class TaskParameterVM
    {
        public Guid? Id { get; set; }
        public string Module { get; set; }
        public string Summary { get; set; }
        public string Function { get; set; }
        public string? Behavior { get; set; }
        public string AccessControl { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Assignee { get; set; }
        public DateTime? DueDate { get; set; }
        public string? DisapproveReason { get; set; }
    }

    public class CommentParameterVM
    {
        public Guid? Id { get; set; }
        public Guid TaskId { get; set; }
        public string Content { get; set; }
    }

    public class IssueParameterVM
    {
        public Guid? Id { get; set; }
        public Guid TaskId { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Replication { get; set; }
        public string Severity { get; set; }
        public string Status { get; set; }
        public string? Resolution { get; set; }
        public string? FixedBy { get; set; }
        public DateTime? DateFixed { get; set; }
    }

    public class TaskListCountVM
    {
        public int OpenCount { get; set; }
        public int DoingCount { get; set; }
        public int TestingCount { get; set; }
        public int IncompleteCount { get; set; }
        public int ForProductionCount { get; set; }
        public int OnProductionCount { get; set; }
        public int LowCount { get; set; }
        public int MediumCount { get; set; }
        public int HighCount { get; set; }
    }
}
