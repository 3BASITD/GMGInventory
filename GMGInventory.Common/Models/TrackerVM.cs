using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class TaskFilter
    {
        public string? Status { get; set; }
        public string? Priority { get;set; }
    }


    public class TaskVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string AssignedTo { get; set; }

        [Display(Name ="Created By")]
        public string CreatedBy { get; set; }
        public int ErrorCount { get; set; }
        public int CommentCount { get; set; }
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }
    }

    public class TaskDetailsVM : TaskVM
    {
        public string Description { get; set; }
    }
}
