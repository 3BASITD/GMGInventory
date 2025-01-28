using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Select2Model<JsonNameOptionsModel>> GetOptions2(string search, int page)
        {
            var agents = await context.Agents
                                      .Where(s => string.IsNullOrWhiteSpace(search)
                                               || s.Code.ToLower().Contains(search.ToLower())
                                               || ((s.FirstName + (s.MiddleName == null ? "" : " " + s.MiddleName) + " " + s.LastName).ToLower().Contains(search.ToLower())))
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
                             MiddleName = agent.MiddleName ?? "-",
                             LastName = agent.LastName ?? "-",
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
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.FirstName.ToLower().Contains(form.SearchValue)
                                    || x.MiddleName.ToLower().Contains(form.SearchValue)
                                    || x.LastName.ToLower().Contains(form.SearchValue)
                                    || x.Area.ToLower().Contains(form.SearchValue)
                                    || x.UserCreated.ToLower().Contains(form.SearchValue)
                                    || x.DateCreated.ToString().ToLower().Contains(form.SearchValue)
                                    || x.UserModified.ToLower().ToLower().Contains(form.SearchValue)
                                    || x.DateModified.ToString().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "FirstName": data = data.OrderBy(s => s.FirstName); break;
                        case "MiddleName": data = data.OrderBy(s => s.MiddleName); break;
                        case "LastName": data = data.OrderBy(s => s.LastName); break;
                        case "Area": data = data.OrderBy(s => s.Area); break;
                        case "UserCreated": data = data.OrderBy(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderBy(s => s.DateCreatedD); break;
                        case "UserModified": data = data.OrderBy(s => s.UserModified); break;
                        case "DateModified": data = data.OrderBy(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "FirstName": data = data.OrderByDescending(s => s.FirstName); break;
                        case "MiddleName": data = data.OrderByDescending(s => s.MiddleName); break;
                        case "LastName": data = data.OrderByDescending(s => s.LastName); break;
                        case "Area": data = data.OrderByDescending(s => s.Area); break;
                        case "UserCreated": data = data.OrderByDescending(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateCreatedD); break;
                        case "UserModified": data = data.OrderByDescending(s => s.UserModified); break;
                        case "DateModified": data = data.OrderByDescending(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
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
                data = data.Where(x => x.Code.ToLower().Contains(form.SearchValue)
                                    || x.FirstName.ToLower().Contains(form.SearchValue)
                                    || x.MiddleName.ToLower().Contains(form.SearchValue)
                                    || x.LastName.ToLower().Contains(form.SearchValue)
                                    || x.Area.ToLower().Contains(form.SearchValue)
                                    || x.UserCreated.ToLower().Contains(form.SearchValue)
                                    || x.DateCreated.ToString().ToLower().Contains(form.SearchValue)
                                    || x.UserModified.ToLower().ToLower().Contains(form.SearchValue)
                                    || x.DateModified.ToString().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderBy(s => s.Code); break;
                        case "FirstName": data = data.OrderBy(s => s.FirstName); break;
                        case "MiddleName": data = data.OrderBy(s => s.MiddleName); break;
                        case "LastName": data = data.OrderBy(s => s.LastName); break;
                        case "Area": data = data.OrderBy(s => s.Area); break;
                        case "UserCreated": data = data.OrderBy(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderBy(s => s.DateCreatedD); break;
                        case "UserModified": data = data.OrderBy(s => s.UserModified); break;
                        case "DateModified": data = data.OrderBy(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Code": data = data.OrderByDescending(s => s.Code); break;
                        case "FirstName": data = data.OrderByDescending(s => s.FirstName); break;
                        case "MiddleName": data = data.OrderByDescending(s => s.MiddleName); break;
                        case "LastName": data = data.OrderByDescending(s => s.LastName); break;
                        case "Area": data = data.OrderByDescending(s => s.Area); break;
                        case "UserCreated": data = data.OrderByDescending(s => s.UserCreated); break;
                        case "DateCreated": data = data.OrderByDescending(s => s.DateCreatedD); break;
                        case "UserModified": data = data.OrderByDescending(s => s.UserModified); break;
                        case "DateModified": data = data.OrderByDescending(s => s.DateModifiedD); break;
                        default: break;
                    }
                }
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

        private async Task<IList<AgentVM>> GetAgentAsync(int id)
        {
            var agentVM = from agent in context.Agents
                          join cust in context.Customers
                                    on agent.Id equals cust.AgentId
                          where agent.Id == id
                          select new AgentVM
                          {
                              CustomerName = cust.Name,
                              CustomerAddress = cust.ShippingAddresses
                                                    .Where(s => s.IsDefault == true)
                                                    .Select(s => s.Address)
                                                    .FirstOrDefault() ?? "N/A",
                              CustomerEmail = cust.Email + "",
                              CustomerContactNo = cust.ShippingAddresses
                                                      .Where(s => s.IsDefault == true)
                                                      .Select(s => s.PhoneNumber)
                                                      .FirstOrDefault() ?? "N/A",
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
                data = data.Where(x => x.CustomerName.ToString().Contains(form.SearchValue)
                                    || x.CustomerAddress.ToString().Contains(form.SearchValue)
                                    || x.CustomerEmail.ToString().Contains(form.SearchValue)
                                    || x.CustomerContactNo.ToString().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        case "CustomerAddress": data = data.OrderBy(s => s.CustomerAddress); break;
                        case "CustomerEmail": data = data.OrderBy(s => s.CustomerEmail); break;
                        case "CustomerContactNo": data = data.OrderBy(s => s.CustomerContactNo); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)

                    {
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        case "CustomerAddress": data = data.OrderByDescending(s => s.CustomerAddress); break;
                        case "CustomerEmail": data = data.OrderByDescending(s => s.CustomerEmail); break;
                        case "CustomerContactNo": data = data.OrderByDescending(s => s.CustomerContactNo); break;
                        default: break;
                    }
                }
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
            var agentVM = (from cust in context.Agents
                                    .Include(s => s.Area)
                                    .Include(s => s.Customers)

                           where cust.Id == id
                           select new AgentVM
                           {
                               Id = cust.Id,
                               Code = cust.Code,
                           }).FirstOrDefault();
            return agentVM;
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
            return await context.Areas.AnyAsync(s => s.Id == id);
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