using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISoacrRepository 
    {

        Task<IList<SoacrVM>> GetAllAsync(SoaFilterVM filter);
        Task<ForDataTables<SoacrVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, SoaFilterVM filter);
        SoacrVM? GetSoacrVM(int? id);

        Task AddAsync(SoacrVM soaVM);

        Task<bool> IsSOAIdExist(int id);

        Task<bool> IsSOALineItemIdExist(int id);

        SoacrLineItemVM? GetSoaInvoiceDtlVM(int? id);

        Task UpdateAsync(SoacrLineItemVM soaVM);

        Task AddCustomerPaymentAsync(PaymentVM paymentVM);

        Task<bool> IsCustomerPaymentIdExist(int id);

        Task<ForDataTables<SoacrVM>> GetSoaDetailsDTAsync(DataTablesRequestForm form, int id);

        InvoiceVM? GetInvoiceVM(int? id);
        Task<IList<InvoiceVM>> GetContractInvoices(SoaFilterVM filter);
        Task<ForDataTables<InvoiceVM>> GetContractReceiptDTAsync(DataTablesRequestForm form, SoaFilterVM filter);

        Task<IList<InvoiceVM>> GetContractReceiptDetailsAsync(int id);

        Task<ForDataTables<InvoiceVM>> GetContractReceiptDetailsDTAsync(DataTablesRequestForm form, int id);
        Task<IList<SoacrVM>> GetSoaDetailsAsync(int id);

        Task<IList<SoacrVM>> GetSOAInvoice(/*int id*/);

        Task<SoaDetailsPVM> PrintSoaDetails(int id);
        Task<SoaDetailsPVM> PrintCRDetails(int id);

        Task<IList<SoacrVM>> GetSoaDetails(int id);
        Task<SoaDetailsPVM> PrintInvoice(SoaFilterVM filter);
        Task<bool> IsSoacrTypeCR(int id);

        //test

        Task<IList<SoacrVM>> GetAll2Async(SoaFilterVM filter);
        Task<ForDataTables<SoacrVM>> GetAllForDataTables2Async(DataTablesRequestForm form, SoaFilterVM filter);



    }
}