using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ISoacrLineItemRepository : IGenericRepository<SOACRLineItem>
    {

        Task<bool> IsSoaLineItemIdExistAsync(int id);

    }
}