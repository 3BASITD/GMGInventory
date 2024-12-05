using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class BusinessStyleRepository : GenericRepository<BusinessStyle>, IBusinessStyleRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public BusinessStyleRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.BusinessStyles
                 .Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
            .ToListAsync();
        }

        public async Task<IList<BusinessStyle>> GetAllBusinessStyleAsync(int includeId)
        {
          return await context.BusinessStyles.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var businessStyle = await context.BusinessStyles.Where(s => s.Name == Name).FirstOrDefaultAsync();
            return businessStyle?.Id;
        }

    }
}
