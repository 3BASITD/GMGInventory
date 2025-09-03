using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;

namespace GMGInventory.Application.Contracts
{
    public interface ITrackerRepository
    {
        Task<IList<TaskVM>> GetTasks(TaskFilter filter);
        Task<ForDataTables<TaskVM, TaskListCountVM>> GetTasksDT(DataTablesRequestForm form, TaskFilter filter);
        Task<TaskDetailsVM?> GetTask(Guid Id);
        Task<TaskDetailsVM?> GetTaskDetails(Guid Id);
        Task<IList<TaskHistoryVM>> GetHistories(Guid TaskId);
        Task<IList<TaskCommentVM>> GetComments(Guid TaskId);
        Task<IList<TaskIssueVM>> GetIssues(Guid TaskId);
        Task<ResponseModel> AddTask(TaskParameterVM task);
        Task<ResponseModel> UpdateTask(TaskParameterVM task);
        Task<ResponseModel> IncreaseTaskPriority(Guid Id);
        Task<ResponseModel> DecreaseTaskPriority(Guid Id);
        Task<ResponseModel> DeleteTask(Guid Id);
        Task<ResponseModel> AddComment(CommentParameterVM comment);
        Task<ResponseModel> UpdateComment(CommentParameterVM comment);
        Task<ResponseModel> DeleteComment(CommentParameterVM comment);
        Task<ResponseModel> AddIssue(IssueParameterVM issue);
        Task<ResponseModel> UpdateIssue(IssueParameterVM issue);
        Task<ResponseModel> DeleteIssue(IssueParameterVM issue);

        Task<ResponseModel<string>> UploadImage(IFormFile file);
    }
}