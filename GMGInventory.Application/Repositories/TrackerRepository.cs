using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
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

        public TrackerRepository(ApplicationDbContext context
                                        , IHttpContextAccessor httpContextAccessor
                                        , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IList<TaskVM>> GetTasks(TaskFilter filter)
        {
            var taskVM = from task in context.TrackerTasks
                         join user in userManager.Users on task.UserCreated equals user.Id
                         where (filter.Status == null || task.Status.ToLower() == filter.Status.ToLower())
                         && (filter.Priority == null || task.Priority.ToLower() == filter.Priority.ToLower())
                         select new TaskVM
                         {
                             Id = task.Id,
                             Title = task.Title,
                             Status = task.Status,
                             Priority = task.Priority,
                             AssignedTo = task.AssignedTo.FirstName + " " + task.AssignedTo.LastName,
                             CreatedBy = user.FirstName + " " + user.LastName,
                             ErrorCount = task.Errors.Count,
                             CommentCount = task.Comments.Count,
                             DateCreatedD = task.DateCreated
                         };
            return await taskVM.ToListAsync();
        }

        public async Task<ForDataTables<TaskVM>> GetTasksDT(DataTablesRequestForm form, TaskFilter filter)
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
                    s.Title,
                    s.Status,
                    s.Priority,
                    s.AssignedTo,
                    s.CreatedBy,
                    s.ErrorCount.ToString(),
                    s.CommentCount.ToString(),
                    s.DateCreated
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumn.ToLower().Contains("date"))
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "DateCreated": data = data.OrderBy(s => s.DateCreatedD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "DateCreated": data = data.OrderByDescending(s => s.DateCreatedD); break;
                            default: break;
                        }
                    }
                }
                else
                {
                    data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<TaskVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}