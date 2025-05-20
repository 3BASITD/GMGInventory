using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GMGInventory.Application.Repositories
{
    public class ItemTransferRepository : IItemTransferRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public ItemTransferRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IList<WarehouseTransferVM>> GetAllTransferAsync(ListFilterCommonModel filter)
        {
            var transfers = from transfer in context.TransferItems

                            join userc in userManager.Users on transfer.UserCreated equals userc.Id
                            join userm in userManager.Users on transfer.UserModified equals userm.Id into m
                            from um in m.DefaultIfEmpty()

                            where filter.DateFrom <= transfer.TransferDate.Date && transfer.TransferDate.Date <= filter.DateTo

                            select new WarehouseTransferVM
                            {
                                Destination = transfer.Destination.Name,
                                Id = transfer.Id,
                                ItemCount = transfer.TransferItemDetails.Count,
                                Remarks = transfer.Remarks,
                                Source = transfer.Source.Name,
                                CreatedBy = userc.FirstName + " " + userc.LastName,
                                TransferDateD = transfer.TransferDate
                            };
            return await transfers.ToListAsync();
        }


        public async Task<TransferVM?> GetTransferVMAsync(int? Id)
        {
            var transferVM = (from transfer in context.TransferItems
                                                      .Include(s => s.Source)
                                                      .Include(s => s.Destination)
                              join userc in userManager.Users on transfer.UserCreated equals userc.Id
                              join userm in userManager.Users on transfer.UserModified equals userm.Id into um
                              from userm in um.DefaultIfEmpty()
                              where transfer.Id == Id
                              select new TransferVM
                              {
                                  Destination = transfer.Destination.Name,
                                  Id = transfer.Id,
                                  Remarks = transfer.Remarks,
                                  Source = transfer.Source.Name,
                                  TransferDateD = transfer.TransferDate,
                                  CreatedBy = userc.FirstName + " " + userc.LastName,
                                  DateCreatedD = transfer.DateCreated,
                                  ModifiedBy = userm == null ? null : userm.FirstName + " " + userm.LastName,
                                  DateModifiedD = transfer.DateModified
                              });
            return await transferVM.FirstOrDefaultAsync();
        }

        private async Task<IList<WarehouseTransferVM>> GetWarehouseTransfersAsync(ListFilterCommonModel filter)
        {
            var transferVM = (from transfer in context.TransferItems
                                                      .Include(s => s.TransferItemDetails)
                                                      .Include(s => s.Source)
                                                      .Include(s => s.Destination)
                              join user in userManager.Users on transfer.UserCreated equals user.Id
                              where filter.DateFrom <= transfer.TransferDate.Date && transfer.TransferDate.Date <= filter.DateTo
                              select new WarehouseTransferVM
                              {
                                  Destination = transfer.Destination.Name,
                                  Id = transfer.Id,
                                  ItemCount = transfer.TransferItemDetails.Count,
                                  Remarks = transfer.Remarks,
                                  Source = transfer.Source.Name,
                                  CreatedBy = user.FirstName + " " + user.LastName,
                                  TransferDateD = transfer.TransferDate
                              });
            return await transferVM.ToListAsync();
        }

        public async Task<ForDataTables<WarehouseTransferVM>> GetWarehouseTransfersDTAsync(DataTablesRequestForm form, ListFilterCommonModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetWarehouseTransfersAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Id.ToString().Contains(form.SearchValue)
                                    || x.Source.ToLower().Contains(form.SearchValue)
                                    || x.Destination.ToLower().Contains(form.SearchValue)
                                    || x.ItemCount.ToString().Contains(form.SearchValue)
                                    || x.TransferDate.ToLower().Contains(form.SearchValue)
                                    || x.CreatedBy.ToLower().Contains(form.SearchValue)
                                    || x.Remarks.ToLower().Contains(form.SearchValue));
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
                        case "Id": data = data.OrderBy(s => s.Id); break;
                        case "Source": data = data.OrderBy(s => s.Source); break;
                        case "Destination": data = data.OrderBy(s => s.Destination); break;
                        case "ItemCount": data = data.OrderBy(s => s.ItemCount); break;
                        case "TransferDate": data = data.OrderBy(s => s.TransferDateD); break;
                        case "CreatedBy": data = data.OrderBy(s => s.CreatedBy); break;
                        case "Remarks": data = data.OrderBy(s => s.Remarks); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Id": data = data.OrderByDescending(s => s.Id); break;
                        case "Source": data = data.OrderByDescending(s => s.Source); break;
                        case "Destination": data = data.OrderByDescending(s => s.Destination); break;
                        case "ItemCount": data = data.OrderByDescending(s => s.ItemCount); break;
                        case "TransferDate": data = data.OrderByDescending(s => s.TransferDateD); break;
                        case "CreatedBy": data = data.OrderByDescending(s => s.CreatedBy); break;
                        case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<WarehouseTransferVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<WarehouseTransferVM>> GetWarehouseTransfersAsync(int WarehouseId, ListFilterCommonModel filter)
        {
            var transferVM = (from trdetail in context.TransferItemDetails
                                                      .Include(s => s.Item)
                              join transfer in context.TransferItems
                                                      .Include(s => s.Source) on trdetail.TransferItemId equals transfer.Id
                              join user in userManager.Users on transfer.UserCreated equals user.Id
                              where transfer.DestinationId == WarehouseId
                                 || transfer.SourceId == WarehouseId

                                 && filter.DateFrom <= transfer.TransferDate.Date && transfer.TransferDate.Date <= filter.DateTo
                              select new WarehouseTransferVM
                              {
                                  Destination = transfer.Destination.Name,
                                  Id = transfer.Id,
                                  ItemCode = trdetail.Item.Code,
                                  ItemName = trdetail.Item.Name,
                                  Quantity = trdetail.Quantity,
                                  Remarks = transfer.Remarks,
                                  Source = transfer.Source.Name,
                                  CreatedBy = user.FirstName + " " + user.LastName,
                                  TransferDateD = transfer.TransferDate
                              });
            return await transferVM.ToListAsync();
        }

        public async Task<ForDataTables<WarehouseTransferVM>> GetWarehouseTransfersDTAsync(DataTablesRequestForm form, int WarehouseId, ListFilterCommonModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetWarehouseTransfersAsync(WarehouseId, filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.Source.ToLower().Contains(form.SearchValue)
                                    || x.Destination.ToLower().Contains(form.SearchValue)
                                    || x.TransferDate.ToLower().Contains(form.SearchValue)
                                    || x.CreatedBy.ToLower().Contains(form.SearchValue)
                                    || x.Remarks.ToLower().Contains(form.SearchValue));
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
                        case "Id": data = data.OrderBy(s => s.Id); break;
                        case "Source": data = data.OrderBy(s => s.Source); break;
                        case "Destination": data = data.OrderBy(s => s.Destination); break;
                        case "ItemCount": data = data.OrderBy(s => s.ItemCount); break;
                        case "TransferDate": data = data.OrderBy(s => s.TransferDateD); break;
                        case "CreatedBy": data = data.OrderBy(s => s.CreatedBy); break;
                        case "Remarks": data = data.OrderBy(s => s.Remarks); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Id": data = data.OrderByDescending(s => s.Id); break;
                        case "Source": data = data.OrderByDescending(s => s.Source); break;
                        case "Destination": data = data.OrderByDescending(s => s.Destination); break;
                        case "ItemCount": data = data.OrderByDescending(s => s.ItemCount); break;
                        case "TransferDate": data = data.OrderByDescending(s => s.TransferDateD); break;
                        case "CreatedBy": data = data.OrderByDescending(s => s.CreatedBy); break;
                        case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<WarehouseTransferVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        private async Task<IList<ItemTransferVM>> GetItemTransfersAsync()
        {
            var transferVM = (from transfer in context.TransferItemDetails
                                                      .Include(s => s.TransferItem).ThenInclude(s => s.Source)
                                                      .Include(s => s.TransferItem).ThenInclude(s => s.Destination)
                                                      .Include(s => s.Item).ThenInclude(s => s.Unit)
                              join user in userManager.Users on transfer.UserCreated equals user.Id
                              select new ItemTransferVM
                              {
                                  Code = transfer.Item.Code,
                                  Destination = transfer.TransferItem.Destination.Code,
                                  Id = transfer.TransferItem.Id,
                                  Name = transfer.Item.Name,
                                  Quantity = transfer.Quantity.ToString(),
                                  Remarks = transfer.TransferItem.Remarks,
                                  Source = transfer.TransferItem.Source.Code,
                                  CreatedBy = user.FirstName + " " + user.LastName,
                                  TransferDateD = transfer.TransferItem.TransferDate,
                                  Unit = transfer.Item.Unit.Code
                              });
            return await transferVM.ToListAsync();
        }

        public async Task<ForDataTables<ItemTransferVM>> GetItemTransfersDTAsync(DataTablesRequestForm form, int? TransferId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemTransfersAsync()).Where(s => TransferId == null || s.Id == TransferId).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Code.ToString().Contains(form.SearchValue)
                                    || x.Destination.ToString().Contains(form.SearchValue)
                                    || x.Id.ToString().Contains(form.SearchValue)
                                    || x.Name.ToString().Contains(form.SearchValue)
                                    || x.Quantity.ToLower().Contains(form.SearchValue)
                                    || x.Remarks.ToLower().Contains(form.SearchValue)
                                    || x.Source.ToLower().Contains(form.SearchValue)
                                    || x.CreatedBy.ToLower().Contains(form.SearchValue)
                                    || x.TransferDate.ToLower().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue));
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
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "Destination": data = data.OrderBy(s => s.Destination); break;
                        case "Id": data = data.OrderBy(s => s.Id); break;
                        case "Name": data = data.OrderBy(s => s.Name); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "Remarks": data = data.OrderBy(s => s.Remarks); break;
                        case "Source": data = data.OrderBy(s => s.Source); break;
                        case "CreatedBy": data = data.OrderBy(s => s.CreatedBy); break;
                        case "TransferDate": data = data.OrderBy(s => s.TransferDateD); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "Destination": data = data.OrderByDescending(s => s.Destination); break;
                        case "Id": data = data.OrderByDescending(s => s.Id); break;
                        case "Name": data = data.OrderByDescending(s => s.Name); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "Remarks": data = data.OrderByDescending(s => s.Remarks); break;
                        case "Source": data = data.OrderByDescending(s => s.Source); break;
                        case "CreatedBy": data = data.OrderByDescending(s => s.CreatedBy); break;
                        case "TransferDate": data = data.OrderByDescending(s => s.TransferDateD); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemTransferVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<int> AddAsync(TransferFormVM formVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var transferVM = new TransferItem
            {
                DestinationId = formVM.Destination,
                Remarks = formVM.Remarks + "",
                SourceId = formVM.Source,
                TransferDate = formVM.TransferDate
            };
            await context.TransferItems.AddAsync(transferVM);
            await context.SaveChangesAsync(currentUserId);

            var itemsVM = formVM.Items.Select(s => new TransferItemDetail
            {
                ItemId = s.Id,
                Quantity = s.Quantity,
                TransferItemId = transferVM.Id
            });
            await context.AddRangeAsync(itemsVM);
            await context.SaveChangesAsync(currentUserId);

            foreach (var item in formVM.Items)
            {
                #region ItemInventories
                var sourceItem = await context.ItemInventories
                                               .Where(s => s.WarehouseId == formVM.Source && s.ItemId == item.Id)
                                               .FirstAsync();
                sourceItem.Quantity -= item.Quantity;
                context.ItemInventories.Update(sourceItem);
                await context.SaveChangesAsync(currentUserId);

                var destinationItem = await context.ItemInventories
                                                   .Where(s => s.WarehouseId == formVM.Destination && s.ItemId == item.Id)
                                                   .FirstOrDefaultAsync();
                if (destinationItem != null)
                {
                    destinationItem.Quantity += item.Quantity;
                    context.ItemInventories.Update(destinationItem);
                }
                else
                {
                    destinationItem = new ItemInventory
                    {
                        ItemId = item.Id,
                        Quantity = item.Quantity,
                        WarehouseId = formVM.Destination
                    };
                    await context.ItemInventories.AddAsync(destinationItem);
                }
                await context.SaveChangesAsync(currentUserId);
                #endregion

                #region LocationCode
                var locationCodes = await context.ItemLocations.Where(x => x.ItemId == item.Id && x.LocationCodeId == item.LocationCodeId).Select(x => x.LocationCodeId).ToListAsync();
                if (!locationCodes.Contains(item.LocationCodeId.GetValueOrDefault()))
                {
                    if (item.LocationCodeId != null)
                    {
                        var itemLoc = new ItemLocation
                        {
                            ItemId = item.Id,
                            LocationCodeId = item.LocationCodeId ?? 0
                        };
                        await context.ItemLocations.AddAsync(itemLoc);
                        await context.SaveChangesAsync(currentUserId);
                    }
                }
                #endregion
            }

            return transferVM.Id;
        }
    }
}