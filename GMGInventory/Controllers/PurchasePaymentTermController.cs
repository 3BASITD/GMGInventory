using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class PurchasePaymentTermController(IAccessRepository accessRepository
                                    , IPurchasePaymentTermRepository purchasePaymentTerm) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await purchasePaymentTerm.GetOptions(IncludeId));
        }

        public async Task<IActionResult> GetById(int? Id)
        {
            return Json(await purchasePaymentTerm.GetAsync(Id));
        }
    }
}
