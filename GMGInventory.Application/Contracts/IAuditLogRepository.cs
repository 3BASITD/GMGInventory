using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IAuditLogRepository
    {
        Task<ForDataTables<AuditLogVM>> GetListDataTablesAsync(DataTablesRequestForm form);
    }
}