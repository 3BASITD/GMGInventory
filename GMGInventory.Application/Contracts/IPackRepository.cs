using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPackRepository
    {
        Task<Select2Model<JsonOptionsModel>> GetOptions(string search, int page);
        Task<PackDetailsVM?> GetDetailsAsync(int? Id);
        Task<PackDetailsVM?> GetDetailsBySOAsync(int? Id);
        Task<ForDataTables<PackVM>> GetListDTAsync(DataTablesRequestForm form, ListFilterCommonModel filter);
        Task<IList<PackItemVM>> GetItemsAsync(int packId);
        Task<IList<PackItemVM>> GetItemsWithPriceAsync(int packId);
        Task<ForDataTables<PackItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int packId);

    }
}