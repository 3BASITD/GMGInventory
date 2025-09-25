using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface ISoacrRepository 
    {
        #region for SOACR/Index
        Task<ForDataTables<SOACRVM>> SOACRListDT(DataTablesRequestForm form);
        
        Task<int> GenerateSOAAsync(GenerateSOAVM soaVM);
		#endregion

		#region for SOACR/Invoice
		Task<ForDataTables<SOACRInvoices>> GetInvoiceListDTAsync(DataTablesRequestForm form, ListFilterSalesModel filter);
        Task<SOACRInvoicesPVM> PrintInvoice(ListFilterSalesModel filter);
        #endregion

        #region for SOACR/Generate CR
        Task<ForDataTables<SoacrInvoiceVM>> GetCustomerInvoiceToAddDT(DataTablesRequestForm form, int customerId);
        Task<int> GenerateCRAsync(int CustomerId, int[] InvoiceIds);
        #endregion

        #region for SOACR/Details
        Task<SOACRDetailsVM?> GetSOACRDetails(int? id, bool loadItems = false);
        Task<ForDataTables<SOACRItemsVM>> GetSOACRItemsDTAsync(DataTablesRequestForm form, int id);
        Task UpdateSOACRInvoiceAsync(SOACRItemsForm soaVM);
        Task<bool> IsSOACRLineItemIdExist(int id); 
        Task<bool> IsCheckNumberInUse(string checkNumber);
        Task<bool> IsCollectorReceiptNumberInUse(string collectorReceiptNumber);
        Task<bool> AddPaymentDetails(SOACRPaymentDetails payment);
        Task<SOACRDetailsPVM> PrintCRDetails(int id);
        Task<SOACRDetailsPVM> PrintSOADetails(int id);
        Task AddInvoicesAsync(int soaId, int[] invoiceIds);
        Task<Select2Model<SOACRInvoiceTBRVM>> GetInvoicesTBRAsync(string search, int page, int CustomerId);
        Task AddInvoiceTBRAsync(int soaLineItemId, int salesReturnId, decimal amount);
        Task<ForDataTables<SOACRInvoiceTBRHistoryVM>> GetInvoiceTBRHistoryDTAsync(DataTablesRequestForm form, int soaLineItemId);
        #endregion




        //Task<bool> IsSOAIdExist(int id);


        ////SoacrLineItemVM? GetSoaInvoiceDtlVM(int? id);


        //Task AddCustomerPaymentAsync(PaymentVM paymentVM);

        ////Task<SoacrPaymentDetails> GetPaymentDetails(SoacrPaymentFilter filter);

        ////Task<ForDataTables<SoacrPaymentCheck>> GetPaymentChecksDTAsync(DataTablesRequestForm form, int customerPaymentId);

        ////Task<bool> AddPaymentCheck(SoacrPaymentCheck check);

        ////Task<bool> RemovePaymentCheck(int soaCheckId);

        //


        //InvoiceVM? GetInvoiceVM(int? id);


        //Task<IList<InvoiceVM>> GetContractReceiptDetailsAsync(int id);

        //Task<ForDataTables<InvoiceVM>> GetContractReceiptDetailsDTAsync(DataTablesRequestForm form, int id);

        //Task<IList<SoacrVM>> GetSOAInvoice(/*int id*/);


        //Task<IList<SoacrVM>> GetSoaDetails(int id);
        //Task<bool> IsSoacrTypeCR(int id);

        ////test

        //Task<IList<SoacrVM>> GetAll2Async(SoaFilterVM filter);
        //Task<ForDataTables<SoacrVM>> GetAllForDataTables2Async(DataTablesRequestForm form, SoaFilterVM filter);

        ////CR
        //Task<IList<SoacrVM>> GetContractInvoices2(SoaFilterVM filter);
        //Task<ForDataTables<SoacrVM>> GetContractReceipt2DTAsync(DataTablesRequestForm form, SoaFilterVM filter);

        ////Add Invoice
        //Task<IList<InvoiceVM>> GetContractInvoicesCustomer(int id, int soaId);
        //Task<ForDataTables<InvoiceVM>> GetContractReceiptCustomerDTAsync(DataTablesRequestForm form, int id, int soaId);

        ////Task<int> AddCrAsync(SoacrVM soaVM, int[] invoiceIds);


        ////test1
        ////Task<SoaDetailsPVM> GetContractInvoicesCustomer2(int id, int custId);
        //Task<IList<SoacrVM>> GetSoaDetails2(int id, int custId);

        //Task<IList<SoacrVM>> GetContractInvoices3(int? id);
        //Task<SoaDetailsPVM> GetCounterReceiptInvoices(int? id);




    }
}