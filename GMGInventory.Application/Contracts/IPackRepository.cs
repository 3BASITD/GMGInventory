using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPackRepository
    {
        Task<PackDetailsVM?> GetDetailsAsync(int? Id);
        Task<ForDataTables<PackVM>> GetListDTAsync(DataTablesRequestForm form, ListFilterCommonModel filter);
        Task<IList<PackItemVM>> GetItemsAsync(int packId);
        Task<ForDataTables<PackItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int packId);

    }
}