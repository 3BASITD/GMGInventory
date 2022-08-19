using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPaymentRepository
    {
        ForDataTables<PaymentVM> GetListDataTables(DataTablesRequestForm form);

        PaymentVM? GetPaymentVM(int? id);

        Task<ForDataTables<PaymentVM>> GetPaymentChecksDTAsync(DataTablesRequestForm form, int id);
    }
}