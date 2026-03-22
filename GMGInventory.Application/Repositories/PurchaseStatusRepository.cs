using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class PurchaseStatusRepository : IPurchaseStatusRepository
    {
        private readonly ApplicationDbContext context;

        public PurchaseStatusRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<JsonOptionsModel>> GetOptionAsync()
        {
            var status = (from stats in context.PurchaseStatuses
                          select new JsonOptionsModel
                          {
                              Id = stats.Id.ToString(),
                              Text = stats.Name,
                              Selected = false
                          });
            return await status.ToListAsync();
        }

        public async Task<Select2Model<JsonOptionsModel>> GetOptions2(string search, int page)
        {
            var status = await context.PurchaseStatuses
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
            var statusFiltered = status.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonOptionsModel>
            {
                //incomplete_results = false,
                items = statusFiltered,
                total_count = status.Count
            };
        }
    }
}