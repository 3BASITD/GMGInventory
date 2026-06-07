using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository
    {
        private readonly ApplicationDbContext context;
        private readonly string currentUserId;

        public CurrencyRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
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
            var currencies = from currency in context.Currencies
                             let latestRate = currency.PurchaseOrderNews
                                                      .OrderByDescending(s => s.DateCreated)
                                                      .Select(s => s.CurrencyRate)
                                                      .FirstOrDefault()
                             where currency.IsActive == true
                             select new JsonCurrencyOptionModel
                             {
                                 Id = currency.Id.ToString(),
                                 Text = currency.Name,
                                 Rate = latestRate == 0 ? 0m : latestRate
                             };
            return await currencies.ToListAsync();
        }

        private async Task<IList<Currency>> GetCurrencies()
        {
            var currencies = await context.Currencies.Where(s => s.IsActive == true).ToListAsync();
            return currencies;
        }

        public async Task<ForDataTables<Currency>> GetCurrenciesDT(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetCurrencies()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.Name,
                    s.IsActive.ToString(),
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<Currency>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<ResponseModel> UpdateList(IList<Currency> currencies)
        {
            try
            {
                if (currencies.Count == 0)
                    return new ResponseModel { Status = "Error", Message = "Nothing to update" };

                var currentCurrencies = await context.Currencies
                                                     .Include(s => s.PurchaseOrderNews)
                                                     .Include(s => s.PurchaseOrders)
                                                     .Include(s => s.Suppliers)
                                                     .ToListAsync();

                var currentNames = currentCurrencies.Select(s => s.Name)
                                                    .ToHashSet();

                var newCurrencies = currencies.Where(s => !currentNames.Contains(s.Name)).ToList();
                foreach (var currency in newCurrencies)
                {
                    currency.IsActive = true;
                    await context.Currencies.AddAsync(currency);
                }

                var existingCurrencies = currentCurrencies.Where(s => currencies.Any(c => c.Name == s.Name))
                                                          .ToList();
                foreach (var dbCurrency in existingCurrencies)
                {
                    dbCurrency.IsActive = true;
                }

                var inputNames = currencies.Select(s => s.Name)
                                           .ToHashSet();
                var removeCurrencies = currentCurrencies.Where(s => !inputNames.Contains(s.Name))
                                                        .ToList();

                foreach (var currency in removeCurrencies)
                {
                    if (currency.PurchaseOrderNews.Any() || currency.PurchaseOrders.Any() || currency.Suppliers.Any())
                        currency.IsActive = false;
                    else
                        context.Currencies.Remove(currency);
                }

                await context.SaveChangesAsync(currentUserId);

                return new ResponseModel { Status = "Success", Message = "Successfully update currencies" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Status = "Error", Message = SystemFormat.GetInnermostExceptionMessage(ex) };
            }
        }

        public async Task<IList<Currency>> GetAllActiveCurrencyAsync(int includeId)
        {
            var currencies = await context.Currencies.ToListAsync();
            return currencies;
        }
    }
}