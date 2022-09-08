using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.Accounting + ","
                     + Roles.InvoicingClerk + ","
                     + Roles.SystemAdmin + "," 
                     + Roles.Telemarketer)]
    public class SalesInvoiceController : Controller
    {
        private readonly IInvoiceRepository invoiceRepository;

        public SalesInvoiceController(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson(int? Id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await invoiceRepository.GetListDataTablesAsync(form, Id));
        }
    }
}