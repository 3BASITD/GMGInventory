using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IReportAccountingRepository
    {
        Task<IList<CreditDebitMemoVM>> GetCreditMemo(ListFilterCustomerModel filter);
        Task<ForDataTables<CreditDebitMemoVM>> GetCreditMemoDT(DataTablesRequestForm form, ListFilterCustomerModel filter);

        Task<IList<CreditDebitMemoVM>> GetDebitMemo(ListFilterCustomerModel filter);
        Task<ForDataTables<CreditDebitMemoVM>> GetDebitMemoDT(DataTablesRequestForm form, ListFilterCustomerModel filter);
    }
}