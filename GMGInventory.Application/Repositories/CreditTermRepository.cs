using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class CreditTermRepository : GenericRepository<CreditTerm>, ICreditTermRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CreditTermRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IList<CreditTermVM>> GetAllCreditTermVMAsync()
        {
            var creditterms = await context.CreditTerms.ToListAsync();
            var credittermsVM = mapper.Map<List<CreditTermVM>>(creditterms);
            foreach (CreditTermVM cvm in credittermsVM)
            {
                cvm.UserCreated = await GetUserName(cvm.UserCreated);
                cvm.UserModified = await GetUserName(cvm.UserModified);
            }
            return credittermsVM;
        }

        public async Task<CreditTermVM?> GetCreditTermVMAsync(int? id)
        {
            var creditterms = await context.CreditTerms.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (creditterms == null)
            {
                return null;
            }
            var credittermsVM = mapper.Map<CreditTermVM>(creditterms);
            credittermsVM.UserCreated = await GetUserName(credittermsVM.UserCreated);
            credittermsVM.UserModified = await GetUserName(credittermsVM.UserModified);
            return credittermsVM;
        }

        public async Task<IList<CreditTerm>> GetAllActiveCreditTermAsync(int includeId)
        {
            //return await context.Regions.Where(s => s.IsActive || s.Id == includeId).ToListAsync();
            var creditterms = await context.CreditTerms.ToListAsync();
            return creditterms;
        }

        public async Task<bool> IsCreditTermCodeExist(int code)
        {
            return await context.CreditTerms.AnyAsync(c => c.Id == code);
        }

        public async Task<bool> IsCreditTermInUse(int id)
        {
            return await context.Customers.AnyAsync(s => s.CreditTermId == id);
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.CreditTerms
                .Select(s => new JsonOptionsModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .ToListAsync();
        }


    }





}