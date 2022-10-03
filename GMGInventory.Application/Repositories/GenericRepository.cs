using GMGInventory.Application.Contracts;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public GenericRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<T> AddAsync(T entity)
        {
            string userId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            await context.AddAsync(entity);
            await context.SaveChangesAsync(userId);
            return entity;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            string userId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync(userId);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            string userId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            context.Update(entity);
            await context.SaveChangesAsync(userId);
        }

        public async Task<string> GetUserName(string? userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user != null ? user.Nickname : "-";
        }
    }
}