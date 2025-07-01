using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class ItemConversionRepository : GenericRepository<ItemConversion>, IItemConversionRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public ItemConversionRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        
        public async Task<bool> IsConversionExistAsync(int itemId, int? childItemId, int? unitId, int? equivalentUnitId)
        {
            return await context.ItemConversions.AnyAsync(s => s.ItemId == itemId &&
                                                            s.UnitId == unitId && 
                                                            s.EquivalentUnitId == equivalentUnitId &&
                                                            s.SubItemId == childItemId);
        }
        public async Task AddAsync(ItemDetailConversionVM ItemDetailConversionVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var hasConversion = await context.ItemConversions.AnyAsync(s => s.ItemId == ItemDetailConversionVM.ItemId);

            if (!hasConversion)
            {
                var item = await context.Items.Where(x => x.Id == ItemDetailConversionVM.ItemId).FirstOrDefaultAsync();
                if (item != null)
                {
                    item.UnitId = ItemDetailConversionVM.UnitId ?? 0;
                    context.Items.Update(item);
                    await context.SaveChangesAsync(currentUserId);
                }
            }

            var conversion = new ItemConversion
            {
                ItemId = ItemDetailConversionVM.ItemId,
                Quantity = ItemDetailConversionVM.Quantity,
                SubItemId = ItemDetailConversionVM.SubItemId,
                UnitId = ItemDetailConversionVM.UnitId,
                EquivalentQuantity = ItemDetailConversionVM.EquivalentQuantity == 0 ? null : ItemDetailConversionVM.EquivalentQuantity,
                EquivalentUnitId = ItemDetailConversionVM.EquivalentUnitId,
                OrderNo = ItemDetailConversionVM.OrderNo
            };
            await context.ItemConversions.AddAsync(conversion);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task<ForDataTables<ItemDetailConversionVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllConversionAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // get total count of records after search
            filterRecord = data.Count();
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemDetailConversionVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<ItemDetailConversionVM>> GetAllConversionAsync(int id)
        {
            var conversions = from conversion in context.ItemConversions
                              where conversion.ItemId == id
                              select new ItemDetailConversionVM
                              {
                                  Id = conversion.Id,
                                  ItemId = conversion.ItemId,
                                  Quantity = conversion.Quantity,
                                  SubItemId = conversion.SubItemId,
                                  SubItemCode = conversion.SubItem.Code,
                                  UnitId = conversion.UnitId,
                                  UnitName = conversion.Unit.Name,
                                  EquivalentQuantity = conversion.EquivalentQuantity,
                                  EquivalentUnitId = conversion.EquivalentUnitId,
                                  EquivalentUnitName = conversion.EquivalentUnit.Name,
                                  OrderNo = conversion.OrderNo
                              };

            return await conversions.ToListAsync();
        }

        public async Task UpdateAsync(ItemDetailConversionVM itemDetailConversionVM)
        {
            var conversion = context.ItemConversions.Where(s => s.Id == itemDetailConversionVM.Id).FirstOrDefault();
            if (conversion == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            conversion.Id = itemDetailConversionVM.Id;
            conversion.Quantity = itemDetailConversionVM.Quantity;
            conversion.UnitId = itemDetailConversionVM.UnitId;
            conversion.EquivalentUnitId = itemDetailConversionVM.EquivalentUnitId;
            conversion.EquivalentQuantity = itemDetailConversionVM.EquivalentQuantity == 0 ? null : itemDetailConversionVM.EquivalentQuantity;
            //conversion.OrderNo = itemDetailConversionVM.OrderNo;
            conversion.SubItemId = itemDetailConversionVM.SubItemId;

            context.ItemConversions.Update(conversion);
            await context.SaveChangesAsync(currentUserId);


            if (itemDetailConversionVM.OrderNo == 1)
            {
                var item = await context.Items.Where(x => x.Id == itemDetailConversionVM.ItemId).FirstOrDefaultAsync();
                if (item != null)
                {
                    item.UnitId = itemDetailConversionVM.UnitId ?? 0;
                    context.Items.Update(item);
                    await context.SaveChangesAsync(currentUserId);
                }
            }
        }

        public async Task<bool> IsIdExistAsync(int id)
        {
            return await context.ItemConversions.AnyAsync(s => s.Id == id);
        }

        public ItemDetailConversionVM? GetItemConversionVM(int? itemId)
        {
            var itemVM = (from item in context.ItemConversions
                          where item.ItemId == itemId
                          orderby item.OrderNo
                          select new ItemDetailConversionVM
                          {
                              Id = item.Id,
                              SubItemId = item.SubItemId,
                              UnitId = item.UnitId,
                              EquivalentUnitId = item.EquivalentUnitId,
                              OrderNo = item.OrderNo + 1
                          }).LastOrDefault();
            return itemVM;
        }

        public async Task DeleteConversionAsync(int itemId, int orderNo)
        {
            var conversions = context.ItemConversions.Where(s => s.ItemId == itemId && s.OrderNo >= orderNo).ToList();
            if (conversions.Count > 0)
            {
                context.ItemConversions.RemoveRange(conversions);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> HasSubItem(int itemId)
        {
            return await context.ItemConversions.AnyAsync(s => s.ItemId == itemId && s.SubItemId != null);
        }
    }
}
