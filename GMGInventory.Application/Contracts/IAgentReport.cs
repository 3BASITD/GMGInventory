using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IAgentReport
    {
        Task<IList<AgentItirenaryVM>> GetAgentItineraryDT(ListFilterCustomerModel filter);

        Task<ForDataTables<AgentItirenaryVM>> GetAgentDT(DataTablesRequestForm form, ListFilterCustomerModel filter);

        Task<AgentWeeklyVM> PrintAgentWeeklyReport(ListFilterCustomerModel filter);

        Task<AgentMonthlyVM> PrintAgentMonthlyReport(ListFilterCustomerModel filter);
    }
}
