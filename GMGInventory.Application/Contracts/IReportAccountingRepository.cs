using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReportAccountingRepository
    {
        Task<IList<CreditDebitMemoRVM>> GetCreditMemo(ListFilterCustomerModel filter);
        Task<ForDataTables<CreditDebitMemoRVM>> GetCreditMemoDT(DataTablesRequestForm form, ListFilterCustomerModel filter);

        Task<IList<CreditDebitMemoRVM>> GetDebitMemo(ListFilterCustomerModel filter);
        Task<ForDataTables<CreditDebitMemoRVM>> GetDebitMemoDT(DataTablesRequestForm form, ListFilterCustomerModel filter);
    }
}