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

        public async Task<List<AreaVM>> GetAllAreaVMAsync()
        {
            var areas = await context.Areas.Include(s => s.Region).ToListAsync();
            var areasViewVM = mapper.Map<List<AreaVM>>(areas);
            foreach (AreaVM ivm in areasViewVM)
            {
                ivm.UserCreated = await GetUserName(ivm.UserCreated);
                ivm.UserModified = await GetUserName(ivm.UserModified);
            }
            return areasViewVM;
        }

        public async Task<AreaVM?> GetAreaVMAsync(int? id)
        {
            var areas = await context.Areas.Include(s => s.Region).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (areas == null)
            {
                return null;
            }
            var areasViewVM = mapper.Map<AreaVM>(areas);
            areasViewVM.UserCreated = await GetUserName(areasViewVM.UserCreated);
            areasViewVM.UserModified = await GetUserName(areasViewVM.UserModified);
            return areasViewVM;
        }

        public async Task<List<Area>> GetAllActiveAreaAsync(int includeId)
        {
            return await context.Areas.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsAreaCodeExist(string code)
        {
            return await context.Areas.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsAreaInUse(int id)
        {
            return await context.Regions.AnyAsync(s => s.Id == id);
        }
    }
}