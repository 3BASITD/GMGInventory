using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReportManagementRepository
    {
        Task<IList<ReportManagementCheckRegister>> GetCheckRegisterByDate(ListFilterCommonModel filter);
        //Task<ForDataTables<ReportManagementRegisterSummary>> GetCheckRegisterByDateDT(DataTablesRequestForm form, ListFilterCommonModel filter);
        
        Task<IList<ReportManagementCheckRegister>> GetCheckRegisterByCustomer(ListFilterCustomerModel filter);
        //Task<ForDataTables<ReportManagementRegisterSummary>> GetCheckRegisterByCustomerDT(DataTablesRequestForm form, ListFilterCustomerModel filter);

        Task<IList<ReportManagementRegisterSummary>> GetCheckRegisterSummary(ListFilterCommonModel filter);
        Task<ForDataTables<ReportManagementRegisterSummary>> GetCheckRegisterSummaryDT(DataTablesRequestForm form, ListFilterCommonModel filter);
    }
}