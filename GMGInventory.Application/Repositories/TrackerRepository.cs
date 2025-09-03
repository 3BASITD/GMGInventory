using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class TrackerRepository : ITrackerRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IHostingEnvironment hostingEnvironment;

        public TrackerRepository(ApplicationDbContext context
                               , IHttpContextAccessor httpContextAccessor
                               , UserManager<User> userManager
                               , IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IList<TaskVM>> GetTasks(TaskFilter filter)
        {
            var taskVM = from task in context.TrackerTasks
                         let issueCount = task.Issues.Where(s => s.Status.ToLower() != "fixed").Count()
                         let commentCount = task.Comments.Count()
                         where (filter.Status == null || task.Status.ToLower() == filter.Status.ToLower() || (task.Status.ToLower() == "active issue" && issueCount > 0))
                         && (filter.Priority == null || task.Priority.ToLower() == filter.Priority.ToLower())
                         select new TaskVM
                         {
                             Id = task.Id,
                             Number = string.Format("#{0:0000#}", task.Number),
                             Module = task.Module,
                             Summary = task.Summary,
                             Status = task.Status,
                             Priority = task.Priority,
                             AssigneeId = task.AssigneeId,
                             Assignee = task.Assignee.FirstName + " " + task.Assignee.LastName,
                             Function = task.Function,
                             Behavior = task.Behavior,
                             AccessControl = task.AccessControl,
                             DueDateD = task.DueDate,
                             ModifyExpiration = task.DateCreated.AddDays(5) <= DateTime.Now ? null : $"{(int)(task.DateCreated.AddDays(5) - DateTime.Now).Days} day(s) {(int)(task.DateCreated.AddDays(5) - DateTime.Now).Hours} hour(s) {(task.DateCreated.AddDays(5) - DateTime.Now).Minutes} minute(s)",
                             IssueCount = issueCount,
                             CommentCount = commentCount
                         };
            return await taskVM.ToListAsync();
        }

        public async Task<ForDataTables<TaskVM, TaskListCountVM>> GetTasksDT(DataTablesRequestForm form, TaskFilter filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetTasks(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Number,
                    s.Module,
                    s.Summary,
                    s.Status,
                    s.Priority,
                    s.Assignee,
                    s.DueDate + "",
                    s.IssueCount.ToString(),
                    s.CommentCount.ToString()
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumn.ToLower().Contains("date") || form.SortColumn == "Module" || form.SortColumn == "Priority")
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "Module": data = data.OrderBy(s => s.Module).ThenBy(s => s.Summary); break;
                            case "Priority": data = data.OrderBy(s => s.Priority.ToLower() == "high" ? 3
                                                                    : s.Priority.ToLower() == "medium" ? 2
                                                                    : s.Priority.ToLower() == "low" ? 1
                                                                    : 0)
                                                        .ThenBy(s => s.Status.ToLower() == "open" ? 6
                                                                   : s.Status.ToLower() == "incomplete" ? 5
                                                                   : s.Status.ToLower() == "doing" ? 4
                                                                   : s.Status.ToLower() == "testing" ? 3
                                                                   : s.Status.ToLower() == "for production" ? 2
                                                                   : s.Status.ToLower() == "on production" ? 1
                                                                   : 0)
                                                        .ThenBy(s => s.Module + " " + s.Summary); break;
                            case "DueDate": data = data.OrderBy(s => s.Priority); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "Module": data = data.OrderByDescending(s => s.Module).ThenByDescending(s => s.Summary); break;
                            case "Priority": data = data.OrderByDescending(s => s.Priority.ToLower() == "high" ? 3
                                                                    : s.Priority.ToLower() == "medium" ? 2
                                                                    : s.Priority.ToLower() == "low" ? 1
                                                                    : 0)
                                                        .ThenByDescending(s => s.Status.ToLower() == "open" ? 6
                                                                   : s.Status.ToLower() == "incomplete" ? 5
                                                                   : s.Status.ToLower() == "doing" ? 4
                                                                   : s.Status.ToLower() == "testing" ? 3
                                                                   : s.Status.ToLower() == "for production" ? 2
                                                                   : s.Status.ToLower() == "on production" ? 1
                                                                   : 0)
                                                        .ThenByDescending(s => s.Module + " " + s.Summary); break;
                            case "DueDate": data = data.OrderByDescending(s => s.DueDateD); break;
                            default: break;
                        }
                    }
                }
                else
                {
                    data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
                }
            }

            var count = new TaskListCountVM
            {
                OpenCount = data.Count(s => s.Status.ToLower() == "open"),
                DoingCount = data.Count(s => s.Status.ToLower() == "doing"),
                TestingCount = data.Count(s => s.Status.ToLower() == "testing"),
                IncompleteCount = data.Count(s => s.Status.ToLower() == "incomplete"),
                ForProductionCount = data.Count(s => s.Status.ToLower() == "for production"),
                OnProductionCount = data.Count(s => s.Status.ToLower() == "on production"),
                LowCount = data.Count(s => s.Priority.ToLower() == "low"),
                MediumCount = data.Count(s => s.Priority.ToLower() == "medium"),
                HighCount = data.Count(s => s.Priority.ToLower() == "high")
            };

            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<TaskVM, TaskListCountVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList,
                AdditionalData = count
            };
        }

        public async Task<TaskDetailsVM?> GetTask(Guid Id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var tasks = from t in context.TrackerTasks
                        let namenum = t.Module + " " + t.Summary
                        let prioritynum = t.Priority.ToLower() == "high" ? 3
                                        : t.Priority.ToLower() == "medium" ? 2
                                        : t.Priority.ToLower() == "low" ? 1
                                        : 0
                        let statusnum = t.Status.ToLower() == "open" ? 6
                                      : t.Status.ToLower() == "incomplete" ? 5
                                      : t.Status.ToLower() == "doing" ? 4
                                      : t.Status.ToLower() == "testing" ? 3
                                      : t.Status.ToLower() == "for production" ? 2
                                      : t.Status.ToLower() == "on production" ? 1
                                      : 0
                        select new
                        {
                            Tasks = t,
                            NameOrder = namenum,
                            PriorityOrder = prioritynum,
                            StatusOrder = statusnum
                        };

          var details = from task in context.TrackerTasks
                          join userc in userManager.Users on task.UserCreated equals userc.Id
                          join userm in userManager.Users on task.UserModified equals userm.Id into m
                          from userm in m.DefaultIfEmpty()
                          where task.Id == Id
                          select new TaskDetailsVM
                          {
                              Id = task.Id,
                              Module = task.Module,
                              Summary = task.Summary,
                              Function = task.Function,
                              Behavior = task.Behavior,
                              AccessControl = task.AccessControl,
                              Status = task.Status,
                              Priority = task.Priority,
                              AssigneeId = task.Assignee.Id,
                              Assignee = task.Assignee.FirstName + " " + task.Assignee.LastName,
                              DueDateD = task.DueDate,
                              ApprovedBy = task.ApprovedById == null ? null : task.ApprovedBy.FirstName + " " + task.ApprovedBy.LastName,
                              DateApprovedD = task.DateApproved,
                              DisapproveReason = task.DisapproveReason,
                              DisapprovedBy = task.DisapprovedById == null ? null : task.DisapprovedBy.FirstName + " " + task.DisapprovedBy.LastName,
                              DateDisapprovedD = task.DateDisapproved,
                              CreatedBy = userc.FirstName + " " + userc.LastName,
                              DateCreatedD = task.DateCreated,
                              ModifiedBy = userm == null ? null : userm.FirstName + " " + userm.LastName,
                              DateModifiedD = task.DateModified,
                              ModifyExpiration = task.DateCreated.AddDays(5) <= DateTime.Now ? null : $"{(int)(task.DateCreated.AddDays(5) - DateTime.Now).Days} day(s) {(int)(task.DateCreated.AddDays(5) - DateTime.Now).Hours} hour(s) {(task.DateCreated.AddDays(5) - DateTime.Now).Minutes} minute(s)",
                              IsAssignee = currentUserId == task.AssigneeId,
                              PreviousTaskId = (from t in tasks
                                                where t.Tasks.DateCreated < task.DateCreated
                                                orderby t.PriorityOrder descending
                                                      , t.StatusOrder descending
                                                      , t.NameOrder descending
                                                      , t.Tasks.DateCreated descending
                                                select t.Tasks.Id).FirstOrDefault() != Guid.Empty
                                             ? (from t in tasks
                                                where t.Tasks.DateCreated < task.DateCreated
                                                orderby t.PriorityOrder descending
                                                      , t.StatusOrder descending
                                                      , t.NameOrder descending
                                                      , t.Tasks.DateCreated descending
                                                select t.Tasks.Id).First()
                                             : (from t in tasks
                                                orderby t.PriorityOrder descending
                                                      , t.StatusOrder descending
                                                      , t.NameOrder descending
                                                      , t.Tasks.DateCreated descending
                                                select t.Tasks.Id).First(),
                              NextTaskId = (from t in tasks
                                            where t.Tasks.DateCreated > task.DateCreated
                                            orderby t.PriorityOrder ascending
                                                  , t.StatusOrder ascending
                                                  , t.NameOrder ascending
                                                  , t.Tasks.DateCreated ascending
                                            select t.Tasks.Id).FirstOrDefault() != Guid.Empty
                                         ? (from t in tasks
                                            where t.Tasks.DateCreated > task.DateCreated
                                            orderby t.PriorityOrder ascending
                                                  , t.StatusOrder ascending
                                                  , t.NameOrder ascending
                                                  , t.Tasks.DateCreated ascending
                                            select t.Tasks.Id).First()
                                         : (from t in tasks
                                            orderby t.PriorityOrder ascending
                                                  , t.StatusOrder ascending
                                                  , t.NameOrder ascending
                                                  , t.Tasks.DateCreated ascending
                                            select t.Tasks.Id).First(),
                              Histories = from history in task.Histories
                                          join userh in userManager.Users on history.UserCreated equals userh.Id
                                          orderby history.DateCreated descending
                                          select new TaskHistoryVM
                                          {
                                              Description = history.Description,
                                              CreatedBy = userh.FirstName + " " + userh.LastName,
                                              DateCreatedD = history.DateCreated
                                          },
                              CommentCount = task.Comments.Count(),
                              Comments = from comment in task.Comments
                                         join usercc in userManager.Users on comment.UserCreated equals usercc.Id
                                         join usercm in userManager.Users on comment.UserModified equals usercm.Id into m
                                         from usercm in m.DefaultIfEmpty()
                                         orderby comment.DateCreated descending
                                         select new TaskCommentVM
                                         {
                                             Id = comment.Id,
                                             Content = comment.Content,
                                             CreatedBy = usercc.FirstName + " " + usercc.LastName,
                                             DateCreatedD = comment.DateCreated,
                                             ModifiedBy = usercm == null ? null : usercm.FirstName + " " + usercm.LastName,
                                             DateModifiedD = comment.DateModified,
                                             ModifyExpiration = comment.DateCreated.AddHours(1) <= DateTime.Now ? null : $"{(int)(comment.DateCreated.AddHours(1) - DateTime.Now).TotalHours} hour(s) {(comment.DateCreated.AddHours(1) - DateTime.Now).Minutes} minute(s)"
                                         },
                              IssueCount = task.Issues.Where(s => s.Status.ToLower() != "fixed").Count(),
                              Issues = from issue in task.Issues
                                       join userif in userManager.Users on issue.FixedBy equals userif.Id into f
                                       from userif in f.DefaultIfEmpty()
                                       join useric in userManager.Users on issue.UserCreated equals useric.Id
                                       join userim in userManager.Users on issue.UserModified equals userim.Id into m
                                       from userim in m.DefaultIfEmpty()
                                       orderby issue.DateFixed == null descending, issue.DateFixed ascending, issue.DateCreated descending
                                       select new TaskIssueVM
                                       {
                                           Id = issue.Id,
                                           Summary = issue.Summary,
                                           Description = issue.Description,
                                           Replication = issue.Replication,
                                           Severity = issue.Severity,
                                           Status = issue.Status,
                                           Resolution = issue.Resolution,
                                           FixedBy = userif == null ? null : userif.FirstName + " " + userif.LastName,
                                           DateFixedD = issue.DateFixed,
                                           CreatedBy = useric.FirstName + " " + useric.LastName,
                                           DateCreatedD = issue.DateCreated,
                                           ModifiedBy = userim == null ? null : userim.FirstName + " " + userim.LastName,
                                           DateModifiedD = issue.DateModified,
                                           ModifyExpiration = issue.DateCreated.AddDays(1) <= DateTime.Now ? null : $"{(int)(issue.DateCreated.AddDays(1) - DateTime.Now).TotalHours} hour(s) {(issue.DateCreated.AddDays(1) - DateTime.Now).Minutes} minute(s)"
                                       }

                          };
            return await details.FirstOrDefaultAsync();
        }

        public async Task<TaskDetailsVM?> GetTaskDetails(Guid Id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var tasks = from t in context.TrackerTasks
                        let namenum = t.Module + " " + t.Summary
                        let prioritynum = t.Priority.ToLower() == "high" ? 3
                                        : t.Priority.ToLower() == "medium" ? 2
                                        : t.Priority.ToLower() == "low" ? 1
                                        : 0
                        let statusnum = t.Status.ToLower() == "open" ? 6
                                      : t.Status.ToLower() == "incomplete" ? 5
                                      : t.Status.ToLower() == "doing" ? 4
                                      : t.Status.ToLower() == "testing" ? 3
                                      : t.Status.ToLower() == "for production" ? 2
                                      : t.Status.ToLower() == "on production" ? 1
                                      : 0
                        select new
                        {
                            Tasks = t,
                            NameOrder = namenum,
                            PriorityOrder = prioritynum,
                            StatusOrder = statusnum
                        };

            var details = from task in context.TrackerTasks
                          join userc in userManager.Users on task.UserCreated equals userc.Id
                          join userm in userManager.Users on task.UserModified equals userm.Id into m
                          from userm in m.DefaultIfEmpty()
                          where task.Id == Id
                          select new TaskDetailsVM
                          {
                              Id = task.Id,
                              Module = task.Module,
                              Summary = task.Summary,
                              Function = task.Function,
                              Behavior = task.Behavior,
                              AccessControl = task.AccessControl,
                              Status = task.Status,
                              Priority = task.Priority,
                              AssigneeId = task.Assignee.Id,
                              Assignee = task.Assignee.FirstName + " " + task.Assignee.LastName,
                              DueDateD = task.DueDate,
                              ApprovedBy = task.ApprovedById == null ? null : task.ApprovedBy.FirstName + " " + task.ApprovedBy.LastName,
                              DateApprovedD = task.DateApproved,
                              DisapproveReason = task.DisapproveReason,
                              DisapprovedBy = task.DisapprovedById == null ? null : task.DisapprovedBy.FirstName + " " + task.DisapprovedBy.LastName,
                              DateDisapprovedD = task.DateDisapproved,
                              CreatedBy = userc.FirstName + " " + userc.LastName,
                              DateCreatedD = task.DateCreated,
                              ModifiedBy = userm == null ? null : userm.FirstName + " " + userm.LastName,
                              DateModifiedD = task.DateModified,
                              ModifyExpiration = task.DateCreated.AddDays(5) <= DateTime.Now ? null : $"{(int)(task.DateCreated.AddDays(5) - DateTime.Now).Days} day(s) {(int)(task.DateCreated.AddDays(5) - DateTime.Now).Hours} hour(s) {(task.DateCreated.AddDays(5) - DateTime.Now).Minutes} minute(s)",
                              IsAssignee = currentUserId == task.AssigneeId,
                              PreviousTaskId = (from t in tasks
                                                where t.Tasks.DateCreated < task.DateCreated
                                                orderby t.PriorityOrder descending
                                                      , t.StatusOrder descending
                                                      , t.NameOrder descending
                                                      , t.Tasks.DateCreated descending
                                                select t.Tasks.Id).FirstOrDefault() != Guid.Empty
                                             ? (from t in tasks
                                                where t.Tasks.DateCreated < task.DateCreated
                                                orderby t.PriorityOrder descending
                                                      , t.StatusOrder descending
                                                      , t.NameOrder descending
                                                      , t.Tasks.DateCreated descending
                                                select t.Tasks.Id).First()
                                             : (from t in tasks
                                                orderby t.PriorityOrder descending
                                                      , t.StatusOrder descending
                                                      , t.NameOrder descending
                                                      , t.Tasks.DateCreated descending
                                                select t.Tasks.Id).First(),
                              NextTaskId = (from t in tasks
                                            where t.Tasks.DateCreated > task.DateCreated
                                            orderby t.PriorityOrder ascending
                                                  , t.StatusOrder ascending
                                                  , t.NameOrder ascending
                                                  , t.Tasks.DateCreated ascending
                                            select t.Tasks.Id).FirstOrDefault() != Guid.Empty
                                         ? (from t in tasks
                                            where t.Tasks.DateCreated > task.DateCreated
                                            orderby t.PriorityOrder ascending
                                                  , t.StatusOrder ascending
                                                  , t.NameOrder ascending
                                                  , t.Tasks.DateCreated ascending
                                            select t.Tasks.Id).First()
                                         : (from t in tasks
                                            orderby t.PriorityOrder ascending
                                                  , t.StatusOrder ascending
                                                  , t.NameOrder ascending
                                                  , t.Tasks.DateCreated ascending
                                            select t.Tasks.Id).First(),
                          };
            return await details.FirstOrDefaultAsync();
        }

        public async Task<IList<TaskHistoryVM>> GetHistories(Guid TaskId)
        {
            var histories = from history in context.TrackerHistories
                            join user in userManager.Users on history.UserCreated equals user.Id
                            where history.TaskId == TaskId
                            orderby history.DateCreated descending
                            select new TaskHistoryVM
                            {
                                Description = history.Description,
                                CreatedBy = user.FirstName + " " + user.LastName,
                                DateCreatedD = history.DateCreated
                            };
            return await histories.ToListAsync();
        }

        public async Task<IList<TaskCommentVM>> GetComments(Guid TaskId)
        {
            var comments = from comment in context.TrackerComments
                           join userc in userManager.Users on comment.UserCreated equals userc.Id
                           join userm in userManager.Users on comment.UserModified equals userm.Id into m
                           from userm in m.DefaultIfEmpty()
                           where comment.TaskId == TaskId
                           orderby comment.DateCreated descending
                           select new TaskCommentVM
                           {
                               Id = comment.Id,
                               Content = comment.Content,
                               CreatedBy = userc.FirstName + " " + userc.LastName,
                               DateCreatedD = comment.DateCreated,
                               ModifiedBy = userm == null ? null : userm.FirstName + " " + userm.LastName,
                               DateModifiedD = comment.DateModified,
                               ModifyExpiration = comment.DateCreated.AddHours(1) <= DateTime.Now ? null : $"{(int)(comment.DateCreated.AddHours(1) - DateTime.Now).TotalHours} hour(s) {(comment.DateCreated.AddHours(1) - DateTime.Now).Minutes} minute(s)"
                           };
            return await comments.ToListAsync();
        }

        public async Task<IList<TaskIssueVM>> GetIssues(Guid TaskId)
        {
            var issues = from issue in context.TrackerIssues
                         join userf in userManager.Users on issue.FixedBy equals userf.Id into f
                         from userf in f.DefaultIfEmpty()
                         join userc in userManager.Users on issue.UserCreated equals userc.Id
                         join userm in userManager.Users on issue.UserModified equals userm.Id into m
                         from userm in m.DefaultIfEmpty()
                         where issue.TaskId == TaskId
                         orderby issue.DateFixed == null descending, issue.DateFixed ascending, issue.DateCreated descending
                         select new TaskIssueVM
                         {
                             Id = issue.Id,
                             Summary = issue.Summary,
                             Description = issue.Description,
                             Replication = issue.Replication,
                             Severity = issue.Severity,
                             Status = issue.Status,
                             Resolution = issue.Resolution,
                             FixedBy = userf == null ? null : userf.FirstName + " " + userf.LastName,
                             DateFixedD = issue.DateFixed,
                             CreatedBy = userc.FirstName + " " + userc.LastName,
                             DateCreatedD = issue.DateCreated,
                             ModifiedBy = userm == null ? null : userm.FirstName + " " + userm.LastName,
                             DateModifiedD = issue.DateModified,
                             ModifyExpiration = issue.DateCreated.AddDays(1) <= DateTime.Now ? null : $"{(int)(issue.DateCreated.AddDays(1) - DateTime.Now).TotalHours} hour(s) {(issue.DateCreated.AddDays(1) - DateTime.Now).Minutes} minute(s)"
                         };
            return await issues.ToListAsync();
        }

        public async Task<ResponseModel> AddTask(TaskParameterVM task)
        {
            var response = new ResponseModel();
            try
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                var newTask = new Tracker_Task
                {
                    Module = task.Module,
                    Summary = task.Summary,
                    Function = task.Function,
                    Behavior = task.Behavior,
                    AccessControl = task.AccessControl,
                    Status = task.Status,
                    Priority = task.Priority,
                    AssigneeId = task.Assignee,
                    DueDate = task.DueDate
                };
                await context.TrackerTasks.AddAsync(newTask);
                await context.SaveChangesAsync(currentUserId);

                await AddHistoryAsync(newTask.Id, "Create", "Task", $"Created Task: \"{newTask.Summary}\"");

                response.Status = "Success";
                response.Message = "Successfully added new task";
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
            }

            return response;
        }

        public async Task<ResponseModel> UpdateTask(TaskParameterVM task)
        {
            var response = new ResponseModel();
            try
            {
                var oldTask = await context.TrackerTasks
                                           .Where(s => s.Id == task.Id)
                                           .FirstOrDefaultAsync();
                if (oldTask == null)
                {
                    response.Status = "Error";
                    response.Message = "Invalid task, failed to update";
                }
                else if (Equals(oldTask.Module, task.Module)
                      && Equals(oldTask.Summary, task.Summary)
                      && Equals(oldTask.Function, task.Function)
                      && Equals(oldTask.Behavior, task.Behavior)
                      && Equals(oldTask.AccessControl, task.AccessControl)
                      && Equals(oldTask.Status, task.Status)
                      && Equals(oldTask.Priority, task.Priority)
                      && Equals(oldTask.Assignee, task.Assignee)
                      && Equals(oldTask.DueDate, task.DueDate)
                      && Equals(oldTask.DisapproveReason, task.DisapproveReason))
                {
                    response.Status = "Error";
                    response.Message = "Nothing to update";
                }
                else
                {
                    var oldTaskBU = await context.TrackerTasks
                                                 .AsNoTracking()
                                                 .Where(s => s.Id == task.Id)
                                                 .FirstAsync();

                    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                    oldTask.Module = task.Module;
                    oldTask.Summary = task.Summary;
                    oldTask.Function = task.Function;
                    oldTask.Behavior = task.Behavior;
                    oldTask.AccessControl = task.AccessControl;
                    oldTask.Status = task.Status;
                    if (task.Status.ToLower() == "testing")
                    {
                        oldTask.ApprovedById = null;
                        oldTask.DateApproved = null;
                        oldTask.DisapproveReason = null;
                        oldTask.DisapprovedById = null;
                        oldTask.DateDisapproved = null;
                    }
                    else if (task.Status.ToLower() == "for production")
                    {
                        oldTask.ApprovedById = currentUserId;
                        oldTask.DateApproved = DateTime.Now;
                    }
                    else if (task.Status.ToLower() == "incomplete")
                    {
                        oldTask.DisapproveReason = task.DisapproveReason;
                        oldTask.DisapprovedById = currentUserId;
                        oldTask.DateDisapproved = DateTime.Now;
                    }
                    oldTask.Priority = task.Priority;
                    oldTask.AssigneeId = task.Assignee;
                    oldTask.DueDate = task.DueDate;

                    context.TrackerTasks.Update(oldTask);
                    await context.SaveChangesAsync(currentUserId);

                    var oldAssignee = await userManager.FindByIdAsync(oldTaskBU.AssigneeId);
                    var newAssignee = await userManager.FindByIdAsync(oldTask.AssigneeId);

                    var changes = new Dictionary<string, (object? oldVal, object? newVal)>
                    {
                        ["Summary"] = (oldTaskBU.Summary, oldTask.Summary),
                        ["Function"] = (oldTaskBU.Function, oldTask.Function),
                        ["Behavior"] = (oldTaskBU.Behavior, oldTask.Behavior),
                        ["Access Control"] = (oldTaskBU.AccessControl, oldTask.AccessControl),
                        ["Status"] = (oldTaskBU.Status, oldTask.Status),
                        ["Priority"] = (oldTaskBU.Priority, oldTask.Priority),
                        ["Assignee"] = (oldAssignee, newAssignee),
                        ["Due Date"] = (oldTaskBU.DueDate, oldTask.DueDate),
                        ["Disapprove Reason"] = (oldTaskBU.DisapproveReason, oldTask.DisapproveReason)
                    };

                    foreach (var change in changes)
                    {
                        if (!Equals(change.Value.oldVal, change.Value.newVal))
                        {
                            if (change.Key == "Function" || change.Key == "Behavior" || change.Key == "Disapprove Reason")
                            {
                                if (change.Value.newVal is string s && !string.IsNullOrWhiteSpace(s))
                                {
                                    await AddHistoryAsync(oldTask.Id, "Update", "Task", $"Updated Task: {change.Key} was modified");
                                }
                            }
                            else
                            {
                                if (change.Value.oldVal == null || (change.Value.oldVal is string s && string.IsNullOrWhiteSpace(s)))
                                    await AddHistoryAsync(oldTask.Id, "Update", "Task", $"Updated Task: {change.Key} new data \"{change.Value.newVal}\"");
                                else
                                    await AddHistoryAsync(oldTask.Id, "Update", "Task", $"Updated Task: {change.Key} from \"{change.Value.oldVal}\" to \"{change.Value.newVal}\"");
                            }
                        }
                    }

                    response.Status = "Success";
                    response.Message = "Successfully updated task";
                }
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
            }

            return response;
        }

        public async Task<ResponseModel> IncreaseTaskPriority(Guid Id)
        {
            var response = new ResponseModel();
            try
            {
                var oldTask = await context.TrackerTasks.Where(s => s.Id == Id).FirstOrDefaultAsync();

                if (oldTask == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid task, failed to update." };

                if (oldTask.Status.ToLower() != "open" && oldTask.Status.ToLower() != "doing" && oldTask.Status.ToLower() != "incomplete")
                    return new ResponseModel { Status = "Error", Message = "Priority can only be changed for tasks with status OPEN, DOING, or INCOMPLETE" };

                switch (oldTask.Priority.ToLower())
                {
                    case "high":
                        return new ResponseModel { Status = "Error", Message = "Task is already on highest priority" };

                    case "low":
                        oldTask.Priority = "MEDIUM";
                        break;

                    case "medium":
                        oldTask.Priority = "HIGH";
                        break;

                    default:
                        return new ResponseModel { Status = "Error", Message = "Invalid task priority" };
                }

                var currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                await context.SaveChangesAsync(currentUserId);
                await AddHistoryAsync(oldTask.Id, "Update", "Task", "Updated Task: Priority increased");

                return new ResponseModel { Status = "Success", Message = "Successfully increased priority to " + oldTask.Priority };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DecreaseTaskPriority(Guid Id)
        {
            var response = new ResponseModel();
            try
            {
                var oldTask = await context.TrackerTasks.Where(s => s.Id == Id).FirstOrDefaultAsync();

                if (oldTask == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid task, failed to update." };

                if (oldTask.Status.ToLower() != "open" && oldTask.Status.ToLower() != "doing" && oldTask.Status.ToLower() != "incomplete")
                    return new ResponseModel { Status = "Error", Message = "Priority can only be changed for tasks with status OPEN, DOING, or INCOMPLETE" };

                switch (oldTask.Priority.ToLower())
                {
                    case "high":
                        oldTask.Priority = "MEDIUM";
                        break;

                    case "low":
                        return new ResponseModel { Status = "Error", Message = "Task is already on lowest priority" };

                    case "medium":
                        oldTask.Priority = "LOW";
                        break;

                    default:
                        return new ResponseModel { Status = "Error", Message = "Invalid task priority" };
                }

                var currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                await context.SaveChangesAsync(currentUserId);
                await AddHistoryAsync(oldTask.Id, "Update", "Task", "Updated Task: Priority decreased");

                return new ResponseModel { Status = "Success", Message = "Successfully decreased priority to " + oldTask.Priority };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DeleteTask(Guid Id)
        {
            var response = new ResponseModel();
            try
            {
                var oldTask = await context.TrackerTasks.Where(s => s.Id == Id).FirstOrDefaultAsync();
                if (oldTask == null)
                {
                    response.Status = "Error";
                    response.Message = "Invalid task, failed to delete.";
                }
                else if (await context.TrackerComments.AnyAsync(s => s.Id == Id)
                      || await context.TrackerIssues.AnyAsync(s => s.Id == Id))
                {
                    response.Status = "Error";
                    response.Message = "Task already have comment(s), or issue(s). failed to delete.";
                }
                else
                {
                    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                    var historyTasks = await context.TrackerHistories.Where(s => s.TaskId == Id).ToListAsync();
                    if (historyTasks != null)
                        context.TrackerHistories.RemoveRange(historyTasks);

                    context.TrackerTasks.Remove(oldTask);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Successfully deleted task";
                }
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
            }

            return response;
        }

        public async Task<ResponseModel> AddComment(CommentParameterVM comment)
        {
            var response = new ResponseModel();
            try
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                var newComment = new Tracker_Comment
                {
                    TaskId = comment.TaskId,
                    Content = comment.Content
                };
                await context.TrackerComments.AddAsync(newComment);
                await context.SaveChangesAsync(currentUserId);

                await AddHistoryAsync(comment.TaskId, "Add", "Comment", "Added Comment");

                response.Status = "Success";
                response.Message = "Successfully added new comment";
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
            }
            return response;
        }

        public async Task<ResponseModel> UpdateComment(CommentParameterVM comment)
        {
            var response = new ResponseModel();
            try
            {
                var oldComment = await context.TrackerComments
                                              .Where(s => s.Id == comment.Id
                                                       && s.TaskId == comment.TaskId)
                                              .FirstOrDefaultAsync();
                if (oldComment == null)
                {
                    response.Status = "Error";
                    response.Message = "Invalid comment, failed to update";
                }
                else if (Equals(oldComment.Content, comment.Content))
                {
                    response.Status = "Error";
                    response.Message = "Nothing changed";
                }
                else
                {
                    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                    oldComment.Content = comment.Content;

                    context.TrackerComments.Update(oldComment);
                    await context.SaveChangesAsync(currentUserId);

                    response.Status = "Success";
                    response.Message = "Successfully updated task";

                    await AddHistoryAsync(oldComment.TaskId, "Update", "Comment", "Updated Comment: Content was modified");
                }
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
            }

            return response;
        }

        public async Task<ResponseModel> DeleteComment(CommentParameterVM comment)
        {
            var response = new ResponseModel();

            try
            {
                var oldComment = await context.TrackerComments
                                              .Where(s => s.Id == comment.Id
                                                       && s.TaskId == comment.TaskId)
                                              .FirstOrDefaultAsync();
                if (oldComment == null)
                {
                    response.Status = "Error";
                    response.Message = "Invalid comment, failed to delete.";
                }
                else
                {
                    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                    context.TrackerComments.Remove(oldComment);
                    await context.SaveChangesAsync(currentUserId);

                    await AddHistoryAsync(comment.TaskId, "Delete", "Comment", "Deleted Comment");

                    response.Status = "Success";
                    response.Message = "Successfully deleted comment";
                }
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
            }

            return response;
        }

        public async Task<ResponseModel> AddIssue(IssueParameterVM issue)
        {
            var response = new ResponseModel();

            try
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                var newIssue = new Tracker_Issue
                {
                    TaskId = issue.TaskId,
                    Summary = issue.Summary,
                    Description = issue.Description,
                    Replication = issue.Replication,
                    Severity = issue.Severity,
                    Status = issue.Status
                };
                await context.TrackerIssues.AddAsync(newIssue);
                await context.SaveChangesAsync(currentUserId);

                await AddHistoryAsync(issue.TaskId, "Add", "Issue", "Added Issue");

                response.Status = "Success";
                response.Message = "Successfully added new issue";
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
            }

            return response;
        }

        public async Task<ResponseModel> UpdateIssue(IssueParameterVM issue)
        {
            var response = new ResponseModel();

            try
            {
                var oldIssue = await context.TrackerIssues
                                            .Where(s => s.Id == issue.Id
                                                     && s.TaskId == issue.TaskId)
                                            .FirstOrDefaultAsync();
                if (oldIssue == null)
                {
                    response.Status = "Error";
                    response.Message = "Invalid issue, failed to update";
                }
                else if (issue.DateFixed != null && issue.DateFixed < oldIssue.DateCreated)
                {
                    response.Status = "Error";
                    response.Message = "Date Fixed cannot be earlier than Date Created";
                }
                else
                {
                    var oldIssueBU = await context.TrackerIssues
                                                  .AsNoTracking()
                                                  .Where(s => s.Id == issue.Id
                                                           && s.TaskId == issue.TaskId)
                                                  .FirstAsync();
                    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                    oldIssue.Description = issue.Description;
                    oldIssue.Replication = issue.Replication;
                    oldIssue.Severity = issue.Severity;
                    oldIssue.Status = issue.Status;
                    oldIssue.Resolution = issue.Resolution;
                    oldIssue.FixedBy = issue.Resolution != null && issue.DateFixed != null ? currentUserId : issue.FixedBy;
                    oldIssue.DateFixed = issue.DateFixed;
                    context.TrackerIssues.Update(oldIssue);
                    await context.SaveChangesAsync(currentUserId);

                    var changes = new Dictionary<string, (object? oldVal, object? newVal)>
                    {
                        ["Summary"] = (oldIssueBU.Summary, oldIssue.Summary),
                        ["Description"] = (oldIssueBU.Description, oldIssue.Description),
                        ["Status"] = (oldIssueBU.Status, oldIssue.Status),
                        ["Replication"] = (oldIssueBU.Replication, oldIssue.Replication),
                        ["Severity"] = (oldIssueBU.Severity, oldIssue.Severity),
                        ["Resolution"] = (oldIssueBU.Resolution, oldIssue.Resolution)
                    };

                    foreach (var change in changes)
                    {
                        if (!Equals(change.Value.oldVal, change.Value.newVal))
                        {
                            var longContent = new HashSet<string> { "Description", "Replication", "Resolution" };
                            if (longContent.Contains(change.Key))
                                await AddHistoryAsync(oldIssue.TaskId, "Update", "Issue", $"Updated Issue: {change.Key} was modified");
                            else
                            {
                                if (change.Value.oldVal == null || (change.Value.oldVal is string s && string.IsNullOrWhiteSpace(s)))
                                    await AddHistoryAsync(oldIssue.TaskId, "Update", "Issue", $"Updated Issue: {change.Key} new data \"{change.Value.newVal}\"");
                                else
                                    await AddHistoryAsync(oldIssue.TaskId, "Update", "Issue", $"Updated Issue: {change.Key} from \"{change.Value.oldVal}\" to \"{change.Value.newVal}\"");
                            }
                        }
                    }

                    response.Status = "Success";
                    response.Message = "Successfully updated issue";
                }
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
            }

            return response;
        }

        public async Task<ResponseModel> DeleteIssue(IssueParameterVM issue)
        {
            var response = new ResponseModel();

            try
            {
                var oldIssue = await context.TrackerIssues
                                            .Where(s => s.Id == issue.Id
                                                     && s.TaskId == issue.TaskId)
                                            .FirstOrDefaultAsync();
                if (oldIssue == null)
                {
                    response.Status = "Error";
                    response.Message = "Invalid issue, failed to delete.";
                }
                else
                {
                    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                    context.TrackerIssues.Remove(oldIssue);
                    await context.SaveChangesAsync(currentUserId);

                    await AddHistoryAsync(issue.TaskId, "Delete", "Issue", "Deleted Issue");

                    response.Status = "Success";
                    response.Message = "Successfully deleted issue";
                }
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
            }
            return response;
        }




        private async Task AddHistoryAsync(Guid TaskId, string Action, string Entity, string Description)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var history = new Tracker_TaskHistory
            {
                TaskId = TaskId,
                ActionType = Action,
                EntityName = Entity,
                Description = Description
            };

            await context.TrackerHistories.AddAsync(history);
            await context.SaveChangesAsync(currentUserId);
        }
        public async Task<ResponseModel<string>> UploadImage(IFormFile file)
        {
            var response = new ResponseModel<string>();

            try
            {
                if (file == null || file.Length == 0)
                {
                    response.Status = "Error";
                    response.Message = "Invalid file.";
                    return response;
                }

                const long maxSize = 3 * 1024 * 1024;
                if (file.Length > maxSize)
                {
                    response.Status = "Error";
                    response.Message = "Image size exceeds 3MB.";
                    return response;
                }

                using (var image = await SixLabors.ImageSharp.Image.LoadAsync(file.OpenReadStream()))
                {
                    if (image.Width > 2000 || image.Height > 2000)
                    {
                        response.Status = "Error";
                        response.Message = "Image dimensions exceed 2000x2000 pixels.";
                        return response;
                    }
                }

                var uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images", "DevTask");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                response.Status = "Success";
                response.Message = "Image uploaded successfully.";
                response.NewRow = uniqueFileName;
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = SystemFormat.GetInnermostExceptionMessage(ex);
            }

            return response;
        }

    }
}