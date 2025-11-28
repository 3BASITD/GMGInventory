using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public VehicleRepository(ApplicationDbContext context
                               , IHttpContextAccessor httpContextAccessor
                               , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IList<VehicleVM>> GetVehicles()
        {
            var vehicles = from vehicle in context.Vehicles
                           join userc in userManager.Users on vehicle.UserCreated equals userc.Id
                           join userm in userManager.Users on vehicle.UserModified equals userm.Id into m
                           from userm in m.DefaultIfEmpty()
                           select new VehicleVM
                           {
                               Id = vehicle.Id,
                               FleetPlateNumber = vehicle.FleetPlateNumber,
                               VehicleType = vehicle.VehicleType,
                               DateRegisteredD = vehicle.DateRegistered,
                               DateExpirationD = vehicle.DateExpiration,
                               IsActive = vehicle.IsActive,
                               UserCreated = userc.FirstName + " " + userc.LastName,
                               DateCreatedD = vehicle.DateCreated,
                               UserModified = userm == null ? null : userm.FirstName + " " + userm.LastName,
                               DateModifiedD = vehicle.DateModified
                           };
            return await vehicles.ToListAsync();
        }

        public async Task<Select2Model<JsonOptionsModel>> GetOptions(string search, int page)
        {
            var dateNow = DateTime.Now.Date;
            
            var vehicles = from vehicle in context.Vehicles
                       where vehicle.IsActive == true
                       && (vehicle.DateRegistered <= dateNow && vehicle.DateExpiration >= dateNow)
                       && (string.IsNullOrWhiteSpace(search) || vehicle.FleetPlateNumber.ToLower().Contains(search.ToLower()) || vehicle.VehicleType.ToLower().Contains(search.ToLower()))
                       select new JsonOptionsModel
                       {
                           Id = vehicle.Id.ToString(),
                           Text = vehicle.FleetPlateNumber //+" - "+ vehicle.VehicleType
                       };
            var filteredVehicles = await vehicles.Skip((page - 1) * 50).Take(50).ToListAsync();

            return new Select2Model<JsonOptionsModel>
            {
                items = filteredVehicles,
                total_count = filteredVehicles.Count()
            };
        }

        public async Task<ForDataTables<VehicleVM>> GetVehiclesDT(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetVehicles()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.FleetPlateNumber,
                    s.VehicleType,
                    s.DateRegistered,
                    s.DateExpiration,
                    s.IsActive + "",
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumn.ToLower().Contains("date"))
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "DateRegistered": data = data.OrderBy(s => s.DateRegisteredD); break;
                            case "DateExpiration": data = data.OrderBy(s => s.DateExpirationD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "DateRegistered": data = data.OrderByDescending(s => s.DateRegisteredD); break;
                            case "DateExpiration": data = data.OrderByDescending(s => s.DateExpirationD); break;
                            default: break;
                        }
                    }
                }
                else
                {
                    data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
                }
            }

            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<VehicleVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ResponseModel> AddVehicle(VehicleVM vehicle)
        {
            try
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                var newVehicle = new Vehicle
                {
                    FleetPlateNumber = vehicle.FleetPlateNumber,
                    VehicleType = vehicle.VehicleType,
                    DateRegistered = vehicle.DateRegisteredD,
                    DateExpiration = vehicle.DateExpirationD,
                    IsActive = true
                };
                await context.Vehicles.AddAsync(newVehicle);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully added new Vehicle" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> UpdateVehicle(VehicleVM vehicle)
        {
            try
            {
                var oldVehicle = await context.Vehicles
                                              .Where(s => s.Id == vehicle.Id)
                                              .FirstOrDefaultAsync();
                if (oldVehicle == null)
                    return new ResponseModel  { Status = "Error", Message = "Invalid Vehicle, failed to update" };

                if (Equals(oldVehicle.FleetPlateNumber, vehicle.FleetPlateNumber)
                 && Equals(oldVehicle.VehicleType, vehicle.VehicleType)
                 && Equals(oldVehicle.DateRegistered, vehicle.DateRegisteredD)
                 && Equals(oldVehicle.DateExpiration, vehicle.DateExpirationD))
                    return new ResponseModel { Status = "Error", Message = "Nothing to update" };

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                oldVehicle.FleetPlateNumber = vehicle.FleetPlateNumber;
                oldVehicle.VehicleType = vehicle.VehicleType;
                oldVehicle.DateRegistered = vehicle.DateRegisteredD;
                oldVehicle.DateExpiration = vehicle.DateExpirationD;

                context.Vehicles.Update(oldVehicle);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully updated Vehicle" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DeleteOrDeactivateVehicle(int? Id)
        {
            try
            {
                var oldVehicle = await context.Vehicles
                                              .Where(s => s.Id == Id)
                                              .FirstOrDefaultAsync();
                if (oldVehicle == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid Vehicle, failed to reactivate" };

                if (oldVehicle.IsActive == false)
                    return new ResponseModel { Status = "Error", Message = "Vehicle already inactive" };

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);


                //if (await context.SalesOrderDeliveryMonitorings.AnyAsync(s => s.VehicleId == Id))
                if (false)
                {
                    // is in use by other record
                    oldVehicle.IsActive = false;
                    context.Vehicles.Update(oldVehicle);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Successfully deactivated Vehicle" };
                }
                else
                {
                    context.Vehicles.Remove(oldVehicle);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Successfully deleted Vehicle" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> ReactivateVehicle(int? Id)
        {
            try
            {
                var oldVehicle = await context.Vehicles
                                             .Where(s => s.Id == Id)
                                             .FirstOrDefaultAsync();
                if (oldVehicle == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid Vehicle, failed to reactivate" };

                if (oldVehicle.IsActive)
                    return new ResponseModel { Status = "Error", Message = "Vehicle already active" };

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                oldVehicle.IsActive = true;

                context.Vehicles.Update(oldVehicle);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully reactivate Vehicle" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }
    }
}