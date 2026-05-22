using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IItemClassRepository
    {
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page);
        Task<IList<GenericAuditVM>> GetListAsync();
        Task<ForDataTables<GenericAuditVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<ResponseModel> AddAsync(GenericAuditVM iclassVM);
        Task<ResponseModel> UpdateAsync(GenericAuditVM iclassVM);
        Task<ResponseModel> DeleteOrDeactivateAsync(int? Id);
        Task<ResponseModel> ReactivateAsync(int? Id);
    }
}