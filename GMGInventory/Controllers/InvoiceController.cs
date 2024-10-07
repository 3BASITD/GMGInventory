using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class InvoiceController(IInvoiceRepository invoiceRepository) : Controller
    {
        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await invoiceRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> GetOptionsInvoiceNumber(string search, int page)
        {
            return Json(await invoiceRepository.GetOptionsInvoiceNumber(search, page));
        }

        public async Task<IActionResult> GetOptionsBIRInvoiceNumber(string search, int page)
        {
            return Json(await invoiceRepository.GetOptionsBIRInvoiceNumber(search, page));
        }
    }
}