using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class RegionRepository : GenericRepository<Region>, IRegionRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public RegionRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IList<RegionVM>> GetAllRegionVMAsync()
        {
            var regions = await context.Regions.ToListAsync();
            var regionsVM = mapper.Map<List<RegionVM>>(regions);
            foreach (RegionVM cvm in regionsVM)
            {
                cvm.UserCreated = await GetUserName(cvm.UserCreated);
                cvm.UserModified = await GetUserName(cvm.UserModified);
            }
            return regionsVM;
        }

        public async Task<RegionVM?> GetRegionVMAsync(int? id)
        {
            var regions = await context.Regions.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (regions == null)
            {
                return null;
            }
            var regionsVM = mapper.Map<RegionVM>(regions);
            regionsVM.UserCreated = await GetUserName(regionsVM.UserCreated);
            regionsVM.UserModified = await GetUserName(regionsVM.UserModified);
            return regionsVM;
        }

        public async Task<IList<Region>> GetAllActiveRegionAsync(int includeId)
        {
            //return await context.Regions.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            var region = await context.Regions.ToListAsync();
            return region;
        }

        public async Task<bool> IsRegionCodeExist(string code)
        {
            return await context.Regions.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsRegionInUse(int id)
        {
            return await context.Areas.AnyAsync(s => s.RegionId == id);
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Regions
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