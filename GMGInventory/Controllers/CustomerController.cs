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
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IAreaRepository areaRepository;
        private readonly IRegionRepository regionRepository;
        private readonly ICreditTermRepository credittermRepository;
        private readonly ICustomerTypeRepository customertypeRepository;
        private readonly IAgentRepository agentRepository;

        public CustomerController(ICustomerRepository customerRepository
            , IAreaRepository areaRepository
            , IRegionRepository regionRepository
            , ICreditTermRepository credittermRepository
            , ICustomerTypeRepository customertypeRepository
            , IAgentRepository agentRepository)
        {
            this.customerRepository = customerRepository;
            this.areaRepository = areaRepository;
            this.regionRepository = regionRepository;
            this.credittermRepository = credittermRepository;
            this.customertypeRepository = customertypeRepository;
            this.agentRepository = agentRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetListJson()
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(customerRepository.GetListDataTables(dtForm));


        }

        [HttpPost]
        public async Task<JsonResult> CreateJson(CustomerVM custVM)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await customerRepository.IsCodeExistAsync(custVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Customer code already in use";
            }
            else
            {
                await customerRepository.AddAsync(custVM);
                responseModel.Status = "Success";
                responseModel.Message = "Customer successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> EditJson(CustomerVM custVM)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                await customerRepository.UpdateAsync(custVM);
                responseModel.Status = "Success";
                responseModel.Message = "Customer successfully updated";
            }
            catch (Exception ex)
            {
                if (!await customerRepository.IsCustomerIdExistAsync(custVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Customer cannot be found";
                }
                else
                {
                    responseModel.Status = "Error";
                    responseModel.Message = ex.Message;
                }
            }
            return Json(responseModel);
        }
        public ActionResult Details(int id)
        {
            var cust = customerRepository.GetCustomerVM(id);
            if (cust == null)
            {
                return NotFound();
            }
            return View(cust);
        }

        [HttpPost]
        public async Task<JsonResult> GetShippingJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await customerRepository.GetCustomerShippingDTAsync(form, id));
        }

        [HttpPost]
        public async Task<JsonResult> GetForwarderJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await customerRepository.GetCustomerForwarderDTAsync(form, id));
        }


        [HttpPost]
        public async Task<JsonResult> GetPaymentJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await customerRepository.GetCustomerPaymentDTAsync(form, id));
        }

        [HttpPost]
        public async Task<JsonResult> GetReceivableJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await customerRepository.GetCustomerReceivableDTAsync(form, id));
        }





        //[HttpPost]
        //public JsonResult GetListJsonDetails()
        //{
        //    DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
        //    return Json(soaRepository.GetListDataTablesDetails(dtForm));
        //}


        //public ActionResult Details(int id)
        //{
        //    var payment = soaRepository.GetSoacrVM(id);
        //    if (payment == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(payment);
        //}


    }
}
