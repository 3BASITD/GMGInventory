using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await invoiceRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> GetOptionsInvoiceNumber(string search, int page)
        {
            return Json(await invoiceRepository.GetOptionsInvoiceNumber(search, page));
        }
    }
}