using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using static Humanizer.On;

namespace GMGInventory.Application.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public DriverRepository(ApplicationDbContext context
                               , IHttpContextAccessor httpContextAccessor
                               , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            var dateNow = DateTime.Now.Date;

            var drivers = from driver in context.Drivers
                          where driver.IsActive == true
                          select new JsonOptionsModel
                          {
                              Id = driver.Id.ToString(),
                              Text = driver.LastName + ", " + driver.FirstName
                          };
            return await drivers.ToListAsync();
        }

        public async Task<Select2Model<JsonOptionsModel>> GetOptions2(string search, int page)
        {
            var dateNow = DateTime.Now.Date;

            var drivers = from driver in context.Drivers
                          where driver.IsActive == true
                          && (string.IsNullOrWhiteSpace(search) || driver.FirstName.ToLower().Contains(search.ToLower()) || driver.Nickname.ToLower().Contains(search.ToLower()) || driver.FirstName.ToLower().Contains(search.ToLower()))
                          select new JsonOptionsModel
                          {
                              Id = driver.Id.ToString(),
                              Text = driver.LastName + ", " + driver.FirstName
                          };
            var filteredDrivers = await drivers.Skip((page - 1) * 50).Take(50).ToListAsync();

            return new Select2Model<JsonOptionsModel>
            {
                items = filteredDrivers,
                total_count = filteredDrivers.Count()
            };
        }

        public async Task<IList<DriverVM>> GetDrivers()
        {
            var drivers = from driver in context.Drivers
                          join userc in userManager.Users on driver.UserCreated equals userc.Id
                          join userm in userManager.Users on driver.UserModified equals userm.Id into m
                          from userm in m.DefaultIfEmpty()
                          select new DriverVM
                          {
                              Id = driver.Id,
                              FirstName = driver.FirstName,
                              LastName = driver.LastName,
                              Nickname = driver.Nickname,
                              LicenseNumber = driver.LicenseNumber,
                              Restriction = driver.Restriction,
                              LicenseDateExpirationD = driver.LicenseDateExpiration,
                              IsActive = driver.IsActive,
                              UserCreated = userc.FirstName + " " + userc.LastName,
                              DateCreatedD = driver.DateCreated,
                              UserModified = userm == null ? null : userm.FirstName + " " + userm.LastName,
                              DateModifiedD = driver.DateModified
                          };
            return await drivers.ToListAsync();
        }

        public async Task<ForDataTables<DriverVM>> GetDriversDT(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetDrivers()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.FirstName,
                    s.LastName,
                    s.Nickname,
                    s.LicenseNumber,
                    s.Restriction,
                    s.LicenseDateExpiration,
                    s.IsActive + "",
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumn.ToLower().Contains("date") || form.SortColumn.ToLower() == "name")
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "LicenseDateExpiration": data = data.OrderBy(s => s.LicenseDateExpirationD); break;
                            case "Name": data = data.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ThenBy(s => s.Nickname); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "LicenseDateExpiration": data = data.OrderByDescending(s => s.LicenseDateExpirationD); break;
                            case "Name": data = data.OrderByDescending(s => s.LastName).ThenByDescending(s => s.FirstName).ThenByDescending(s => s.Nickname); break;
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
            return new ForDataTables<DriverVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<APIDriver>> GetListAPI()
        {
            var drivers = from driver in context.Drivers
                          where driver.IsActive == true
                          select new APIDriver
                          {
                              Id = driver.Id,
                              FirstName = driver.FirstName,
                              LastName = driver.LastName,
                              Nickname = driver.Nickname,
                              LicenseNumber = driver.LicenseNumber,
                              Restriction = driver.Restriction,
                              LicenseDateExpiration = driver.LicenseDateExpiration
                          };
            return await drivers.ToListAsync();
        }

        public async Task<ResponseModel> AddDriver(DriverVM driver)
        {
            try
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                var newDriver = new Driver
                {
                    FirstName = driver.FirstName,
                    LastName = driver.LastName,
                    Nickname = driver.Nickname,
                    LicenseNumber = driver.LicenseNumber,
                    Restriction = driver.Restriction,
                    LicenseDateExpiration = driver.LicenseDateExpirationD,
                    IsActive = true
                };
                await context.Drivers.AddAsync(newDriver);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully added new driver" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> UpdateDriver(DriverVM driver)
        {
            try
            {
                var oldDriver = await context.Drivers
                                             .Where(s => s.Id == driver.Id)
                                             .FirstOrDefaultAsync();
                if (oldDriver == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid driver, failed to update" };

                if (Equals(oldDriver.FirstName, driver.FirstName)
                 && Equals(oldDriver.LastName, driver.LastName)
                 && Equals(oldDriver.Nickname, driver.Nickname)
                 && Equals(oldDriver.LicenseNumber, driver.LicenseNumber)
                 && Equals(oldDriver.Restriction, driver.Restriction)
                 && Equals(oldDriver.LicenseDateExpiration, driver.LicenseDateExpirationD))
                    return new ResponseModel { Status = "Error", Message = "Nothing to update" };

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                oldDriver.FirstName = driver.FirstName;
                oldDriver.LastName = driver.LastName;
                oldDriver.Nickname = driver.Nickname;
                oldDriver.LicenseNumber = driver.LicenseNumber;
                oldDriver.Restriction = driver.Restriction;
                oldDriver.LicenseDateExpiration = driver.LicenseDateExpirationD;

                context.Drivers.Update(oldDriver);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully updated driver" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DeleteOrDeactivateDriver(int? Id)
        {
            try
            {
                var oldDriver = await context.Drivers
                                             .Where(s => s.Id == Id)
                                             .FirstOrDefaultAsync();
                if (oldDriver == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid record, driver cannot be found" };

                if (oldDriver.IsActive == false)
                    return new ResponseModel { Status = "Error", Message = "Driver already inactive" };

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);


                if (await context.SalesOrderDeliveries.AnyAsync(s => s.DriverId == Id))
                {
                    oldDriver.IsActive = false;
                    context.Drivers.Update(oldDriver);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Successfully deactivated driver" };
                }
                else
                {
                    context.Drivers.Remove(oldDriver);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Successfully deleted driver" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> ReactivateDriver(int? Id)
        {
            try
            {
                var oldDriver = await context.Drivers
                                             .Where(s => s.Id == Id)
                                             .FirstOrDefaultAsync();
                if (oldDriver == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid record, driver cannot be found" };

                if (oldDriver.IsActive)
                    return new ResponseModel { Status = "Error", Message = "Driver already active" };

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                oldDriver.IsActive = true;

                context.Drivers.Update(oldDriver);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully reactivate driver" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }
    }
}