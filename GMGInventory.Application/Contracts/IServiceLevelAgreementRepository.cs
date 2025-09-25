using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IServiceLevelAgreementRepository
    {
        Task<IList<ServiceLevelAgreementVM>> GetServiceLevelAgreements();
        Task<ForDataTables<ServiceLevelAgreementVM>> GetServiceLevelAgreementsDT(DataTablesRequestForm form);
        Task<ResponseModel> AddServiceLevelAgreement(ServiceLevelAgreementVM ServiceLevelAgreement);
        Task<ResponseModel> UpdateServiceLevelAgreement(ServiceLevelAgreementVM ServiceLevelAgreement);
        Task<ResponseModel> DeleteOrDeactivateServiceLevelAgreement(int? Id);
        Task<ResponseModel> ReactivateServiceLevelAgreement(int? Id);
    }
}