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
    public class ServiceLevelAgreementRepository : IServiceLevelAgreementRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public ServiceLevelAgreementRepository(ApplicationDbContext context
                                             , IHttpContextAccessor httpContextAccessor
                                             , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IList<ServiceLevelAgreementVM>> GetServiceLevelAgreements()
        {
            var serviceLevelAgreements = from serviceLevelAgreement in context.ServiceLevelAgreements
                           join userc in userManager.Users on serviceLevelAgreement.UserCreated equals userc.Id
                           join userm in userManager.Users on serviceLevelAgreement.UserModified equals userm.Id into m
                           from userm in m.DefaultIfEmpty()
                           select new ServiceLevelAgreementVM
                           {
                               Id = serviceLevelAgreement.Id,
                               Name = serviceLevelAgreement.Name,
                               PriorAvailabilityD = TimeSpan.FromTicks(serviceLevelAgreement.PriorAvailability),
                               IsActive = serviceLevelAgreement.IsActive,
                               UserCreated = userc.FirstName + " " + userc.LastName,
                               DateCreatedD = serviceLevelAgreement.DateCreated,
                               UserModified = userm == null ? null : userm.FirstName + " " + userm.LastName,
                               DateModifiedD = serviceLevelAgreement.DateModified
                           };
            return await serviceLevelAgreements.ToListAsync();
        }

        public async Task<ForDataTables<ServiceLevelAgreementVM>> GetServiceLevelAgreementsDT(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetServiceLevelAgreements()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Name,
                    s.PriorAvailability,
                    s.IsActive + "",
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
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<ServiceLevelAgreementVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ResponseModel> AddServiceLevelAgreement(ServiceLevelAgreementVM serviceLevelAgreement)
        {
            try
            {
                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                if(context.ServiceLevelAgreements.Any(s=>s.Name.ToLower() == serviceLevelAgreement.Name.ToLower()))
                    return new ResponseModel { Status = "Error", Message = "Name already exist"};

                var newServiceLevelAgreement = new ServiceLevelAgreement
                {
                    Name = serviceLevelAgreement.Name,
                    PriorAvailability = serviceLevelAgreement.PriorAvailabilityD.Ticks,
                    IsActive = true
                };
                await context.ServiceLevelAgreements.AddAsync(newServiceLevelAgreement);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully added new Service Level Agreement" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> UpdateServiceLevelAgreement(ServiceLevelAgreementVM serviceLevelAgreement)
        {
            try
            {
                var oldServiceLevelAgreement = await context.ServiceLevelAgreements
                                              .Where(s => s.Id == serviceLevelAgreement.Id)
                                              .FirstOrDefaultAsync();
                if (oldServiceLevelAgreement == null)
                    return new ResponseModel  { Status = "Error", Message = "Invalid Service Level Agreement, failed to update" };

                if (context.ServiceLevelAgreements.Any(s => s.Id != serviceLevelAgreement.Id && s.Name.ToLower() == serviceLevelAgreement.Name.ToLower()))
                    return new ResponseModel { Status = "Error", Message = "Name already exist" };

                if (Equals(oldServiceLevelAgreement.Name, serviceLevelAgreement.Name)
                 && Equals(oldServiceLevelAgreement.PriorAvailability, serviceLevelAgreement.PriorAvailabilityD.Ticks))
                    return new ResponseModel { Status = "Error", Message = "Nothing to update" };

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                oldServiceLevelAgreement.Name = serviceLevelAgreement.Name;
                oldServiceLevelAgreement.PriorAvailability = serviceLevelAgreement.PriorAvailabilityD.Ticks;

                context.ServiceLevelAgreements.Update(oldServiceLevelAgreement);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully updated Service Level Agreement" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> DeleteOrDeactivateServiceLevelAgreement(int? Id)
        {
            try
            {
                var oldServiceLevelAgreement = await context.ServiceLevelAgreements
                                                            .Where(s => s.Id == Id)
                                                            .FirstOrDefaultAsync();
                if (oldServiceLevelAgreement == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid Service Level Agreement, failed to reactivate" };

                if (oldServiceLevelAgreement.IsActive == false)
                    return new ResponseModel { Status = "Error", Message = "Service Level Agreement already inactive" };

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);


                //if (await context.SalesOrderDeliveryMonitorings.AnyAsync(s => s.ServiceLevelAgreementId == Id))
                if (false)
                {
                    // is in use by other record
                    oldServiceLevelAgreement.IsActive = false;
                    context.ServiceLevelAgreements.Update(oldServiceLevelAgreement);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Successfully deactivated Service Level Agreement" };
                }
                else
                {
                    context.ServiceLevelAgreements.Remove(oldServiceLevelAgreement);
                    await context.SaveChangesAsync(currentUserId);

                    return new ResponseModel { Status = "Success", Message = "Successfully deleted Service Level Agreement" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<ResponseModel> ReactivateServiceLevelAgreement(int? Id)
        {
            try
            {
                var oldServiceLevelAgreement = await context.ServiceLevelAgreements
                                                            .Where(s => s.Id == Id)
                                                            .FirstOrDefaultAsync();
                if (oldServiceLevelAgreement == null)
                    return new ResponseModel { Status = "Error", Message = "Invalid Service Level Agreement, failed to reactivate" };

                if (oldServiceLevelAgreement.IsActive)
                    return new ResponseModel { Status = "Error", Message = "Service Level Agreement already active" };

                string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                oldServiceLevelAgreement.IsActive = true;

                context.ServiceLevelAgreements.Update(oldServiceLevelAgreement);
                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully reactivate Service Level Agreement" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }
    }
}