using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class ItemClassRepository : IItemClassRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public ItemClassRepository(ApplicationDbContext context
             , IHttpContextAccessor httpContextAccessor
             , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.ItemClasses
                                .Where(s => s.IsActive || s.Id == IncludeId)
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = (s.Code == s.Name ? "" : "(" + s.Code + ") ") + s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page)
        {
            var itemClasses = await context.ItemClasses
                                           .Where(s => s.IsActive
                                                    && (string.IsNullOrWhiteSpace(search)
                                                    || (s.Code.ToLower().Contains(search.ToLower()))
                                                    || (s.Name.ToLower().Contains(search.ToLower()))))
                                           .Select(s => new JsonNameOptionsModel
                                           {
                                               Id = s.Id.ToString(),
                                               Text = s.Code,
                                               Name = s.Name
                                           })
                                           .OrderBy(s => s.Text)
                                           .ToListAsync();
            var itemClassesFiltered = itemClasses.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonNameOptionsModel>
            {
                //incomplete_results = false,
                items = itemClassesFiltered,
                total_count = itemClasses.Count
            };
        }
    }
}