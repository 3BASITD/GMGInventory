using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class PurchaseReturnReporitory : IPurchaseReturnRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public PurchaseReturnReporitory(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IList<PurchaseReturnVM>> GetListAsync(int purchaseOrderId)
        {
            var returnVM = (from rturn in context.PurchaseReturns
                            join ritem in context.PurchaseReturnLineItems
                                                 .Include(s => s.PurchaseOrderLineItem)
                                                    .ThenInclude(s => s.Item) on rturn.Id equals ritem.PurchaseReturnId
                            join user in userManager.Users on rturn.UserCreated equals user.Id
                            where rturn.PurchaseOrderId == purchaseOrderId
                            select new PurchaseReturnVM
                            {
                                Id = rturn.Id,
                                ItemCode = ritem.PurchaseOrderLineItem.Item.Code,
                                ItemName = ritem.PurchaseOrderLineItem.Item.Name,
                                Quantity = ritem.Quantity,
                                Remarks = rturn.Remarks + "",
                                ReturnBy = user.Nickname,
                                ReturnDateD = rturn.DateCreated
                            });
            return await returnVM.ToListAsync();
        }

        public async Task AddAsync(PurchaseReturnFormVM formVM)
        {
            string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
            var rturn = new PurchaseReturn
            {
                PurchaseOrderId = formVM.PurchaseOrderId,
                Remarks = formVM.Remarks
            };
            await context.PurchaseReturns.AddAsync(rturn);
            await context.SaveChangesAsync(currentUserId);

            var itemsVM = formVM.Items.Select(s => new PurchaseReturnLineItem
            {
                PurchaseOrderLineItemId = context.PurchaseOrderLineItems
                                                 .Where(a => a.PurchaseOrderId == formVM.PurchaseOrderId && a.ItemId == s.POItemId)
                                                 .Select(a => a.Id)
                                                 .First(),
                PurchaseReturnId = rturn.Id,
                Quantity = s.Quantity
            });
            await context.AddRangeAsync(itemsVM);
            await context.SaveChangesAsync(currentUserId);
        }
    }
}