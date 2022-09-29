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

        public async Task<PackDetailsVM?> GetDetailsAsync(int? Id)
        {
            var orderVM = (from order in context.Packs
                                                .Include(s => s.SalesOrder).ThenInclude(s => s.Customer)
                                                .Include(s=>s.SalesOrder).ThenInclude(s=>s.DeliveryType)
                           join user in userManager.Users on order.UserCreated equals user.Id
                           where order.Id == Id
                           select new PackDetailsVM
                           {
                               CustomerCode = order.SalesOrder.Customer.Code,
                               CustomerName = order.SalesOrder.Customer.Name,
                               DeliveryType = order.SalesOrder.DeliveryType.Name,
                               Id = order.Id,
                               PackBy = user.Nickname,
                               PackDateD = order.DateCreated,
                               SalesOrderDateD = order.SalesOrder.DateCreated,
                               SONumber = order.SalesOrder.SalesOrderNumber
                           });
            return await orderVM.FirstOrDefaultAsync();
        }

        private async Task<IList<PackVM>> GetListAsync(ListFilterCommonModel filter)
        {
            var packs = from pck in context.Packs
                                           .Include(s => s.Status)
                                           .Include(s => s.SalesOrder)
                                               .ThenInclude(s => s.Customer)
                                           .Where(s => (filter.StatusId == null || s.StatusId == filter.StatusId)
                                                     && filter.DateFrom <= s.DateCreated
                                                     && s.DateCreated <= filter.DateTo)
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

        public async Task<ForDataTables<PackVM>> GetListDTAsync(DataTablesRequestForm form, ListFilterCommonModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(filter)).AsQueryable();
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

        public async Task<IList<PackItemVM>> GetItemsAsync(int packId)
        {
            var itemVM = from order in context.PackLineItems
                                              .Include(s => s.Item)
                         where order.PackId == packId
                         select new PackItemVM
                         {
                             BoxNumber = order.BoxNumber,
                             Id = order.Id,
                             ItemCode = order.Item.Code,
                             ItemName = order.Item.Name,
                             Quantity = order.Quantity
                         };
            return await itemVM.ToListAsync();
        }

        public async Task<ForDataTables<PackItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int packId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemsAsync(packId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.BoxNumber.ToString().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue));
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
                        case "BoxNumber": data = data.OrderBy(s => s.BoxNumber); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "BoxNumber": data = data.OrderByDescending(s => s.BoxNumber); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PackItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}