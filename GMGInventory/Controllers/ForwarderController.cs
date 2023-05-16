using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector + "," + Roles.SystemAdmin)]
    //[Authorize]
    public class ForwarderController : Controller
    {
        private readonly IForwarderRepository forwarderRepository;
        private readonly ICustomerRepository customerRepository;

        public ForwarderController(IForwarderRepository forwarderRepository
            , ICustomerRepository customerRepository)
        {
            this.forwarderRepository = forwarderRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await forwarderRepository.GetOptions(IncludeId));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson()
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await forwarderRepository.GetAllForDataTablesAsync(form));
        }
        public async Task<IActionResult> Details(int? id)
        {
            var forwarderVM = await forwarderRepository.GetForwarderViewVMAsync(id);
            if (forwarderVM == null)
            {
                return NotFound();
            }
            return View(forwarderVM);
        }

        [HttpPost]
        public async Task<JsonResult> CreateJson(ForwarderVM forwarderVM)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await forwarderRepository.IsForwarderCodeExist(forwarderVM.Name))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Forwarder name already in use";
            }
            else
            {
                await forwarderRepository.AddAsync(forwarderVM);
                responseModel.Status = "Success";
                responseModel.Message = "Forwarder successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> EditJson(ForwarderVM forwarderVM)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                await forwarderRepository.UpdateAsync(forwarderVM);
                responseModel.Status = "Success";
                responseModel.Message = "Forwarder successfully updated";
            }
            catch (Exception ex)
            {
                if (!await forwarderRepository.Exists(forwarderVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Forwarder cannot be found";
                }
                else
                {
                    responseModel.Status = "Error";
                    responseModel.Message = ex.Message;
                }
            }
            return Json(responseModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await forwarderRepository.IsForwarderIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await forwarderRepository.IsForwarderInUse(id))
            {
                await forwarderRepository.DeactivateForwarderAsync(id);
                TempData["WarningMessage"] = "Forwarder has been deactivated";
            }
            else
            {
                await forwarderRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Forwarder successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            if (await forwarderRepository.IsForwarderIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await forwarderRepository.ReactivateForwarderAsync(id);
            TempData["SuccessMessage"] = "Forwarder reactivated successfully";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> DownloadFile()
        {
            DataTable dt = new DataTable("Forwarder");
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("Name"),
                new DataColumn("Address"),
                new DataColumn("Phone Number"),
                new DataColumn("Fax Number")

            });
            var forwarder = (await forwarderRepository.GetAllAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var forwarders in forwarder)
            {
                dt.Rows.Add(forwarders.Name, forwarders.Address, forwarders.PhoneNumber, forwarders.FaxNumber);
            }
            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    DateTime.Now.ToString("yyyyMMdd") + " Forwarders.xlsx");
                }
            }

        }

        [HttpPost]
        public async Task<JsonResult> UploadJson(ForwarderVM forwarderVM)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await forwarderRepository.UploadCombinationExistAsync(forwarderVM))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Already exist";
            }
            else if (await forwarderRepository.IsForwarderCodeExist(forwarderVM.Name))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Name already in use";
            }
            else
            {
                await forwarderRepository.AddAsync(forwarderVM);
                responseModel.Status = "Success";
                responseModel.Message = "Forwarder added";
            }
            return Json(responseModel);
        }
    }
}