using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IPaymentRepository
    {
        Task<ForDataTables<PaymentVM>> GetListDataTables(DataTablesRequestForm form);

        PaymentVM? GetPaymentVM(int? id);

        Task<ForDataTables<PaymentVM>> GetPaymentChecksDTAsync(DataTablesRequestForm form, int id);

        Task<ForDataTables<PaymentVM>> GetPaymentInvoicesDTAsync(DataTablesRequestForm form, int id);
    }
}