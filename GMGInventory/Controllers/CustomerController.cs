using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await customerRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> GetOptions2(int? IncludeId)
        {
            return Json(await customerRepository.GetOptions2(IncludeId));
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
        public async Task<JsonResult> CreateShippingJson(CustomerVM shippingVM)
        {
            ResponseModel responseModel = new ResponseModel();
            await customerRepository.AddShippingAsync(shippingVM);
            responseModel.Status = "Success";
            responseModel.Message = "Shipping successfully added";

            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> CreateForwarderJson(CustomerVM forwarderVM)
        {
            ResponseModel responseModel = new ResponseModel();
            await customerRepository.AddForwarderAsync(forwarderVM);
            responseModel.Status = "Success";
            responseModel.Message = "Forwarder successfully added";

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
                if (!await customerRepository.IsCodeExistAsync(custVM.Code))
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
            var cust = customerRepository.GetCustomerDtlVM(id);
            if (cust == null)
            {
                return NotFound();
            }
            return View(cust);
        }

        public ActionResult MoreDetails(int id)
        {
            var cust = customerRepository.GetCustomerDtlVM(id); // gawa ka panibagong GetCustomerVM(id) na andun mga kailangan mo sa modal
            if (cust == null)
            {
                return NotFound();
            }
            return Json(cust);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerJson(int id)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await customerRepository.GetCustomerDetailsDTAsync(form, id));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            {
                await customerRepository.DeactivateCustomerAsync(id);
                TempData["WarningMessage"] = "Customer has been deactivated";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            await customerRepository.ReactivateCustomerAsync(id);
            TempData["SuccessMessage"] = "Customer reactivated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}