using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class PurchaseOrderReporitory : IPurchaseOrderRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PurchaseOrderReporitory(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<Select2Model<JsonPurchaseOrderOptionsModel>> GetOptions(string search, int page)
        {
            var orders = from order in context.PurchaseOrders
                                              .Include(s => s.Supplier)
                         join user in userManager.Users on order.UserCreated equals user.Id
                         where string.IsNullOrWhiteSpace(search)
                            || (order.PONumber.ToLower().Contains(search.ToLower()))
                         select new JsonPurchaseOrderOptionsModel
                         {
                             Id = order.Id.ToString(),
                             OrderBy = string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.FirstName) ? user.Nickname : (user.FirstName + " " + user.LastName),
                             OrderDateD = order.DateCreated,
                             Supplier = order.Supplier.Name,
                             Text = order.PONumber
                         };
            var ordersFiltered = await orders.Skip((page - 1) * 10).Take(10).ToListAsync();
            return new Select2Model<JsonPurchaseOrderOptionsModel>
            {
                //incomplete_results = false,
                items = ordersFiltered,
                total_count = orders.Count()
            };
        }

        public async Task<IList<JsonCurrencyOptionModel>> GetCurrencyOptions(int? IncludeId)
        {
            return await context.Currencies
                                .Select(s => new JsonCurrencyOptionModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = (s.Code == s.Name ? "" : "(" + s.Code + ") ") + s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id,
                                    Rate = s.Rate
                                })
                                .ToListAsync();
        }

        public async Task<PurchaseOrderDetailsVM?> GetDetailsAsync(int? Id)
        {
            var orderVM = (from order in context.PurchaseOrders
                                                .Include(s => s.Currency)
                                                .Include(s => s.Destination)
                                                .Include(s => s.Status)
                                                .Include(s => s.Supplier)
                           join user in userManager.Users on order.UserCreated equals user.Id
                           where order.Id == Id
                           select new PurchaseOrderDetailsVM
                           {
                               Address1 = order.Supplier.AddressLine1,
                               Address2 = order.Supplier.AddressLine2,
                               Currency = order.Currency.Code,
                               CurrencyRateD = order.CurrencyRate,
                               Destination = order.Destination.Code,
                               Id = order.Id,
                               OrderBy = user.Nickname,
                               OrderDateD = order.DateCreated,
                               PONumber = order.PONumber,
                               ReferenceNumber = order.ReferenceNumber,
                               Status = order.Status.Code,
                               Supplier = order.Supplier.Code,
                               TotalAmountD = order.TotalPurchaseAmount == null ? 0 : order.TotalPurchaseAmount.Value
                           });
            return await orderVM.FirstOrDefaultAsync();
        }

        private async Task<IList<PurchaseOrderVM>> GetListAsync()
        {
            var orderVM = (from order in context.PurchaseOrders
                                                .Include(s => s.Destination)
                                                .Include(s => s.Status)
                                                .Include(s => s.Supplier)
                           join user in userManager.Users on order.UserCreated equals user.Id
                           select new PurchaseOrderVM
                           {
                               Destination = order.Destination.Code,
                               Id = order.Id,
                               OrderBy = user.Nickname,
                               OrderDateD = order.DateCreated,
                               PONumber = order.PONumber,
                               Status = order.Status.Code,
                               Supplier = order.Supplier.Code,
                               TotalAmountD = order.TotalPurchaseAmount == null ? 0 : order.TotalPurchaseAmount.Value
                           });
            return await orderVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseOrderVM>> GetListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Destination.ToLower().Contains(form.SearchValue)
                                    || x.OrderBy.ToLower().Contains(form.SearchValue)
                                    || x.OrderDate.ToLower().Contains(form.SearchValue)
                                    || x.PONumber.ToLower().Contains(form.SearchValue)
                                    || x.Status.ToLower().Contains(form.SearchValue)
                                    || x.Supplier.ToLower().Contains(form.SearchValue)
                                    || x.TotalAmount.ToLower().Contains(form.SearchValue));
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
                        case "Destination": data = data.OrderBy(s => s.Destination); break;
                        case "OrderBy": data = data.OrderBy(s => s.OrderBy); break;
                        case "OrderDate": data = data.OrderBy(s => s.OrderDateD); break;
                        case "PONumber": data = data.OrderBy(s => s.PONumber); break;
                        case "Status": data = data.OrderBy(s => s.Status); break;
                        case "Supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Destination": data = data.OrderByDescending(s => s.Destination); break;
                        case "OrderBy": data = data.OrderByDescending(s => s.OrderBy); break;
                        case "OrderDate": data = data.OrderByDescending(s => s.OrderDateD); break;
                        case "PONumber": data = data.OrderByDescending(s => s.PONumber); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        case "Supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PurchaseOrderVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<PurchaseOrderItemVM>> GetItemsAsync(int purchaseOrderId)
        {
            var itemVM = from order in context.PurchaseOrderLineItems
                         //join rturn in context.PurchaseReturnLineItems on order.Id equals rturn.PurchaseOrderLineItemId into ritem
                         where order.PurchaseOrderId == purchaseOrderId
                         select new PurchaseOrderItemVM
                         {
                             Discount1D = order.Discount1,
                             Discount2D = order.Discount2,
                             Id = order.Id,
                             ItemCode = order.Item.Code,
                             ItemName = order.Item.Name,
                             Quantity = order.Quantity - ((int?)context.PurchaseReturnLineItems.Where(s => s.PurchaseOrderLineItemId == order.Id).Sum(s => s.Quantity) ?? 0),
                             Unit = order.Item.Unit.Code,
                             UnitPriceD = order.UnitPrice
                         };
            return await itemVM.ToListAsync();
        }

        public async Task<ForDataTables<PurchaseOrderItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int purchaseOrderId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemsAsync(purchaseOrderId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Discount1.ToLower().Contains(form.SearchValue)
                                    || x.Discount2.ToLower().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue)
                                    || x.UnitPrice.ToLower().Contains(form.SearchValue)
                                    || x.GrossPrice.ToLower().Contains(form.SearchValue));
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
                        case "Discount1": data = data.OrderBy(s => s.Discount1); break;
                        case "Discount2": data = data.OrderBy(s => s.Discount2); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "UnitPrice": data = data.OrderBy(s => s.UnitPrice); break;
                        case "GrossPrice": data = data.OrderBy(s => s.GrossPrice); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Discount1": data = data.OrderByDescending(s => s.Discount1); break;
                        case "Discount2": data = data.OrderByDescending(s => s.Discount2); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "UnitPrice": data = data.OrderByDescending(s => s.UnitPrice); break;
                        case "GrossPrice": data = data.OrderByDescending(s => s.GrossPrice); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<PurchaseOrderItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        //public async Task AddAsync(TransferFormVM formVM)
        //{
        //    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        //    var transferVM = new TransferItem
        //    {
        //        DestinationId = formVM.Destination,
        //        Remarks = formVM.Remarks,
        //        SourceId = formVM.Source
        //    };
        //    await context.TransferItems.AddAsync(transferVM);
        //    await context.SaveChangesAsync(currentUserId);
        //    var itemsVM = formVM.Items.Select(s => new TransferItemDetail
        //    {
        //        ItemId = s.Id,
        //        Quantity = s.Quantity,
        //        TransferItemId = transferVM.Id
        //    });
        //    await context.AddRangeAsync(itemsVM);
        //    await context.SaveChangesAsync(currentUserId);
        //}
    }
}