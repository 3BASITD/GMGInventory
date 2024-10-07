using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.Accounting + ","
                     + Roles.InventoryManager + ","
                     + Roles.SalesDirector + ","
                     + Roles.SystemAdmin)]
    public class PaymentController(IAccessRepository accessRepository
                             , IPaymentRepository customerpaymentRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Payment, AccessTypes.View);
            if (redirectResult != null) return redirectResult;
            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Payment)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Payment, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await customerpaymentRepository.GetListDataTables(dtForm));
        }

        public async Task<IActionResult> Details(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Payment, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var cust = customerpaymentRepository.GetPaymentVM(id);
            if (cust == null)
            {
                return NotFound();
            }
            return View(cust);
        }

        [HttpPost]
        public async Task<IActionResult> GetPaymentChecksJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Payment, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await customerpaymentRepository.GetPaymentChecksDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> GetPaymentInvoicesJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Payment, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await customerpaymentRepository.GetPaymentInvoicesDTAsync(form, id));
        }
    }
}