using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IPaymentRepository
    {
        Task<PaymentViewVM?> GetAsync(int? id);
        Task<PaymentFormVM?> GetToEditAsync(int? id);
        Task<List<PaymentViewVM>> GetAllAsync();
        Task<PaymentFormVM> AddAsync(PaymentFormVM paymentVM);
        Task<List<Payment>> GetAllActivePaymentAsync(int includeId);
        Task<bool> IsIdExists(int id);
        Task<bool> IsCodeExists(string code);
        Task DeleteAsync(int id);
        Task UpdateAsync(PaymentFormVM paymentVM);
        Task ReactivateAsync(int id);
        Task DeactivateAsync(int id);
        Task<bool> IsInUse(int id);
        IQueryable<PaymentViewVM> GetAll();
        ForDataTables<PaymentViewVM> GetListDT(DataTablesRequestForm form);
    }
}