using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class CustomerAccountStatusRepository: ICustomerAccountStatusRepository
    {
        private readonly ApplicationDbContext context;

        public CustomerAccountStatusRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.CustomerAccountStatuses
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }
    }
}
