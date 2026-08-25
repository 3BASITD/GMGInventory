using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IKYCRepository
    {
        Task<ForDataTables<KYCModel>> GetKYCsDT(DataTablesRequestForm form);
        Task<KYCVM> GetDetails(int CustomerId);
        Task<ResponseModel> SaveDetails(KYCParam param);
        Task<ResponseModel> ApproveDetails(int CustomerId);
        Task<ResponseModel> DisapproveDetails(int CustomerId, string Reason);
        Task<ResponseModel> EditDetails(int CustomerId);
        Task<ForDataTables<KYCSignatoryModel>> GetKYCSignatoriesDT(DataTablesRequestForm form, int CustomerId);
        Task<ResponseModel> SaveSignatory(KYCSignatoryModel param);
        Task<ResponseModel> DeleteSignatory(int SignatoryId, int KYCId);
        Task<ForDataTables<KYCClientModel>> GetKYCClientsDT(DataTablesRequestForm form, int CustomerId);
        Task<ResponseModel> SaveClient(KYCClientModel param);
        Task<ResponseModel> DeleteClient(int ClientId, int KYCId);
        Task<ForDataTables<KYCBankModel>> GetKYCBanksDT(DataTablesRequestForm form, int CustomerId);
        Task<ResponseModel> SaveBank(KYCBankModel param);
        Task<ResponseModel> DeleteBank(int BankId, int KYCId);
        Task<IList<KYCDocumentModel>> GetKYCDocuments(int CustomerId);
        Task<ResponseModel> SaveDocument(KYCDocumentModel param);
        Task<ResponseModel> DeleteDocument(int DocumentId, int KYCId);
    }
}