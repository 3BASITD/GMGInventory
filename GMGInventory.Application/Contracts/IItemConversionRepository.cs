using GMGInventory.Common.Models;
using GMGInventory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Contracts
{
    public interface IItemConversionRepository : IGenericRepository<ItemConversion>
    {
        Task<bool> IsConversionExistAsync(int itemId, int? childItemId, int? unitId, int? equivalentUnitId);
        Task AddAsync(ItemDetailConversionVM itemConversionVM);
        Task<ForDataTables<ItemDetailConversionVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, int id);
        Task UpdateAsync(ItemDetailConversionVM itemConversionVM);
        Task<bool> IsIdExistAsync(int id);
        ItemDetailConversionVM? GetItemConversionVM(int? id);
        Task DeleteConversionAsync(int itemId, int orderNo);
        Task<bool> HasSubItem(int itemId);
    }
}
