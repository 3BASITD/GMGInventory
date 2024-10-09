using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class InvoiceController(IAccessRepository accessRepository
                                 , IInvoiceRepository invoiceRepository) : BaseController(accessRepository)
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