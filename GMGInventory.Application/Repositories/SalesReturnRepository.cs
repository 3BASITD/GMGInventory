using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class SalesReturnRepository : ISalesReturnRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public SalesReturnRepository(ApplicationDbContext context
             , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<SalesReturnVM?> GetDetailsAsync(int? Id)
        {
            var returnVM = from ret in context.SalesReturns
                                              .Include(s => s.Customer)
                           join usr in userManager.Users on ret.UserCreated equals usr.Id
                           where ret.Id == Id
                           select new SalesReturnVM
                           {
                               CustomerCode = ret.Customer.Code,
                               CustomerName = ret.Customer.Name,
                               Id = ret.Id,
                               Remarks = ret.Remarks,
                               ReturnBy = usr.Nickname,
                               ReturnDateD = ret.DateCreated,
                               SRNumber = ret.Number
                           };
            return await returnVM.FirstOrDefaultAsync();
        }

        private async Task<IList<SalesReturnVM>> GetListAsync()
        {
            var returns = from ret in context.SalesReturns
                                             .Include(s => s.Customer)
                          join usr in userManager.Users on ret.UserCreated equals usr.Id
                          select new SalesReturnVM
                          {
                              CustomerCode = ret.Customer.Code,
                              CustomerName = ret.Customer.Name,
                              Id = ret.Id,
                              Remarks = ret.Remarks,
                              ReturnBy = usr.Nickname,
                              ReturnDateD = ret.DateCreated,
                              SRNumber = ret.Number
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
                                    || x.ReturnBy.ToLower().Contains(form.SearchValue)
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
                        case "ReturnBy": data = data.OrderBy(s => s.ReturnBy); break;
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
                        case "ReturnBy": data = data.OrderByDescending(s => s.ReturnBy); break;
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
                             SellingPriceD = rturn.Amount,
                             Discount1D = rturn.Discount1,
                             Discount2D = rturn.Discount2,
                             Id = rturn.Id,
                             IsDefective = rturn.IsDefective,
                             ItemCode = rturn.Item.Code,
                             ItemName = rturn.Item.Name,
                             Quantity = rturn.Quantity,
                             ReferenceNumber = rturn.ReferenceNumber,
                             Unit = rturn.Item.Unit.Code
                         };
            return await itemVM.ToListAsync();
        }
    }
}