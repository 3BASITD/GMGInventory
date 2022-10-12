using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AreaRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }


        public async Task<int?> GetIdAsync(string? Name)
        {
            var area = await context.Areas.Where(s => s.Name == Name).FirstOrDefaultAsync();
            return area?.Id;
        }


        public async Task<IList<AreaViewVM>> GetAllAreaVMAsync()
        {
            var areas = await context.Areas.Include(s => s.Region).ToListAsync();
            var areasViewVM = mapper.Map<List<AreaViewVM>>(areas);
            foreach (AreaViewVM ivm in areasViewVM)
            {
                ivm.UserCreated = await GetUserName(ivm.UserCreated);
                ivm.UserModified = await GetUserName(ivm.UserModified);
            }
            return areasViewVM;
        }

        public async Task<AreaViewVM?> GetAreaViewVMAsync(int? id)
        {
            var areas = await context.Areas.Include(s => s.Region).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (areas == null)
            {
                return null;
            }
            var areasViewVM = mapper.Map<AreaViewVM>(areas);
            areasViewVM.UserCreated = await GetUserName(areasViewVM.UserCreated);
            areasViewVM.UserModified = await GetUserName(areasViewVM.UserModified);
            return areasViewVM;
        }

        public async Task<AreaVM?> GetAreaVMAsync(int? id)
        {
            var area = await context.Areas.Include(s => s.Region).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (area == null)
            {
                return null;
            }
            var areaViewVM = mapper.Map<AreaVM>(area);
            return areaViewVM;
        }

        public async Task<IList<Area>> GetAllActiveAreaAsync(int includeId)
        {
            return await context.Areas.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            //var areas = await context.Areas.ToListAsync();
            //return areas;
        }

        public async Task<bool> IsAreaCodeExist(string code)
        {
            return await context.Areas.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsAreaInUse(int id)
        {
            return await context.Regions.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> IsAreaIdExistAsync(int id)
        {
            return await context.Areas.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateAreaAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var area = await context.Areas.FirstAsync(s => s.Id == id);
            area.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateAreaAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var area = await context.Areas.FirstAsync(s => s.Id == id);
            area.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(AreaVM areaVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var area = mapper.Map<Area>(areaVM);
            area.IsActive = true;
            await context.Areas.AddAsync(area);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(int id, AreaVM areaVM)
        {
            var area = context.Areas.Where(s => s.Id == id).FirstOrDefault();
            if (area == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            mapper.Map(areaVM, area);
            context.Areas.Update(area);
            await context.SaveChangesAsync(currentUserId);
        }


        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Areas
                .Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .ToListAsync();
        }

    }
}