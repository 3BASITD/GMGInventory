using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReportManagementRepository
    {
        Task<IList<CheckRegisterRVM>> GetCheckRegisterByDate(ListFilterCustomerModel filter);
        Task<IList<CheckRegisterRVM>> GetCheckRegisterByCustomer(ListFilterCustomerModel filter);
        Task<ForDataTables<CheckRegisterRVM>> GetCheckRegisterDT(DataTablesRequestForm form, ListFilterCustomerModel filter);
        Task<CheckRegisterPVM> PrintCheckRegisterByDate(ListFilterCustomerModel filter);
        Task<CheckRegisterPVM> PrintCheckRegisterByCustomer(ListFilterCustomerModel filter);

        Task<IList<RegisterSummaryRVM>> GetCheckRegisterSummary(ListFilterCommonModel filter);
        Task<ForDataTables<RegisterSummaryRVM>> GetCheckRegisterSummaryDT(DataTablesRequestForm form, ListFilterCommonModel filter);
        Task<RegisterSummaryPVM> PrintCheckRegisterSummary(ListFilterCommonModel filter);
    }
}