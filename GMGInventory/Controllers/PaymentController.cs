using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository customerpaymentRepository;

        public PaymentController(IPaymentRepository customerpaymentRepository)
        {
            this.customerpaymentRepository = customerpaymentRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetListJson()
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(customerpaymentRepository.GetListDataTables(dtForm));
        }

        public ActionResult Details(int id)
        {
            var cust = customerpaymentRepository.GetPaymentVM(id);
            if (cust == null)
            {
                return NotFound();
            }
            return View(cust);
        }

        [HttpPost]
        public async Task<JsonResult> GetPaymentChecksJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await customerpaymentRepository.GetPaymentChecksDTAsync(form, id));
        }

        [HttpPost]
        public async Task<JsonResult> GetPaymentInvoicesJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await customerpaymentRepository.GetPaymentInvoicesDTAsync(form, id));
        }
    }
}