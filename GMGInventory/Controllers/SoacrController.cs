using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    //[Authorize(Roles = Roles.InventoryManager + ","
    //                 + Roles.SalesDirector + ","
    //                 + Roles.SystemAdmin + ","
    //                 + Roles.Telemarketer)]

    [Authorize]
    public class SoacrController : Controller
    {
        private readonly ISoacrRepository soaRepository;

        public SoacrController(ISoacrRepository soaRepository)
        {
            this.soaRepository = soaRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetListJson()
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(soaRepository.GetListDataTables(dtForm));
        }

        //[HttpPost]
        //public JsonResult GetListJsonDetails()
        //{
        //    DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
        //    return Json(soaRepository.GetListDataTablesDetails(dtForm));
        //}


        public ActionResult Details(int id)
        {
            var payment = soaRepository.GetSoacrVM(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }


    }
}