using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public AuditLogRepository(ApplicationDbContext context,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        private async Task<IList<AuditLogVM>> GetList()
        {
            var auditVM = from audit in context.AuditLogs
                                               .Include(s => s.User)
                          where audit.Type != "None"
                          select new AuditLogVM
                          {
                              Id = audit.Id,
                              User = audit.User.Nickname,
                              Type = audit.Type,
                              TableName = audit.TableName,
                              DateTimeD = audit.DateTime,
                              PrimaryKey = audit.PrimaryKey,
                              OldValues = audit.OldValues,
                              NewValues = audit.NewValues
                          };
            return await auditVM.ToListAsync();
        }

        public async Task<ForDataTables<AuditLogVM>> GetListDataTablesAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetList()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.User.ToLower().Contains(form.SearchValue)
                                    || x.Type.ToLower().Contains(form.SearchValue)
                                    || x.TableName.ToLower().Contains(form.SearchValue)
                                    || x.DateTime.ToLower().Contains(form.SearchValue)
                                    || x.PrimaryKey.ToString().ToLower().Contains(form.SearchValue)
                                    || x.OldValues.ToLower().Contains(form.SearchValue)
                                    || x.NewValues.ToLower().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "User": data = data.OrderBy(s => s.User); break;
                        case "Type": data = data.OrderBy(s => s.Type); break;
                        case "TableName": data = data.OrderBy(s => s.TableName); break;
                        case "DateTime": data = data.OrderBy(s => s.DateTimeD); break;
                        case "PrimaryKey": data = data.OrderBy(s => s.PrimaryKey); break;
                        case "OldValues": data = data.OrderBy(s => s.OldValues); break;
                        case "NewValues": data = data.OrderBy(s => s.NewValues); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "User": data = data.OrderByDescending(s => s.User); break;
                        case "Type": data = data.OrderByDescending(s => s.Type); break;
                        case "TableName": data = data.OrderByDescending(s => s.TableName); break;
                        case "DateTime": data = data.OrderByDescending(s => s.DateTimeD); break;
                        case "PrimaryKey": data = data.OrderByDescending(s => s.PrimaryKey); break;
                        case "OldValues": data = data.OrderByDescending(s => s.OldValues); break;
                        case "NewValues": data = data.OrderByDescending(s => s.NewValues); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<AuditLogVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}