using GMGInventory.Common.Models;

namespace GMGInventory.Application.Contracts
{
    public interface IVehicleRepository
    {
        Task<IList<VehicleVM>> GetVehicles();
        Task<ForDataTables<VehicleVM>> GetVehiclesDT(DataTablesRequestForm form);
        Task<ResponseModel> AddVehicle(VehicleVM Vehicle);
        Task<ResponseModel> UpdateVehicle(VehicleVM Vehicle);
        Task<ResponseModel> DeleteOrDeactivateVehicle(int? Id);
        Task<ResponseModel> ReactivateVehicle(int? Id);
    }
}