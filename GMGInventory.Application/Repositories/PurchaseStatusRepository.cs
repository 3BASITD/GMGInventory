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
    }
}