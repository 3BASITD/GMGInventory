using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public WarehouseRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<WarehouseViewVM?> GetAsync(int? id)
        {
            var warehouse = await context.Warehouses
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
            if (warehouse == null)
            {
                return null;
            }
            var warehouseVM = mapper.Map<WarehouseViewVM>(warehouse);
            warehouseVM.UserCreated = await GetUserName(warehouseVM.UserCreated);
            warehouseVM.UserModified = await GetUserName(warehouseVM.UserModified);
            return warehouseVM;
        }

        public async Task<List<WarehouseViewVM>> GetAllAsync()
        {
            var warehouses = await context.Warehouses.ToListAsync();
            var warehousesVM = mapper.Map<List<WarehouseViewVM>>(warehouses);
            return warehousesVM;
        }

        public async Task<WarehouseFormVM> AddAsync(WarehouseFormVM warehouseVM)
        {
            string userId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var warehouse = mapper.Map<Warehouse>(warehouseVM);
            warehouse.IsActive = true;
            await context.Warehouses.AddAsync(warehouse);
            await context.SaveChangesAsync(userId);
            return mapper.Map<WarehouseFormVM>(warehouse);
        }

        public async Task<bool> IsIdExists(int id)
        {
            return await context.Warehouses.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> IsCodeExists(string code)
        {
            return await context.Warehouses.AnyAsync(s => s.Code == code);
        }

        public async Task DeleteAsync(int id)
        {
            var warehouse = context.Warehouses.Where(s => s.Id == id).First();
            context.Warehouses.Remove(warehouse);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WarehouseFormVM warehouseVM)
        {
            string userId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var warehouse = await context.Warehouses.Where(s => s.Id == warehouseVM.Id).FirstAsync();
            mapper.Map(warehouseVM, warehouse);
            context.Warehouses.Update(warehouse);
            await context.SaveChangesAsync(userId);
        }

        private async Task<string> GetUserName(string? userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user != null ? user.Nickname : "-";
        }
    }
}