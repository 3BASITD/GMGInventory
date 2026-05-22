using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;

namespace GMGInventory.Application.Contracts
{
    public interface IChequeRepository : IGenericRepository<Cheque>
    {
        Task<IList<ChequeVM>> GetAllChequeAsync(ListFilterChequeModel filter);
        Task<ForDataTables<ChequeVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, ListFilterChequeModel filter);
        Task<IList<ChequeVM>> GetListAsync();
        Task AddAsync(ChequeVM ChequeVM);
        Task UpdateAsync(ChequeVM ChequeVM);
        Task<bool> IsRefNoExistAsync(string refNo);

        Task<bool> UploadCombinationExistAsync(ChequeVM chequeVM);
        Task<ChequePVM> PrintCheque(ListFilterChequeModel filter);
        Task<ResponseModelData<FileVM>> UploadFile(IFormFile file);
        Task<string> SaveChequeFromFile(ChequeVM cheque);
    }
}
