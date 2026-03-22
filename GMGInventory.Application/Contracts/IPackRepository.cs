using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPackRepository
    {
        Task<IList<JsonOptionsModel>> GetPackTypeOptions(int? IncludeId);
        Task<Select2Model<JsonOptionsModel>> GetOptions(string search, int page);
        Task<PackDetailsVM?> GetDetailsAsync(int? Id); 
        Task<PackDetailsPVM> PrintDetailsAsync(int? Id);
        Task<PackDetailsVM?> GetDetailsBySOAsync(int? Id);
        Task<IList<PackVM>> GetListAsync(ListFilterCommonModel filter);
        Task<ForDataTables<PackVM>> GetListDTAsync(DataTablesRequestForm form, ListFilterCommonModel filter);
        Task<IList<APIPack>> GetListAPI();
        Task<IList<PackItemVM>> GetItemsAsync(int packId);
        Task<IList<PackItemVM>> GetItemsWithPriceAsync(int packId);
        Task<ForDataTables<PackItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int packId);
        string? CheckItem(PackFormVM form);
        Task<int> SaveAsync(PackFormVM form);
        Task<int> UpdateAsync(PackFormVM form);
        Task<ResponseModel> CancelAsync(int Id);
        Task<ResponseModel> DeleteAsync(int Id);
        Task<ResponseModelData<int>> AddAPI(PackFormAPIVM form);
    }
}