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
        private readonly string currentUser;

        public WarehouseRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
            this.currentUser = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
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

        public async Task<WarehouseFormVM?> GetToEditAsync(int? id)
        {
            var warehouse = await context.Warehouses
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
            if (warehouse == null)
            {
                return null;
            }
            var warehouseVM = mapper.Map<WarehouseFormVM>(warehouse);
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
            var warehouse = mapper.Map<Warehouse>(warehouseVM);
            warehouse.IsActive = true;
            await context.Warehouses.AddAsync(warehouse);
            await context.SaveChangesAsync(currentUser);
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
            await context.SaveChangesAsync(currentUser);
        }

        public async Task UpdateAsync(WarehouseFormVM warehouseVM)
        {
            var warehouse = await context.Warehouses.Where(s => s.Id == warehouseVM.Id).FirstAsync();
            mapper.Map(warehouseVM, warehouse);
            context.Warehouses.Update(warehouse);
            await context.SaveChangesAsync(currentUser);
        }

        public async Task<bool> IsInUse(int id)
        {
            return await context.ItemInventories.AnyAsync(s => s.WarehouseId == id)
                || await context.PhysicalCountHistories.AnyAsync(s => s.WarehouseId == id)
                || await context.PurchaseOrders.AnyAsync(s => s.ToWarehouseId == id)
                || await context.TransferItems.AnyAsync(s => s.ToWarehouseId == id)
                || await context.TransferItems.AnyAsync(s => s.FromWarehouseId == id);
        }

        public async Task ReactivateAsync(int id)
        {
            var warehouse = await context.Warehouses.Where(s => s.Id == id).FirstAsync();
            warehouse.IsActive = true;
            context.Warehouses.Update(warehouse);
            await context.SaveChangesAsync(currentUser);
        }

        public async Task DeactivateAsync(int id)
        {
            var warehouse = await context.Warehouses.Where(s => s.Id == id).FirstAsync();
            warehouse.IsActive = false;
            context.Warehouses.Update(warehouse);
            await context.SaveChangesAsync(currentUser);
        }

        private async Task<string> GetUserName(string? userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user != null ? user.Nickname : "-";
        }
    }
}