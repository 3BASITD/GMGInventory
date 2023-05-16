using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class PackRepository : IPackRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PackRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
             , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Select2Model<JsonOptionsModel>> GetOptions(string search, int page)
        {
            var orders = from order in context.Packs
                         where order.Status.Code != "Posted"
                         && order.SalesOrder.QuantityOrdered != order.SalesOrder.QuantityInvoiced
                         && (string.IsNullOrWhiteSpace(search)
                         || (order.Id.ToString().Contains(search.ToLower().Trim())))
                         orderby order.Id ascending
                         select new JsonOptionsModel
                         {
                             Id = order.Id.ToString(),
                             Text = order.Id.ToString()
                         };
            var ordersFiltered = await orders.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonOptionsModel>
            {
                items = ordersFiltered,
                total_count = orders.Count()
            };
        }

        public async Task<PackDetailsVM?> GetDetailsAsync(int? Id)
        {
            var orderVM = (from order in context.Packs
                                                .Include(s => s.SalesOrder).ThenInclude(s => s.Customer)
                                                .Include(s => s.SalesOrder).ThenInclude(s => s.Forwarder)
                           join user in userManager.Users on order.UserCreated equals user.Id
                           where order.Id == Id
                           select new PackDetailsVM
                           {
                               CustomerCode = order.SalesOrder.Customer.Code,
                               CustomerName = order.SalesOrder.Customer.Name,
                               Forwarder = order.SalesOrder.Forwarder.Name,
                               Id = order.Id,
                               PackBy = user.Nickname,
                               PackDateD = order.DateCreated,
                               SalesOrderDateD = order.SalesOrder.DateCreated,
                               SONumber = order.SalesOrder.SalesOrderNumber
                           });
            return await orderVM.FirstOrDefaultAsync();
        }

        public async Task<PackDetailsVM?> GetDetailsBySOAsync(int? Id)
        {
            var orderVM = (from order in context.SalesOrders
                                                .Include(s => s.Customer)
                                                .Include(s => s.Forwarder)
                           where order.Id == Id
                           select new PackDetailsVM
                           {
                               CustomerCode = order.Customer.Code,
                               CustomerName = order.Customer.Name,
                               Forwarder = order.Forwarder.Name,
                               Id = order.Id,
                               SalesOrderDateD = order.DateCreated,
                               SONumber = order.SalesOrderNumber
                           });
            return await orderVM.FirstOrDefaultAsync();
        }

        private async Task<IList<PackVM>> GetListAsync(ListFilterCommonModel filter)
        {
            var packs = from pck in context.Packs
                                           .Include(s => s.Status)
                                           .Include(s => s.SalesOrder).ThenInclude(s => s.Customer)
                                           .Where(s => (filter.Status == null || s.StatusId == filter.Status)
                                                     && filter.DateFrom <= s.DateCreated.Date
                                                     && s.DateCreated.Date <= filter.DateTo)
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
                             ItemId = order.Item.Id,
                             ItemCode = order.Item.Code,
                             ItemName = order.Item.Name,
                             Quantity = order.Quantity
                         };
            return await itemVM.ToListAsync();
        }

        public async Task<IList<PackItemVM>> GetItemsWithPriceAsync(int packId)
        {
            var itemVM = from pack in context.PackLineItems
                         join order in context.SalesOrderLineItems on pack.Pack.SalesOrder.Id equals order.SalesOrder.Id
                         where pack.PackId == packId
                         && pack.Item.Id == order.Item.Id
                         select new PackItemVM
                         {
                             BoxNumber = pack.BoxNumber,
                             Discount1D = order.Discount1,
                             Discount2D = order.Discount2,
                             Id = pack.Id,
                             ItemCode = pack.Item.Code,
                             ItemId = order.ItemId,
                             ItemName = pack.Item.Name,
                             Quantity = pack.Quantity,
                             SellingPriceD = order.SellingPrice
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

        public async Task<int> SaveAsync(PackFormVM form)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var orderVM = await context.SalesOrders.Where(s => s.Id == form.SONumber).FirstOrDefaultAsync();
            if (orderVM != null)
            {
                orderVM.QuantityPacked = form.Items.Sum(s => s.Quantity);
                if(orderVM.QuantityOrdered <= orderVM.QuantityPacked)
                {
                    orderVM.StatusId = await context.Statuses
                                                    .Where(s => s.Name == "Fully Packed")
                                                    .Select(s => s.Id)
                                                    .FirstAsync();
                }
                await context.SaveChangesAsync(currentUserId);

                int statusId = await context.Statuses
                                            .Where(s => s.Name == "Packing Submitted")
                                            .Select(s => s.Id)
                                            .FirstAsync();
                Pack pack = new()
                {
                    SalesOrderId = orderVM.Id,
                    StatusId = statusId,
                    TotalQuantity = form.Items.Sum(s => s.Quantity)
                };

                await context.Packs.AddAsync(pack);
                await context.SaveChangesAsync(currentUserId);

                foreach (var item in form.Items)
                {
                    var soItem = await context.SalesOrderLineItems
                                              .Where(s => s.SalesOrderId == orderVM.Id
                                                       && s.ItemId == item.Item)
                                              .FirstOrDefaultAsync();
                    if (soItem != null)
                    {
                        soItem.PackedQuantity += item.Quantity;
                    }

                    PackLineItem packItem = new()
                    {
                        BoxNumber = item.BoxNumber,
                        ItemId = item.Item,
                        PackId = pack.Id,
                        Quantity = item.Quantity
                    };
                    await context.PackLineItems.AddAsync(packItem);
                }

                await context.SaveChangesAsync(currentUserId);
                return pack.Id;
            }
            return -1;
        }
    }
}