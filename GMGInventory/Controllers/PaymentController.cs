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

        [HttpPost]
        public JsonResult GetListJsonDetails()
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(customerpaymentRepository.GetListDataTablesDetails(dtForm));
        }


        public ActionResult Details(int id)
        {
            var payment = customerpaymentRepository.GetPaymentVM(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }


    }
}