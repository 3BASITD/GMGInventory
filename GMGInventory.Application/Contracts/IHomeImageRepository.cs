using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IHomeImageRepository
    {
        Task<IList<HomeImageVM>> GetListAsync();
        Task<ForDataTables<HomeImageVM>> GetListDTAsync(DataTablesRequestForm form);
        Task<ResponseModel> SaveAsync(HomeImageFormVM form);
        Task<ResponseModel> DeleteAsync(int ImageId);
        Task<ResponseModel> MoveUpAsync(int ImageId);
        Task<ResponseModel> MoveDownAsync(int ImageId);
    }
}