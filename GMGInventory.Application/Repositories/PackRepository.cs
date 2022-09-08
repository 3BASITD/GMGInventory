using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class PackRepository : IPackRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public PackRepository(ApplicationDbContext context
             , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        private async Task<IList<PackVM>> GetListAsync(int? statusId)
        {
            var packs = from pck in context.Packs
                                           .Include(s => s.Status)
                                           .Include(s => s.SalesOrder)
                                               .ThenInclude(s => s.Customer)
                                           .Where(s => statusId == null || s.StatusId == statusId)
                        join usr in userManager.Users on pck.UserCreated equals usr.Id
                        select new PackVM
                        {
                            Customer = pck.SalesOrder.Customer.Code,
                            Id = pck.Id,
                            PackedBy = usr.Nickname,
                            PackedDateD = pck.DateCreated,
                            PackNumber = pck.Id.ToString(),
                            SONumber = pck.SalesOrder.SalesOrderNumber,
                            Status = pck.Status.Code
                        };

            return await packs.ToListAsync();
        }

        public async Task<ForDataTables<PackVM>> GetListDataTablesAsync(DataTablesRequestForm form, int? statusId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(statusId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.Id.ToString().Contains(form.SearchValue)
                                    || x.PackedBy.ToLower().Contains(form.SearchValue)
                                    || x.PackedDateD.ToString().Contains(form.SearchValue)
                                    || x.PackNumber.ToLower().Contains(form.SearchValue)
                                    || x.SONumber.ToLower().Contains(form.SearchValue)
                                    || x.Status.ToLower().Contains(form.SearchValue));
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
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "Id": data = data.OrderBy(s => s.Id); break;
                        case "PackedBy": data = data.OrderBy(s => s.PackedBy); break;
                        case "PackedDate": data = data.OrderBy(s => s.PackedDateD); break;
                        case "PackNumber": data = data.OrderBy(s => s.PackNumber); break;
                        case "SONumber": data = data.OrderBy(s => s.SONumber); break;
                        case "Status": data = data.OrderBy(s => s.Status); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "Id": data = data.OrderByDescending(s => s.Id); break;
                        case "PackedBy": data = data.OrderByDescending(s => s.PackedBy); break;
                        case "PackedDate": data = data.OrderByDescending(s => s.PackedDateD); break;
                        case "PackNumber": data = data.OrderByDescending(s => s.PackNumber); break;
                        case "SONumber": data = data.OrderByDescending(s => s.SONumber); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<PackVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}