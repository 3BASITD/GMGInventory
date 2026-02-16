using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISalesReturnRepository
    {
        Task<SalesReturnVM?> GetDetailsAsync(int? Id);
        Task<ForDataTables<SalesReturnVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<IList<APISalesReturn>> GetListAPI();
        Task<IList<SalesReturnItemVM>> GetItemsAsync(int salesReturnId);
        Task<string> GetNextFormattedNumber();
        Task<ResponseModel<int>> SaveAsync(SalesReturnFormVM form);
        Task<SalesReturnDetailPVM> PrintDetails(int returnId);
    }
}