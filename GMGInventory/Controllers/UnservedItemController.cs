using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + "," 
                     + Roles.SalesDirector + "," 
                     + Roles.SystemAdmin + "," 
                     + Roles.Telemarketer)]
    public class UnservedItemController : Controller
    {
        private readonly IUnservedItemRepository unservedItemRepository;

        public UnservedItemController(IUnservedItemRepository unservedItemRepository)
        {
            this.unservedItemRepository = unservedItemRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetListJson()
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(unservedItemRepository.GetListDataTables(dtForm));
        }
    }
}