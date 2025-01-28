using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class ChequeStatusRepository : IChequeStatusRepository
    {
        private readonly ApplicationDbContext context;

        public ChequeStatusRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.ChequeStatuses
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Id)
                .ToListAsync();
        }

        public async Task<int?> GetIdByCodeAsync(string? Code)
        {
            var status = await context.ChequeStatuses.Where(s => s.Code == Code).FirstOrDefaultAsync();
            return status?.Id;
        }
    }
}
