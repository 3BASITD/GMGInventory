using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IDriverRepository
    {
        Task<IList<DriverVM>> GetDrivers();
        Task<ForDataTables<DriverVM>> GetDriversDT(DataTablesRequestForm form);
        Task<IList<APIDriver>> GetListAPI();
        Task<ResponseModel> AddDriver(DriverVM driver);
        Task<ResponseModel> UpdateDriver(DriverVM driver);
        Task<ResponseModel> DeleteOrDeactivateDriver(int? Id);
        Task<ResponseModel> ReactivateDriver(int? Id);
        Task<Select2Model<JsonOptionsModel>> GetOptions(string search, int page);
    }
}