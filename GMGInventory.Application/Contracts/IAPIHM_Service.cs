using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IAPIHM_Service
    {
        Task<ResponseModel<string>> MoveSalesInvoice(int? Id, string Host, string User, string Password);
        Task<IList<ResponseModel<string>>> MoveAllPostedSalesInvoice(DateTime DateFrom, DateTime DateTo, string Host, string User, string Password);
        Task<IList<ResponseModel<string>>> MoveAllSelectedSalesInvoice(List<int> Ids, string Host, string User, string Password);
        Task<ResponseModel<string>> MovePurchaseOrder(int? Id, string Host, string User, string Password);
        Task<IList<ResponseModel<string>>> MoveAllPostedPurchaseOrder(DateTime DateFrom, DateTime DateTo, string Host, string User, string Password);
        Task<ResponseModel<APIHM_SuccessResponse>> GetTaxes(string Host, string User, string Password);
        Task<ResponseModel<APIHM_SuccessResponse>> GetCurrencies(string Host, string User, string Password);
    }
}