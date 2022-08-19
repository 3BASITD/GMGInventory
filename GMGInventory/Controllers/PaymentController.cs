using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Data;
using GMGInventory.Common.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository customerpaymentRepository;
        private readonly IAreaRepository areaRepository;
        private readonly IRegionRepository regionRepository;
        private readonly ICreditTermRepository credittermRepository;
        private readonly ICustomerTypeRepository customertypeRepository;
        private readonly IAgentRepository agentRepository;

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





    }
}
