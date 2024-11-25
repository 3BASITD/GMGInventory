using AutoMapper;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class PurchasePaymentTermRepository : GenericRepository<PurchasePaymentTerm>, IPurchasePaymentTermRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public PurchasePaymentTermRepository(ApplicationDbContext context
           , IHttpContextAccessor httpContextAccessor
           , UserManager<User> userManager
           , IMapper mapper) : base(context, httpContextAccessor, userManager)
        {
            this.context = context;
        }

        public async Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId)
        {
            var creditTerms = await context.PurchasePaymentTerms.ToListAsync();
            return creditTerms.Select(s => new JsonOptionsModel
            {
                Id = s.Id.ToString(),
                Text = s.Name,
                Selected = IncludeId != null && IncludeId == s.Id
            })
                .OrderBy(s => s.Text == "CASH" ? 0 : 1)
                .ThenBy(s =>
                {
                    if (s.Text == "CASH") return 0;
                    int.TryParse(new string(s.Text.Where(char.IsDigit).ToArray()), out int numberPart);
                    return numberPart;
                })
                .ToList();
        }
    }
}
