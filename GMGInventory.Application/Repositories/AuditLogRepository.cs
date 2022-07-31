using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class AuditLogRepository :IAuditLogRepository
    {
        private readonly ApplicationDbContext context;

        public AuditLogRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ForDataTables<AuditLogVM> GetListDataTables(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = context.AuditLogs.Include(s=>s.User).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.User.Nickname.ToLower().Contains(form.SearchValue)
                                    || x.Type.ToLower().Contains(form.SearchValue)
                                    || x.TableName.ToLower().Contains(form.SearchValue)
                                    || x.DateTime.ToString(SystemFormat.StringDateTime).Contains(form.SearchValue)
                                    || x.AffectedColumns.ToString().ToLower().Contains(form.SearchValue)
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
                        case "User": data = data.OrderBy(s => s.User.Nickname); break;
                        case "Type": data = data.OrderBy(s => s.Type); break;
                        case "TableName": data = data.OrderBy(s => s.TableName); break;
                        case "DateTime": data = data.OrderBy(s => s.DateTime); break;
                        case "AffectedColumns": data = data.OrderBy(s => s.AffectedColumns); break;
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
                        case "User": data = data.OrderByDescending(s => s.User.Nickname); break;
                        case "Type": data = data.OrderByDescending(s => s.Type); break;
                        case "TableName": data = data.OrderByDescending(s => s.TableName); break;
                        case "DateTime": data = data.OrderByDescending(s => s.DateTime); break;
                        case "AffectedColumns": data = data.OrderByDescending(s => s.AffectedColumns); break;
                        case "PrimaryKey": data = data.OrderByDescending(s => s.PrimaryKey); break;
                        case "OldValues": data = data.OrderByDescending(s => s.OldValues); break;
                        case "NewValues": data = data.OrderByDescending(s => s.NewValues); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.Select(s => new AuditLogVM
            {
                Id = s.Id,
                User = s.User.Nickname,
                Type = s.Type,
                TableName = s.TableName,
                DateTime = s.DateTime.ToString(SystemFormat.StringDateTime),
                PrimaryKey = s.PrimaryKey,
                OldValues = s.OldValues,
                NewValues = s.NewValues
            }).ToList();
            return new ForDataTables<AuditLogVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}