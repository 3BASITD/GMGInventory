using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IAuditLogRepository
    {
        ForDataTables<AuditLogVM> GetListDataTables(DataTablesRequestForm form);
    }
}