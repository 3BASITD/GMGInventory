using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public SupplierRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<SupplierVM>> GetAllSupplierVMAsync()
        {
            var supplier = await context.Suppliers.ToListAsync();
            var supplierVM = mapper.Map<List<SupplierVM>>(supplier);
            foreach (SupplierVM cvm in supplierVM)
            {
                cvm.UserCreated = await GetUserName(cvm.UserCreated);
                cvm.UserModified = await GetUserName(cvm.UserModified);
            }
            return supplierVM;
        }

        public async Task<SupplierVM?> GetSupplierVMAsync(int? id)
        {
            var supplier = await context.Suppliers.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (supplier == null)
            {
                return null;
            }
            var supplierVM = mapper.Map<SupplierVM>(supplier);
            supplierVM.UserCreated = await GetUserName(supplierVM.UserCreated);
            supplierVM.UserModified = await GetUserName(supplierVM.UserModified);
            return supplierVM;
        }

        public async Task<List<Supplier>> GetAllActiveSupplierAsync(int includeId)
        {
            return await context.Suppliers.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
        }

        public async Task<bool> IsSupplierCodeExist(string code)
        {
            return await context.Suppliers.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsSupplierInUse(int id)
        {
            return await context.ItemTypes.AnyAsync(s => s.CategoryId == id);
        }
    }
}