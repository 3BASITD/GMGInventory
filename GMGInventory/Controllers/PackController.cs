using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InvoicingClerk + ","
                     + Roles.SystemAdmin + "," 
                     + Roles.Telemarketer)]
    public class PackController : Controller
    {
        private readonly IPackRepository packRepository;

        public PackController(IPackRepository packRepository)
        {
            this.packRepository = packRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson(ListFilterCommonModel filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await packRepository.GetListDataTablesAsync(form, filter));
        }
    }
}