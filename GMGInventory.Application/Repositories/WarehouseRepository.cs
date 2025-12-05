using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUser;

        public WarehouseRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUser = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<IList<JsonNameOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Warehouses
                                .Where(s => s.IsActive || s.Id == IncludeId)
                                .Select(s => new JsonNameOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Code.Trim(),
                                    Name = s.Name.Trim(),
                                    Selected = IncludeId != null && IncludeId == s.Id
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<WarehouseVM?> GetWarehouse(int Id)
        {
            var warehouse = from whouse in context.Warehouses
                             join userc in userManager.Users on whouse.UserCreated equals userc.Id
                             join userm in userManager.Users on whouse.UserModified equals userm.Id into um
                             from userm in um.DefaultIfEmpty()
                             select new WarehouseVM
                             {
                                 Id = whouse.Id,
                                 Code = whouse.Code,
                                 Name = whouse.Name,
                                 Address = whouse.Address,
                                 ContactPerson = whouse.ContactPerson,
                                 ContactNumber = whouse.ContactNumber,
                                 IsMain = whouse.IsMain == true,
                                 IsActive = whouse.IsActive,
                                 UserCreated = userc.FirstName + " " + userc.LastName,
                                 DateCreatedD = whouse.DateCreated,
                                 UserModified = userm == null ? null : (userm.FirstName + " " + userm.LastName),
                                 DateModifiedD = whouse.DateModified
                             };
            return await warehouse.FirstOrDefaultAsync();
        }

        private async Task<IList<WarehouseVM>> GetWarehouses()
        {
            var warehouses = from warehouse in context.Warehouses
                             join userc in userManager.Users on warehouse.UserCreated equals userc.Id
                             join userm in userManager.Users on warehouse.UserModified equals userm.Id into um
                             from userm in um.DefaultIfEmpty()
                             select new WarehouseVM
                             {
                                 Id = warehouse.Id,
                                 Code = warehouse.Code,
                                 Name = warehouse.Name,
                                 Address = warehouse.Address,
                                 ContactPerson = warehouse.ContactPerson,
                                 ContactNumber = warehouse.ContactNumber,
                                 IsMain = warehouse.IsMain == true,
                                 IsActive = warehouse.IsActive,
                                 UserCreated = userc.FirstName + " " + userc.LastName,
                                 DateCreatedD = warehouse.DateCreated,
                                 UserModified = userm == null ? null : (userm.FirstName + " " + userm.LastName),
                                 DateModifiedD = warehouse.DateModified
                             };
            return await warehouses.ToListAsync();
        }

        public async Task<ForDataTables<WarehouseVM>> GetWarehousesDT(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var data = (await GetWarehouses()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Code,
                    s.Name,
                    s.Address,
                    s.ContactPerson,
                    s.ContactNumber
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
            }

            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<WarehouseVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<IList<APIGeneric>> GetListAPI()
        {
            var warehouses = from warehouse in context.Warehouses
                             where warehouse.IsActive == true
                             select new APIGeneric
                             {
                                 Id = warehouse.Id,
                                 Code = warehouse.Code,
                                 Name = warehouse.Name
                             };
            return await warehouses.ToListAsync();
        }

        public async Task<ResponseModel> AddAsync(WarehouseVM warehouse)
        {
            var response = new ResponseModel();

            if (await context.Warehouses.AnyAsync(s=>s.Code == warehouse.Code))
            {
                response.Status = "Error";
                response.Message = "Code already in use";
            }
            else
            {
                var hasExistingMain = await context.Warehouses.Where(s => s.IsMain == true).CountAsync();
                var whouse = new Warehouse
                {
                    Code = warehouse.Code,
                    Name = warehouse.Name,
                    Address = warehouse.Address,
                    ContactPerson = warehouse.ContactPerson,
                    ContactNumber = warehouse.ContactNumber,
                    IsMain = hasExistingMain == 0,
                    IsActive = true
                };
                await context.Warehouses.AddAsync(whouse);
                await context.SaveChangesAsync(currentUser);

                response.Status = "Success";
                response.Message = "Successfully added a warehouse";
            }
            return response;
        }

        public async Task<ResponseModel> UpdateAsync(WarehouseVM warehouse)
        {
            var response = new ResponseModel();

            var whouse = await context.Warehouses.Where(s => s.Id == warehouse.Id).FirstOrDefaultAsync();

            if (whouse == null)
            {
                response.Status = "Error";
                response.Message = "Invalid warehouse";
            }
            else
            {

                whouse.Code = warehouse.Code;
                whouse.Name = warehouse.Name;
                whouse.Address = warehouse.Address;
                whouse.ContactPerson = warehouse.ContactPerson;
                whouse.ContactNumber = warehouse.ContactNumber;
                whouse.IsActive = true;

                context.Warehouses.Update(whouse);
                await context.SaveChangesAsync(currentUser);

                response.Status = "Success";
                response.Message = "Successfully updated warehouse";
            }
            return response;
        }

        public async Task<ResponseModel> DeleteDeactivateAsync(int Id)
        {
            var response = new ResponseModel();

            var whouse = await context.Warehouses.Where(s => s.Id == Id).FirstOrDefaultAsync();

            if (whouse == null)
            {
                response.Status = "Error";
                response.Message = "Invalid warehouse";
            }
            else
            {
                if (await context.ItemInventories.AnyAsync(s => s.WarehouseId == Id)
                 || await context.PhysicalCountHistories.AnyAsync(s => s.WarehouseId == Id)
                 || await context.PurchaseOrderNews.AnyAsync(s => s.DeliveryAddressId == Id)
                 || await context.TransferItems.AnyAsync(s => s.SourceId == Id)
                 || await context.TransferItems.AnyAsync(s => s.DestinationId == Id))
                {
                    whouse.IsActive = false;

                    context.Warehouses.Update(whouse);
                    await context.SaveChangesAsync(currentUser);

                    response.Status = "Success";
                    response.Message = "Successfully deactivated warehouse";
                } else
                {
                    context.Warehouses.Remove(whouse);
                    await context.SaveChangesAsync(currentUser);

                    response.Status = "Success";
                    response.Message = "Successfully deleted warehouse";
                }
            }
            return response;
        }

        public async Task<ResponseModel> ReactivateAsync(int Id)
        {
            var response = new ResponseModel();

            var whouse = await context.Warehouses.Where(s => s.Id == Id).FirstOrDefaultAsync();

            if (whouse == null)
            {
                response.Status = "Error";
                response.Message = "Invalid warehouse";
            }
            else
            {
                whouse.IsActive = true;

                context.Warehouses.Update(whouse);
                await context.SaveChangesAsync(currentUser);

                response.Status = "Success";
                response.Message = "Successfully activated warehouse";
            }
            return response;
        }

        public async Task<ResponseModel> SetMainAsync(int id)
        {
            var response = new ResponseModel();
            var warehouse = await context.Warehouses.Where(s => s.Id == id).FirstOrDefaultAsync();
            if(warehouse == null)
            {
                response.Status = "Error";
                response.Message = "Invalid warehouse";
            } else
            {
                var previous = await context.Warehouses.Where(s=>s.IsMain == true).FirstOrDefaultAsync();
                if(previous != null)
                {
                    previous.IsMain = false;
                    context.Warehouses.Update(previous);
                }

                warehouse.IsMain = true;
                context.Warehouses.Update(warehouse);
                await context.SaveChangesAsync(currentUser);

                response.Status = "Success";
                response.Message = warehouse.Name + "is set as main warehouse";
            }
            return response;
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var warehouse = await context.Warehouses
                                         .Where(s => s.Name.ToLower().Replace("\n", "").Replace("\r", "").Trim() == (Name + "").ToLower().Replace("\n", "").Replace("\r", "").Trim())
                                         .FirstOrDefaultAsync();
            return warehouse?.Id;
        }
        public async Task<IList<Warehouse>> GetAllActiveWarehouseAsync(int includeId)
        {
            return await context.Warehouses.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }
    }
}