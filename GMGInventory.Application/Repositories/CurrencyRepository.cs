using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CurrencyRepository(ApplicationDbContext context
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
            var currency = await context.Currencies
                                        .Where(s => s.Name.ToLower() == Name.ToLower())
                                        .FirstOrDefaultAsync();
            return currency?.Id;
        }

        public async Task<IList<JsonCurrencyOptionModel>> GetOptions(int? IncludeId)
        {
            return await context.Currencies
                //.Where(s => s.IsActive || s.Id == IncludeId)
                .Select(s => new JsonCurrencyOptionModel
                {
                    Id = s.Id.ToString(),
                    Text = s.Name,
                    Rate = s.Rate,
                    Abbreviation = s.Code,
                    Selected = IncludeId != null && IncludeId == s.Id
                })
                .OrderBy(s => s.Text)
                .ToListAsync();
        }

        public async Task<IList<CurrencyVM>> GetAllCurrencyVMAsync()
        {
            var currencies = await context.Currencies.ToListAsync();
            var currenciesVM = mapper.Map<List<CurrencyVM>>(currencies);
            foreach (CurrencyVM cvm in currenciesVM)
            {
                cvm.UserCreated = await GetNickname(cvm.UserCreated);
                cvm.UserModified = await GetNickname(cvm.UserModified);
            }
            return currenciesVM;
        }

        public async Task<CurrencyVM?> GetCurrencyVMAsync(int? id)
        {
            var currencies = await context.Currencies.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (currencies == null)
            {
                return null;
            }
            var currenciesVM = mapper.Map<CurrencyVM>(currencies);
            currenciesVM.UserCreated = await GetNickname(currenciesVM.UserCreated);
            currenciesVM.UserModified = await GetNickname(currenciesVM.UserModified);
            return currenciesVM;
        }

        public async Task<IList<Currency>> GetAllActiveCurrencyAsync(int includeId)
        {
            //return await context.Currencies.Where(s =>  s.Id == includeId).ToListAsync();
            var currencies = await context.Currencies.ToListAsync();
            return currencies;
        }

        public async Task<bool> IsCurrencyCodeExist(string code)
        {
            return await context.Currencies.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsCurrencyInUse(int id)
        {
            return await context.Suppliers.AnyAsync(s => s.CurrencyId == id);
        }
    }
}