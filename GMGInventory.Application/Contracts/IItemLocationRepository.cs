using GMGInventory.Common.Models;
using GMGInventory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Contracts
{
  public interface IItemLocationRepository : IGenericRepository<ItemLocation>
    {
        Task<int?> GetIdAsync(string? Name);
        Task<ForDataTables<ItemDetailLocationVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, int id);
        Task AddAsync(ItemDetailLocationVM itemDetailLocationVM);
        Task DeleteAsync(int itemId, int locationCodeId);
    }
}
