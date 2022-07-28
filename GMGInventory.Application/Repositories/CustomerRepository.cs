using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CustomerRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<CustomerViewVM>> GetAllCustomerVMAsync()
        {
            var customers = await context.Customers
                .Include(s => s.CreditTerm)
                .Include(s => s.CustomerType)
                .Include(s => s.Agent)
                .Include(s => s.Area)
                .ToListAsync();
            var customersViewVM = mapper.Map<List<CustomerViewVM>>(customers);
            foreach (CustomerViewVM ivm in customersViewVM)
            {
                ivm.UserCreated = await GetUserName(ivm.UserCreated);
                ivm.UserModified = await GetUserName(ivm.UserModified);
            }
            return customersViewVM;
        }

        public async Task<CustomerViewVM?> GetCustomerViewVMAsync(int? id)
        {
            var customers = await context.Customers
                .Include(s => s.CreditTerm)
                .Include(s => s.CustomerType)
                .Include(s => s.Agent)
                .Include(s => s.Area)
                .Where(s => s.Id == id).FirstOrDefaultAsync();
            if (customers == null)
            {
                return null;
            }
            var customersViewVM = mapper.Map<CustomerViewVM>(customers);
            customersViewVM.UserCreated = await GetUserName(customersViewVM.UserCreated);
            customersViewVM.UserModified = await GetUserName(customersViewVM.UserModified);
            return customersViewVM;
        }

        public async Task<CustomerVM?> GetCustomerVMAsync(int? id)
        {
            var customers = await context.Customers
                .Include(s => s.CreditTerm)
                .Include(s => s.CustomerType)
                .Include(s => s.Agent)
                .Include(s => s.Area)
                .Where(s => s.Id == id).FirstOrDefaultAsync();
            if (customers == null)
            {
                return null;
            }
            var customersViewVM = mapper.Map<CustomerVM>(customers);
            return customersViewVM;
        }

        public async Task<List<Customer>> GetAllActiveCustomerAsync(int includeId)
        {
            return await context.Customers.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            //var areas = await context.Areas.ToListAsync();
            //return areas;
        }

        public async Task<bool> IsCustomerCodeExist(string code)
        {
            return await context.Customers.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsCustomerInUse(int id)
        {
            return await context.Areas.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> IsCustomerIdExistAsync(int id)
        {
            return await context.Customers.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateCustomerAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var customers = await context.Customers.FirstAsync(s => s.Id == id);
            customers.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateCustomerAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var customers = await context.Customers.FirstAsync(s => s.Id == id);
            customers.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(CustomerVM customerVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var customers = mapper.Map<Customer>(customerVM);
            customers.IsActive = true;
            await context.Customers.AddAsync(customers);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(int id, CustomerVM customerVM)
        {
            var customers = context.Customers.Where(s => s.Id == id).FirstOrDefault();
            if (customers == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            mapper.Map(customerVM, customers);
            context.Customers.Update(customers);
            await context.SaveChangesAsync(currentUserId);
        }
    }
}