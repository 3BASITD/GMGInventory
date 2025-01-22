using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class SalesReturnRepository : ISalesReturnRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public SalesReturnRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<SalesReturnVM?> GetDetailsAsync(int? Id)
        {
            var returnVM = from ret in context.SalesReturns
                                              .Include(s => s.Customer)
                           join userc in userManager.Users on ret.UserCreated equals userc.Id
                           join userm in userManager.Users on ret.UserModified equals userm.Id into um
                           from userm in um.DefaultIfEmpty()
                           where ret.Id == Id
                           select new SalesReturnVM
                           {
                               CustomerId = ret.CustomerId,
                               CustomerCode = ret.Customer.Code,
                               CustomerName = ret.Customer.Name,
                               Id = ret.Id,
                               Remarks = ret.Remarks,
                               SRNumber = ret.SalesReturnNumber,
                               ReturnDateD = ret.ReturnDate,
                               UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                               DateCreatedD = ret.DateCreated,
                               UserModified = userm == null ? null : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname),
                               DateModifiedD = ret.DateModified == null ? null : ret.DateModified.Value
                           };
            return await returnVM.FirstOrDefaultAsync();
        }

        private async Task<IList<SalesReturnVM>> GetListAsync()
        {
            var returns = from ret in context.SalesReturns
                                             .Include(s => s.Customer)
                          join user in userManager.Users on ret.UserCreated equals user.Id
                          select new SalesReturnVM
                          {
                              CustomerId = ret.CustomerId,
                              CustomerCode = ret.Customer.Code,
                              CustomerName = ret.Customer.Name,
                              Id = ret.Id,
                              Remarks = string.IsNullOrWhiteSpace(ret.Remarks) ? "N/A" : ret.Remarks,
                              ReturnDateD = ret.ReturnDate,
                              SRNumber = ret.SalesReturnNumber
                          };

            return await returns.ToListAsync();
        }

        public async Task<ForDataTables<SalesReturnVM>> GetListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CustomerCode.ToLower().Contains(form.SearchValue)
                                    || x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.Id.ToString().Contains(form.SearchValue)
                                    || x.Remarks.ToLower().Contains(form.SearchValue)
                                    || x.ReturnDate.ToLower().Contains(form.SearchValue)
                                    || x.SRNumber.ToLower().Contains(form.SearchValue));
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
                        case "CustomerCode": data = data.OrderBy(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        case "Id": data = data.OrderBy(s => s.Id); break;
                        case "Remarks": data = data.OrderBy(s => s.Remarks); break;
                        case "ReturnDate": data = data.OrderBy(s => s.ReturnDateD); break;
                        case "SRNumber": data = data.OrderBy(s => s.SRNumber); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "CustomerCode": data = data.OrderByDescending(s => s.CustomerCode); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        case "Id": data = data.OrderByDescending(s => s.Id); break;
                        case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
                        case "ReturnDate": data = data.OrderByDescending(s => s.ReturnDateD); break;
                        case "SRNumber": data = data.OrderByDescending(s => s.SRNumber); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<SalesReturnVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<IList<SalesReturnItemVM>> GetItemsAsync(int salesReturnId)
        {
            var itemVM = from rturn in context.SalesReturnLineItems
                         where rturn.SalesReturnId == salesReturnId
                         select new SalesReturnItemVM
                         {
                             SellingPriceD = rturn.SellingPrice,
                             Discount1D = rturn.Discount1,
                             Discount2D = rturn.Discount2,
                             Id = rturn.Id,
                             IsDefective = rturn.IsDefective,
                             ItemCode = rturn.Item.Code,
                             ItemName = rturn.Item.Name,
                             Quantity = rturn.Quantity,
                             ReferenceNumber = rturn.InvoiceLineItemId == null ? "TBR" : rturn.InvoiceLineItem!.Invoice.ControlNumber,
                             Unit = rturn.Item.Unit.Code
                         };
            return await itemVM.ToListAsync();
        }

        public async Task<int> SaveAsync(SalesReturnFormVM form)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var lastSalesReturn = await context.SalesReturns
                                               .OrderByDescending(s => s.SalesReturnNumber)
                                               .Select(s => s.SalesReturnNumber)
                                               .FirstOrDefaultAsync() ?? "SR-0000000";
            var lastSRNumber = int.Parse(lastSalesReturn.Replace("SR-", ""));

            var salesReturn = new SalesReturn
            {
                CustomerId = form.Customer,
                Remarks = form.Remarks + "",
                ReturnDate = form.ReturnDate,
                SalesReturnNumber = "SR-" + string.Format("{0:0000000}", lastSRNumber + 1)
            };

            context.SalesReturns.Add(salesReturn);
            await context.SaveChangesAsync(currentUserId);

            foreach (var item in form.Items)
            {
                var salesReturnLineItem = new SalesReturnLineItem
                {
                    Discount1 = item.Discount1,
                    Discount2 = item.Discount2,
                    InvoiceLineItemId = item.Invoice,
                    IsDefective = item.IsDefective,
                    ItemId = item.Item,
                    Quantity = item.Quantity,
                    SalesReturnId = salesReturn.Id,
                    SellingPrice = item.SellingPrice
                };

                context.SalesReturnLineItems.Add(salesReturnLineItem);
            }
            await context.SaveChangesAsync(currentUserId);
            return salesReturn.Id;
        }

        public async Task<bool> IsNumberInUse(int Id, string Number)
        {
            return await context.SalesReturns.AnyAsync(s => s.Id != Id && s.SalesReturnNumber == Number);
        }

        public async Task<int> UpdateAsync(SalesReturnUpdateVM form)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var salesReturn = await context.SalesReturns.Where(s => s.Id == form.Id).FirstOrDefaultAsync();

            if (salesReturn == null)
            {
                return -1;
            }

            salesReturn.SalesReturnNumber = form.ReturnNumber;
            salesReturn.CustomerId = form.Customer;
            salesReturn.DateCreated = form.ReturnDate;
            salesReturn.Remarks = form.Remarks + "";

            context.SalesReturns.Update(salesReturn);
            await context.SaveChangesAsync(currentUserId);
            return salesReturn.Id;
        }

        public async Task<SalesReturnDetailPVM> PrintDetails(int returnId)
        {
            return new SalesReturnDetailPVM
            {
                Contents = await GetItemsAsync(returnId),
                Details = await GetDetailsAsync(returnId) ?? new SalesReturnVM()
            };
        }
    }
}