using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext context;

        public CountryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int?> GetIdAsync(string? Name)
        {
            var country = await context.Countries.Where(s => s.Name.ToLower() == Name.ToLower()).FirstOrDefaultAsync();
            return country?.Id;
        }

        public async Task<Select2Model<JsonCountryOptionsModel>> GetOptions2(string search, int page)
        {
            var countries = await context.Countries
                                         .Where(s => string.IsNullOrWhiteSpace(search)
                                                  || s.Name.ToLower().Contains(search.ToLower())
                                                  || s.Alpha2.ToLower().Contains(search.ToLower())
                                                  || s.Alpha3.ToLower().Contains(search.ToLower()))
                                         .Select(s => new JsonCountryOptionsModel
                                         {
                                             Id = s.Id.ToString(),
                                             Text = s.Name,
                                             Alpha2 = s.Alpha2,
                                             Alpha3 = s.Alpha3
                                         })
                                         .OrderBy(s => s.Text)
                                         .ToListAsync();
            var countriesFilterd = countries.Skip((page - 1) * 50).Take(50).ToList();
            return new Select2Model<JsonCountryOptionsModel>
            {
                //incomplete_results = false,
                items = countriesFilterd,
                total_count = countries.Count
            };
        }

        public async Task<IList<JsonCountryOptionsModel>> GetOptions(int? IncludeId)
        {
            return await context.Countries
                                .Select(s => new JsonCountryOptionsModel
                                {
                                    Id = s.Id.ToString(),
                                    Text = s.Name,
                                    Alpha2 = s.Alpha2,
                                    Alpha3 = s.Alpha3,
                                    Selected = s.Id == IncludeId
                                })
                                .OrderBy(s => s.Text)
                                .ToListAsync();
        }
    }
}