using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class DeliveryTimeRepository : IDeliveryTimeRepository
    {
        private readonly ApplicationDbContext context;

        public DeliveryTimeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<JsonOptionsModel>> GetOptionAsync(int? SelectedId)
        {
            var times = (from time in context.DeliveryTimes
                         select new JsonOptionsModel
                         {
                             Id = time.Id.ToString(),
                             Text = time.Name,
                             Selected = time.Id == SelectedId
                         });
            return await times.ToListAsync();
        }
    }
}