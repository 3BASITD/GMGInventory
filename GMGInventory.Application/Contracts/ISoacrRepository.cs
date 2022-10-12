using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISoacrRepository
    {
        ForDataTables<SoacrVM> GetListDataTables(DataTablesRequestForm form);

        SoacrVM? GetSoacrVM(int? id);
        Task<ForDataTables<SoacrVM>> GetSoaDetailsDTAsync(DataTablesRequestForm form, int id);



    }
}