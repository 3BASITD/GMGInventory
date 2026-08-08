using AutoMapper;
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
    public class AgentRepository : GenericRepository<Agent>, IAgentRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AgentRepository(ApplicationDbContext context
             , IHttpContextAccessor httpContextAccessor
             , UserManager<User> userManager
             , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page, int? customer)
        {
            var agents = context.Agents
                    .Where(a => a.IsActive
                        && (string.IsNullOrWhiteSpace(search)
                            || a.Code.ToLower().Contains(search.ToLower())
                            || ((a.FirstName + (a.MiddleName == null ? "" : " " + a.MiddleName) + " " + a.LastName)
                                .ToLower().Contains(search.ToLower())))
                        && (customer == null || context.Customers.Any(c => c.AgentId == a.Id && c.Id == customer)))
                    .OrderBy(a => a.Code)
                    .Select(a => new JsonNameOptionsModel
                    {
                        Id = a.Id.ToString(),
                        Text = a.Code,
                        Name = a.FirstName + (a.MiddleName == null ? "" : " " + a.MiddleName) + " " + a.LastName,
                    });

            var totalCount = await agents.CountAsync();

            var agentsFiltered = await agents
                .Skip((page - 1) * 50)
                .Take(50)
                .ToListAsync();

            return new Select2Model<JsonNameOptionsModel>
            {
                items = agentsFiltered,
                total_count = totalCount
            };
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Agents
                .Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.FirstName + (s.MiddleName == null ? "" : " " + s.MiddleName) + " " + s.LastName,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }

        public async Task<Select2Model<JsonNameOptionsModel>> GetOptionsWithOrder(string search, int page, int? CustomerId, DateTime? DateFrom, DateTime? DateTo)
        {
            var dtFrom = DateFrom ?? DateTime.MinValue;
            var dtTo = DateTo ?? DateTime.MaxValue;
            var agentsInInvoices = from inv in context.Invoices
                                   where (CustomerId == null || inv.SalesOrder.CustomerId == CustomerId)
                                   && dtFrom.Date <= inv.InvoiceDate.Date
                                   && inv.InvoiceDate.Date <= dtTo.Date
                                   select inv.SalesOrder.AgentId;
            var agents = await context.Agents
                                      .Where(s => (string.IsNullOrWhiteSpace(search)
                                               || (s.Code.ToLower().Contains(search.ToLower()))
                                               || ((s.FirstName + (s.MiddleName == null ? "" : " " + s.MiddleName) + " " + s.LastName).ToLower().Contains(search.ToLower())))
                                               && agentsInInvoices.Contains(s.Id))
                                      .Select(s => new JsonNameOptionsModel
                                      {
                                          Id = s.Id.ToString(),
                                          Text = s.Code,
                                          Name = s.FirstName + (s.MiddleName == null ? "" : " " + s.MiddleName) + " " + s.LastName,
                                      })
                                      .OrderBy(s => s.Text)
                                      .ToListAsync();
            var agentsFilterd = agents.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonNameOptionsModel>
            {
                //incomplete_results = false,
                items = agentsFilterd,
                total_count = agents.Count
            };
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var agent = await context.Agents.Where(s => s.FirstName + (s.MiddleName == null ? "" : " " + s.MiddleName) + " " + s.LastName == Name).FirstOrDefaultAsync();
            return agent?.Id;
        }

        public async Task<IList<AgentViewVM>> GetAllAsync(AgentFilterVM filter)
        {

            var agents = from agent in context.Agents
                         join areas in context.Areas
                                    on agent.AreaId equals areas.Id
                         join regions in context.Regions
                                    on areas.RegionId equals regions.Id
                         join userc in userManager.Users on agent.UserCreated equals userc.Id
                         join userm in userManager.Users on agent.UserModified equals userm.Id into m
                         from userm in m.DefaultIfEmpty()
                         where ((filter.RegionId == null || filter.RegionId == regions.Id))

                         select new AgentViewVM
                         {
                             Id = agent.Id,
                             FirstName = agent.FirstName,
                             MiddleName = agent.MiddleName,
                             LastName = agent.LastName,
                             Area = agent.Area.Name,
                             AreaId = agent.Area.Id,
                             Code = agent.Code,
                             IsActive = agent.IsActive,
                             DateCreatedD = agent.DateCreated,
                             DateModifiedD = agent.DateModified,
                             UserCreated = userc.FirstName + " " + userc.LastName,
                             UserModified = userm == null ? null : userc.FirstName + " " + userc.LastName
                         };
            return await agents.ToListAsync();
        }

        public async Task<ForDataTables<AgentViewVM>> GetAllForDataTablesAsync(DataTablesRequestForm form, AgentFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAllAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Code,
                    s.FirstName,
                    s.MiddleName,
                    s.LastName,
                    s.Area,
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
            return new ForDataTables<AgentViewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<AgentViewVM>> GetListAsync()
        {
            var agents = from agent in context.Agents
                         join areas in context.Areas
                                    on agent.AreaId equals areas.Id
                         join regions in context.Regions
                                    on areas.RegionId equals regions.Id
                         join userc in userManager.Users on agent.UserCreated equals userc.Id
                         join userm in userManager.Users on agent.UserModified equals userm.Id into m
                         from userm in m.DefaultIfEmpty()
                         select new AgentViewVM
                         {
                             Id = agent.Id,
                             FirstName = agent.FirstName,
                             MiddleName = agent.MiddleName + "",
                             LastName = agent.LastName,
                             Area = agent.Area.Name,
                             AreaId = agent.Area.Id,
                             Code = agent.Code,
                             IsActive = agent.IsActive,
                             DateCreatedD = agent.DateCreated,
                             DateModifiedD = agent.DateModified,
                             UserCreated = SystemFormat.Name(userc.FirstName, userc.LastName, userc.Nickname),
                             UserModified = userm == null ? "-" : SystemFormat.Name(userm.FirstName, userm.LastName, userm.Nickname)
                         };
            return await agents.ToListAsync();
        }

        public async Task<ForDataTables<AgentViewVM>> GetListDTAsync(DataTablesRequestForm form)
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
                    s.FirstName,
                    s.MiddleName,
                    s.LastName,
                    s.Area,
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
            return new ForDataTables<AgentViewVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<IList<APIAgent>> GetListAPI(int? AreaId)
        {
            var agents = from agent in context.Agents
                         where agent.IsActive == true
                         && (AreaId == null || AreaId == agent.AreaId)
                         select new APIAgent
                         {
                             Id = agent.Id,
                             Code = agent.Code,
                             FirstName = agent.FirstName,
                             MiddleName = agent.MiddleName,
                             LastName = agent.LastName,
                             AreaCode = agent.Area.Code.Replace("\r\n", "")
                         };
            return await agents.ToListAsync();
        }

        private async Task<IList<AgentVM>> GetAgentAsync(int id)
        {
            var agentVM = from agent in context.Agents
                          join cust in context.Customers
                                    on agent.Id equals cust.AgentId
                          where agent.Id == id
                          select new AgentVM
                          {
                              CustomerId = cust.Id,
                              CustomerName = cust.Name,
                              CustomerAddress = cust.ShippingAddresses
                                                    .Where(s => s.IsDefault == true)
                                                    .Select(s => s.Address)
                                                    .FirstOrDefault() ?? "",
                              CustomerEmail = cust.Email + "",
                              CustomerContactNo = cust.ShippingAddresses
                                                      .Where(s => s.IsDefault == true)
                                                      .Select(s => s.PhoneNumber)
                                                      .FirstOrDefault() ?? "",
                          };
            return await agentVM.ToListAsync();
        }

        public async Task<ForDataTables<AgentVM>> GetAgentDTAsync(DataTablesRequestForm form, int id)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetAgentAsync(id)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.CustomerName,
                    s.CustomerAddress,
                    s.CustomerEmail,
                    s.CustomerContactNo
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
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<AgentVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public AgentVM? GetAgentDtlVM(int? id)
        {
            var agentVM = (from agnt in context.Agents
                           join userc in userManager.Users on agnt.UserCreated equals userc.Id
                           join userm in userManager.Users on agnt.UserModified equals userm.Id into um
                           from userm in um.DefaultIfEmpty()
                           where agnt.Id == id
                           select new AgentVM
                           {
                               Id = agnt.Id,
                               Code = agnt.Code,
                               FirstName = agnt.FirstName,
                               MiddleName = agnt.MiddleName,
                               LastName = agnt.LastName,
                               AreaId = agnt.AreaId,
                               Area = agnt.Area.Name,
                               IsActive = agnt.IsActive,
                               UserCreated = userc.FirstName + " " + userc.LastName,
                               DateCreatedD = agnt.DateCreated,
                               UserModified = userm == null ? null : userm.FirstName + " " + userm.LastName,
                               DateModifiedD = agnt.DateModified
                           }).FirstOrDefault();
            return agentVM;
            //var agent = await context.Agents.Include(s => s.Area).Where(s => s.Id == id).FirstOrDefaultAsync();
            //if (agent == null)
            //{
            //    return null;
            //}
            //var agentViewVM = mapper.Map<AgentVM>(agent);
            //return agentViewVM;
        }

        public async Task<AgentViewVM?> GetAgentViewVM(int? id)
        {
            var agentVM = (await GetListAsync()).Where(s => s.Id == id).FirstOrDefault();
            if (agentVM == null)
            {
                return null;
            }
            return agentVM;
        }

        public async Task<bool> UploadCombinationExistAsync(AgentVM agentVM)
        {
            return (await GetListAsync()).Any(s => s.Code == agentVM.Code && s.FirstName == agentVM.FirstName &&
            s.MiddleName == agentVM.MiddleName && s.LastName == agentVM.LastName && s.AreaId == agentVM.AreaId);
        }

        public async Task<AgentVM?> GetAgentVMAsync(int? id)
        {
            var agent = await context.Agents.Include(s => s.Area).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (agent == null)
            {
                return null;
            }
            var agentViewVM = mapper.Map<AgentVM>(agent);
            return agentViewVM;
        }

        public async Task<IList<Agent>> GetAllActiveAgentAsync(int includeId)
        {
            return await context.Agents.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsCodeExist(string code)
        {
            return await context.Agents.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsAgentInUse(int id)
        {
            return await context.Customers.AnyAsync(s => s.AgentId == id)
                || await context.SalesOrders.AnyAsync(s => s.AgentId == id);
        }

        public async Task<bool> IsAgentIdExistAsync(int id)
        {
            return await context.Agents.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateAgentAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var agent = await context.Agents.FirstAsync(s => s.Id == id);
            agent.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateAgentAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var agent = await context.Agents.FirstAsync(s => s.Id == id);
            agent.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(AgentVM agentVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

            var agent = new Agent
            {
                Code = agentVM.Code,
                FirstName = agentVM.FirstName,
                MiddleName = agentVM.MiddleName,
                LastName = agentVM.LastName,
                AreaId = agentVM.AreaId,
                IsActive = true
            };
            await context.Agents.AddAsync(agent);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(AgentVM agentVM)
        {
            var agent = context.Agents.Where(s => s.Id == agentVM.Id).FirstOrDefault();
            if (agent == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            agent.Id = agentVM.Id;
            agent.Code = agentVM.Code;
            agent.FirstName = agentVM.FirstName;
            agent.MiddleName = agentVM.MiddleName;
            agent.LastName = agentVM.LastName;
            agent.AreaId = agentVM.AreaId;
            agent.IsActive = true;

            context.Agents.Update(agent);
            await context.SaveChangesAsync(currentUserId);
        }




    }
}