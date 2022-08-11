using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPaymentRepository
    {
        ForDataTables<PaymentVM> GetListDataTables(DataTablesRequestForm form);

        ForDataTables<PaymentVM> GetListDataTablesDetails (DataTablesRequestForm form);

        PaymentVM? GetPaymentVM(int? id);
    }
}