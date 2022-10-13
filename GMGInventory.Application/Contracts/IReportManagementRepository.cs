using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReportManagementRepository
    {
        Task<IList<ReportManagementCheckRegisterRVM>> GetCheckRegisterByDate(ListFilterCustomerModel filter);
        Task<IList<ReportManagementCheckRegisterRVM>> GetCheckRegisterByCustomer(ListFilterCustomerModel filter);
        Task<ForDataTables<ReportManagementCheckRegisterRVM>> GetCheckRegisterDT(DataTablesRequestForm form, ListFilterCustomerModel filter);

        Task<IList<ReportManagementRegisterSummaryRVM>> GetCheckRegisterSummary(ListFilterCommonModel filter);
        Task<ForDataTables<ReportManagementRegisterSummaryRVM>> GetCheckRegisterSummaryDT(DataTablesRequestForm form, ListFilterCommonModel filter);
    }
}