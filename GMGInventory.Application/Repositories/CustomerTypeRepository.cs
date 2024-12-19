using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class CustomerTypeRepository : GenericRepository<CustomerType>, ICustomerTypeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CustomerTypeRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var ctype = await context.CustomerTypes
                                     .Where(s => s.Name.ToLower() == Name.ToLower())
                                     .FirstOrDefaultAsync();
            return ctype?.Id;
        }
        public async Task<IList<CustomerTypeVM>> GetAllCustomerTypeVMAsync()
        {
            var customertype = await context.CustomerTypes.ToListAsync();
            var customertypeVM = mapper.Map<List<CustomerTypeVM>>(customertype);
            foreach (CustomerTypeVM cvm in customertypeVM)
            {
                cvm.UserCreated = await GetNickname(cvm.UserCreated);
                cvm.UserModified = await GetNickname(cvm.UserModified);
            }
            return customertypeVM;
        }

        public async Task<CustomerTypeVM?> GetCustomerTypeVMAsync(int? id)
        {
            var customertype = await context.CustomerTypes.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (customertype == null)
            {
                return null;
            }
            var customertypeVM = mapper.Map<CustomerTypeVM>(customertype);
            customertypeVM.UserCreated = await GetNickname(customertypeVM.UserCreated);
            customertypeVM.UserModified = await GetNickname(customertypeVM.UserModified);
            return customertypeVM;
        }

        public async Task<IList<CustomerType>> GetAllActiveCustomerTypeAsync(int includeId)
        {
            //return await context.Regions.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            var customertype = await context.CustomerTypes.ToListAsync();
            return customertype;
        }

        public async Task<bool> IsCustomerTypeCodeExist(string code)
        {
            return await context.CustomerTypes.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsCustomerTypeInUse(int id)
        {
            return await context.Customers.AnyAsync(s => s.CustomerTypeId == id);
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.CustomerTypes
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }
    }
}