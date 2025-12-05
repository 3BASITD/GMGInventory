using GMGInventory.Common.Models;
using GMGInventory.Data;

namespace GMGInventory.Application.Contracts
{
    public interface IWarehouseRepository
    {
        Task<IList<JsonNameOptionsModel>> GetOptions(int? IncludeId);
        Task<WarehouseVM?> GetWarehouse(int Id);
        Task<ForDataTables<WarehouseVM>> GetWarehousesDT(DataTablesRequestForm form);
        Task<IList<APIGeneric>> GetListAPI();
        Task<ResponseModel> AddAsync(WarehouseVM warehouse);
        Task<ResponseModel> UpdateAsync(WarehouseVM warehouse);
        Task<ResponseModel> DeleteDeactivateAsync(int Id);
        Task<ResponseModel> ReactivateAsync(int Id);
        Task<ResponseModel> SetMainAsync(int id);
        Task<int?> GetIdAsync(string? Name);
        Task<IList<Warehouse>> GetAllActiveWarehouseAsync(int includeId);
    }
}