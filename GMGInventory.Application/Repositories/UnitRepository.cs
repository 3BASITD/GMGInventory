using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
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
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UnitRepository(ApplicationDbContext context
           , IHttpContextAccessor httpContextAccessor
           , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Units
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }

        public async Task<Select2Model<JsonOptionsModel>> GetOptions2(string search, int page)
        {
            var units = await context.Units
                                     .Where(s => string.IsNullOrWhiteSpace(search)
                                              || (s.Code.ToLower().Contains(search.ToLower()))
                                              || (s.Name.ToLower().Contains(search.ToLower())))
                                     .Select(s => new JsonOptionsModel
                                     {
                                         Id = s.Id.ToString(),
                                         Text = s.Name
                                     })
                                     .OrderBy(s => s.Text)
                                     .ToListAsync();
            var unitsFiltered = units.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonOptionsModel>
            {
                //incomplete_results = false,
                items = unitsFiltered,
                total_count = units.Count
            };
        }

        public async Task<IList<APIGeneric>> GetListAPI()
        {
            var units = from unit in context.Units
                        select new APIGeneric
                        {
                            Id = unit.Id,
                            Code = unit.Code,
                            Name = unit.Name
                        };
            return await units.ToListAsync();
        }
    }
}
