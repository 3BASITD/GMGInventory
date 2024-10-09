using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class ForwarderController(IAccessRepository accessRepository
                                   , IForwarderRepository forwarderRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await forwarderRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Forwarders, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Forwarders)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Forwarders, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await forwarderRepository.GetAllForDataTablesAsync(form));
        }

        public async Task<IActionResult> Details(int? id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Forwarders, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var forwarderVM = await forwarderRepository.GetForwarderViewVMAsync(id);
            if (forwarderVM == null)
            {
                return NotFound();
            }
            return View(forwarderVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(ForwarderVM forwarderVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Forwarders, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
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
        public async Task<IActionResult> EditJson(ForwarderVM forwarderVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Forwarders, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
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
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Forwarders, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

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
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Forwarders, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Forwarders, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

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
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Forwarders, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Forwarder");
            dt.Columns.AddRange([
                new DataColumn("Name"),
                new DataColumn("Address"),
                new DataColumn("Phone Number"),
                new DataColumn("Fax Number")

            ]);
            var forwarder = (await forwarderRepository.GetAllAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var forwarders in forwarder)
            {
                dt.Rows.Add(forwarders.Name, forwarders.Address, forwarders.PhoneNumber, forwarders.FaxNumber);
            }
            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            DateTime.Now.ToString("yyyyMMdd") + " Forwarders.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> UploadJson(ForwarderVM forwarderVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Forwarders, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
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