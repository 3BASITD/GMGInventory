using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISoacrRepository
    {
        ForDataTables<SoacrVM> GetListDataTables(DataTablesRequestForm form);

        //      ForDataTables<SOACrVM> GetListDataTablesDetails(DataTablesRequestForm form);

        SoacrVM? GetSoacrVM(int? id);
    }
}