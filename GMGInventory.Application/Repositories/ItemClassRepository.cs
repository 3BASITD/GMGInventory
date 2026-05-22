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
    public class ItemClassRepository : IItemClassRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public ItemClassRepository(ApplicationDbContext context
             , IHttpContextAccessor httpContextAccessor
             , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.ItemClasses
                                .Where(s => s.IsActive || s.Id == IncludeId)
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = (s.Code == s.Name ? "" : "(" + s.Code + ") ") + s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }

        public async Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page)
        {
            var itemClasses = await context.ItemClasses
                                           .Where(s => s.IsActive
                                                    && (string.IsNullOrWhiteSpace(search)
                                                    || (s.Code.ToLower().Contains(search.ToLower()))
                                                    || (s.Name.ToLower().Contains(search.ToLower()))))
                                           .Select(s => new JsonNameOptionsModel
                                           {
                                               Id = s.Id.ToString(),
                                               Text = s.Code,
                                               Name = s.Name
                                           })
                                           .OrderBy(s => s.Text)
                                           .ToListAsync();
            var itemClassesFiltered = itemClasses.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonNameOptionsModel>
            {
                //incomplete_results = false,
                items = itemClassesFiltered,
                total_count = itemClasses.Count
            };
        }

        public async Task<IList<GenericAuditVM>> GetListAsync()
        {
            var iclasses = from iclass in context.ItemClasses
                           join userc in userManager.Users on iclass.UserCreated equals userc.Id
                           join userm in userManager.Users on iclass.UserModified equals userm.Id into m
                           from userm in m.DefaultIfEmpty()
                           select new GenericAuditVM
                           {
                               Id = iclass.Id,
                               Code = iclass.Code,
                               DateCreatedD = iclass.DateCreated,
                               DateModifiedD = iclass.DateModified,
                               IsActive = iclass.IsActive,
                               Name = iclass.Name,
                               UserCreated = userc.FirstName + " " + userc.LastName,
                               UserModified = userm == null ? null : userm.FirstName + " " + userm.LastName
                           };
            return await iclasses.ToListAsync();
        }

        public async Task<ForDataTables<GenericAuditVM>> GetListDTAsync(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Code,
                    s.Name,
                    s.UserCreated,
                    s.DateCreated,
                    s.UserModified,
                    s.DateModified
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "DateCreated" => isAsc ? data.OrderBy(s => s.DateCreatedD) : data.OrderByDescending(s => s.DateCreatedD),
                    "DateModified" => isAsc ? data.OrderBy(s => s.DateModifiedD) : data.OrderByDescending(s => s.DateModifiedD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<GenericAuditVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ResponseModel> AddAsync(GenericAuditVM iclassVM)
        {
            try
            {
                var iclass = new ItemClass
                {
                    Code = iclassVM.Code,
                    Name = iclassVM.Name,
                    IsActive = true
                };
                await context.ItemClasses.AddAsync(iclass);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Item Class successfully added" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> UpdateAsync(GenericAuditVM iclassVM)
        {
            try
            {
                var iclass = await context.ItemClasses
                                          .Where(s => s.Id == iclassVM.Id)
                                          .FirstOrDefaultAsync();
                if (iclass == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid record. Item class cannot be found" };

                iclass.Code = iclassVM.Code;
                iclass.Name = iclassVM.Name;

                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully updated item class" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DeleteOrDeactivateAsync(int? Id)
        {
            try
            {
                var iclass = await context.ItemClasses
                                          .Where(s => s.Id == Id)
                                          .FirstOrDefaultAsync();
                if (iclass == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid record. Item class cannot be found" };

                if (iclass.IsActive == false)
                    return new ResponseModel { Status = "Error", Message = "Item class already inactive" };

                if (await context.Items.AnyAsync(s => s.ItemClassId == Id))
                {
                    iclass.IsActive = false;
                    context.ItemClasses.Update(iclass);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Successfully deactivated item class" };
                }
                else
                {
                    context.ItemClasses.Remove(iclass);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Successfully deleted item class" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> ReactivateAsync(int? Id)
        {
            try
            {
                var iclass = await context.ItemClasses
                                          .Where(s => s.Id == Id)
                                          .FirstOrDefaultAsync();
                if (iclass == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid record. Item class cannot be found" };

                if (iclass.IsActive)
                    return new ResponseModel { Status = "Error", Message = "Item class already active" };

                iclass.IsActive = true;

                context.ItemClasses.Update(iclass);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully reactivate item class" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }
    }
}