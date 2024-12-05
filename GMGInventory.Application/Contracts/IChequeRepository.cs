using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IChequeRepository : IGenericRepository<Cheque>
    {
        Task<IList<ChequeVM>> GetAllChequeAsync();
        Task<ForDataTables<ChequeVM>> GetAllForDataTablesAsync(DataTablesRequestForm form);

        Task<IList<ChequeVM>> GetListAsync();

        Task AddAsync(ChequeVM ChequeVM);
        Task UpdateAsync(ChequeVM ChequeVM);
        Task<bool> IsRefNoExistAsync(string refNo);

        Task<bool> UploadCombinationExistAsync(ChequeVM chequeVM);
    }
}
