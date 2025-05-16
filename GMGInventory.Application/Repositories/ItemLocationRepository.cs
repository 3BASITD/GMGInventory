using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Repositories
{
    public class ItemLocationRepository : GenericRepository<ItemLocation>, IItemLocationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly string currentUser;


        public ItemLocationRepository(ApplicationDbContext context
        , IHttpContextAccessor httpContextAccessor
        , UserManager<User> userManager
        , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
            this.currentUser = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public Task<int?> GetIdAsync(string? Name)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<ItemDetailLocationVM>> GetAllLocationAsync(int id)
        {
            var locs = from loc in context.ItemLocation
                              where loc.ItemId == id
                              select new ItemDetailLocationVM
                              {
                                  Id = loc.Id,
                                  ItemId = loc.ItemId,
                                  LocationCodeId = loc.LocationCodeId,
                                  LocationCode = loc.LocationCode.Code,
                                  LocationName = loc.LocationCode.Name
                              };

            return await locs.ToListAsync();
        }
        public async Task<ForDataTables<ItemDetailLocationVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllLocationAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // get total count of records after search
            filterRecord = data.Count();
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ItemDetailLocationVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task AddAsync(ItemDetailLocationVM itemDetailLocationVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var location = new ItemLocation
            {
                Id = itemDetailLocationVM.Id,
                ItemId = itemDetailLocationVM.ItemId,
                LocationCodeId = itemDetailLocationVM.LocationCodeId
            };
            await context.ItemLocation.AddAsync(location);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task DeleteAsync(int itemId, int locationCodeId)
        {
            var location = context.ItemLocation.Where(s => s.ItemId == itemId && s.LocationCodeId >= locationCodeId).First();
            context.ItemLocation.Remove(location);
            await context.SaveChangesAsync(currentUser);
        }
    }
}
