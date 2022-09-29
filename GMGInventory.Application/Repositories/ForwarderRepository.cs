using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class ForwarderRepository : GenericRepository<Forwarder>, IForwarderRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public ForwarderRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Forwarders
                                .Select(s => new JsonOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Selected = IncludeId != null && IncludeId == s.Id
                                })
                                .ToListAsync();
        }

        public async Task<List<ForwarderViewVM>> GetAllForwarderVMAsync()
        {
            var forwarders = await context.Forwarders
                .Include(s => s.Customer)
                .ToListAsync();
            var forwardersViewVM = mapper.Map<List<ForwarderViewVM>>(forwarders);
            foreach (ForwarderViewVM ivm in forwardersViewVM)
            {
                ivm.UserCreated = await GetUserName(ivm.UserCreated);
                ivm.UserModified = await GetUserName(ivm.UserModified);
            }
            return forwardersViewVM;
        }

        public async Task<ForwarderViewVM?> GetForwarderViewVMAsync(int? id)
        {
            var forwarders = await context.Forwarders
                .Include(s => s.Customer)
                .Where(s => s.Id == id).FirstOrDefaultAsync();
            if (forwarders == null)
            {
                return null;
            }
            var forwardersViewVM = mapper.Map<ForwarderViewVM>(forwarders);
            forwardersViewVM.UserCreated = await GetUserName(forwardersViewVM.UserCreated);
            forwardersViewVM.UserModified = await GetUserName(forwardersViewVM.UserModified);
            return forwardersViewVM;
        }

        public async Task<ForwarderVM?> GetForwarderVMAsync(int? id)
        {
            var forwarders = await context.Forwarders
                .Include(s => s.Customer)
                .Where(s => s.Id == id).FirstOrDefaultAsync();
            if (forwarders == null)
            {
                return null;
            }
            var forwardersViewVM = mapper.Map<ForwarderVM>(forwarders);
            return forwardersViewVM;
        }

        public async Task<List<Forwarder>> GetAllActiveForwarderAsync(int includeId)
        {
            //return await context.Suppliers.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            var forwarders = await context.Forwarders.ToListAsync();
            return forwarders;
        }

        public async Task<bool> IsForwarderCodeExist(string code)
        {
            return await context.Forwarders.AnyAsync(c => c.Name == code);
        }

        public async Task<bool> IsForwarderInUse(int id)
        {
            return await context.Forwarders.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> IsForwarderIdExistAsync(int id)
        {
            return await context.Forwarders.AnyAsync(s => s.Id == id);
        }

        public async Task DeactivateForwarderAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var forwarders = await context.Forwarders.FirstAsync(s => s.Id == id);
            // forwarders.IsActive = false;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task ReactivateForwarderAsync(int id)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var forwarders = await context.Forwarders.FirstAsync(s => s.Id == id);
            // forwarders.IsActive = true;
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task AddAsync(ForwarderVM forwarderVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var forwarders = mapper.Map<Forwarder>(forwarderVM);
            // forwarders.IsActive = true;
            await context.Forwarders.AddAsync(forwarders);
            await context.SaveChangesAsync(currentUserId);
        }

        public async Task UpdateAsync(int id, ForwarderVM forwarderVM)
        {
            var forwarders = context.Forwarders.Where(s => s.Id == id).FirstOrDefault();
            if (forwarders == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            mapper.Map(forwarderVM, forwarders);
            context.Forwarders.Update(forwarders);
            await context.SaveChangesAsync(currentUserId);
        }
    }
}