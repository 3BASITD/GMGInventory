using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class ReceptacleRepository : GenericRepository<Access>, IReceptacleRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public ReceptacleRepository(ApplicationDbContext context
                                  , IHttpContextAccessor httpContextAccessor
                                  , UserManager<User> userManager) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        private async Task<IList<ReceptacleVM>> GetListAsync(ReceptacleFilterVM filter)
        {
            var packs = from p in context.Packs
                        join userc in userManager.Users on p.UserCreated equals userc.Id
                        join userm in userManager.Users on p.UserModified equals userm.Id into m
                        from userm in m.DefaultIfEmpty()
                        where (filter.CustomerId == null
                            || filter.CustomerId == p.SalesOrder.Customer.Id)
                            && filter.DateFrom <= (p.DateModified == null ? p.DateCreated : p.DateModified.Value).Date
                            && (p.DateModified == null ? p.DateCreated : p.DateModified.Value).Date <= filter.DateTo
                        select new ReceptacleVM
                        {
                            SONumber = p.SalesOrder.SalesOrderNumber,
                            Id = p.Id,
                            Customer = p.SalesOrder.Customer.Name,
                            Destination = p.SalesOrder
                                           .Customer
                                           .ShippingAddresses
                                           .Where(s => s.IsDefault == true)
                                           .Select(s => s.Address)
                                           .FirstOrDefault(),
                            DeliveryDateD = p.SalesOrder.RequiredDeliveryDate
                        };
            return await packs.ToListAsync();
        }

        public async Task<ForDataTables<ReceptacleVM>> GetListDTAsync(DataTablesRequestForm form, ReceptacleFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.SONumber,
                    s.PackNumber,
                    s.Customer,
                    s.Destination,
                    s.DeliveryDate
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue)));
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
                            case "DeliveryDate": data = data.OrderBy(s => s.DeliveryDateD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "DeliveryDate": data = data.OrderByDescending(s => s.DeliveryDateD); break;
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
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<ReceptacleVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<ReceptacleDetailVM?> GetDetailsAsync(int Id)
        {
            var receptacleVM = from pack in context.Packs
                               let destination = pack.SalesOrder.Customer.ShippingAddresses.Where(s => s.IsDefault == true).FirstOrDefault()
                               where pack.Id == Id
                               select new ReceptacleDetailVM
                               {
                                   SONumber = pack.SalesOrder.SalesOrderNumber,
                                   Id = pack.Id,
                                   Customer = pack.SalesOrder.Customer.Name,
                                   Destination = destination == null ? null : destination.Address,
                                   DeliveryDateD = pack.SalesOrder.RequiredDeliveryDate
                               };
            return await receptacleVM.FirstOrDefaultAsync();
        }

        public async Task<ReceptacleDetailVM?> GetPrintDetailsAsync(int Id, int BoxNumber)
        {
            var receptacleVM = from pack in context.Packs
                               let destination = pack.SalesOrder.Customer.ShippingAddresses.Where(s => s.IsDefault == true).FirstOrDefault()
                               where pack.Id == Id
                               && pack.PackLineItems.Any(s => s.BoxNumber == BoxNumber)
                               select new ReceptacleDetailVM
                               {
                                   SONumber = pack.SalesOrder.SalesOrderNumber,
                                   Id = pack.Id,
                                   Customer = pack.SalesOrder.Customer.Name,
                                   Destination = destination == null ? null : destination.Address,
                                   DestinationId = destination == null ? null : destination.Id,
                                   DeliveryDateD = pack.SalesOrder.RequiredDeliveryDate,
                                   PackDateD = pack.DateCreated,
                                   PackagingType = pack.PackLineItems.Where(s => s.BoxNumber == BoxNumber).First().PackType.Name,
                                   BoxNumber = BoxNumber,
                                   TotalItemsD = pack.PackLineItems.Where(s => s.BoxNumber == BoxNumber).Sum(s => s.Quantity),
                                   TotalPackageD = pack.PackLineItems.Select(s => s.BoxNumber).Distinct().Count()
                               };
            return await receptacleVM.FirstOrDefaultAsync();
        }

        public async Task SaveReceptacleHistory(int PackId, int BoxNumber, int? ShippingAddressId, string CompanyAs)
        {
            var history = new ReceptaclePrintHistory
            {
                PackId = PackId,
                BoxNumber = BoxNumber,
                ShippingAddressId = ShippingAddressId,
                CompanyAs = CompanyAs
            };

            await context.ReceptaclePrintHistories.AddAsync(history);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<IList<ReceptaclePrintHistoryVM>> GetReceptaclePrintHistoriesAsync(int packId)
        {
            var histories = from history in context.ReceptaclePrintHistories
                            join userc in userManager.Users on history.UserCreated equals userc.Id
                            let address = context.ShippingAddresses.Where(s => s.Id == history.ShippingAddressId).FirstOrDefault()
                            where history.PackId == packId
                            select new ReceptaclePrintHistoryVM
                            {
                                Id = history.Id,
                                BoxNumber = history.BoxNumber,
                                ShippingAddress = address == null ? null : address.Address,
                                PrintDateD = history.DateCreated,
                                PrintBy = userc.FirstName + " " + userc.LastName,
                                CompanyAs = history.CompanyAs
                            };

            return await histories.ToListAsync();
        }

        public async Task<ForDataTables<ReceptaclePrintHistoryVM>> GetReceptaclePrintHistoriesDTAsync(DataTablesRequestForm form, int packId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetReceptaclePrintHistoriesAsync(packId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.BoxNumber + "",
                    s.ShippingAddress,
                    s.PrintDate,
                    s.PrintBy,
                    s.CompanyAs
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue)));
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
                            case "PrintDate": data = data.OrderBy(s => s.PrintDateD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "PrintDate": data = data.OrderByDescending(s => s.PrintDateD); break;
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
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<ReceptaclePrintHistoryVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}