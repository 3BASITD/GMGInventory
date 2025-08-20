using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface ITrackerRepository
    {
        Task<IList<TaskVM>> GetTasks(TaskFilter filter);
        Task<ForDataTables<TaskVM>> GetTasksDT(DataTablesRequestForm form, TaskFilter filter);
    }
}