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
    public class AreaController(IAccessRepository accessRepository
                              , IAreaRepository areaRepository
                              , IRegionRepository regionRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await areaRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await areaRepository.GetOptions2(search, page));
        }

        public async Task<IActionResult> GetByRegionOptions(string search, int page, int? region)
        {
            return Json(await areaRepository.GetByRegionOptions(search, page, region));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Areas, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Areas)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(AreaVM areaVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Areas, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            if (await areaRepository.IsAreaCodeExist(areaVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Area code already in use";
            }
            else
            {
                await areaRepository.AddAsync(areaVM);
                responseModel.Status = "Success";
                responseModel.Message = "Area successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Areas, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await areaRepository.GetAllForDataTablesAsync(form));
        }

        [HttpPost]
        public async Task<IActionResult> EditJson(AreaVM areaVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Areas, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            try
            {
                await areaRepository.UpdateAsync(areaVM);
                responseModel.Status = "Success";
                responseModel.Message = "Area successfully updated";
            }
            catch (Exception ex)
            {
                if (!await areaRepository.Exists(areaVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Area cannot be found";
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
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Areas, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            if (await areaRepository.IsAreaIdExistAsync(id) == false)
                return NotFound();

            if (await areaRepository.IsAreaInUse(id))
            {
                await areaRepository.DeactivateAreaAsync(id);
                TempData["WarningMessage"] = "Area has been deactivated";
            }
            else
            {
                await areaRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Area successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Areas, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Areas, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            if (await areaRepository.IsAreaIdExistAsync(id) == false)
                return NotFound();

            await areaRepository.ReactivateAreaAsync(id);
            TempData["SuccessMessage"] = "Area reactivated successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadFile()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Areas, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var dt = new DataTable("Area");
            dt.Columns.AddRange([
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Commission Rate"),
                new DataColumn("Region")

            ]);
            var area = (await areaRepository.GetAllAreaAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var areas in area)
            {
                dt.Rows.Add(areas.Code, areas.Name, areas.CommissionRate, areas.RegionName);
            }
            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            DateTime.Now.ToString("yyyyMMdd") + " Areas.xlsx");

        }

        [HttpPost]
        public async Task<IActionResult> UploadJson(AreaVM areaVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Areas, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            if (await areaRepository.UploadCombinationExistAsync(areaVM))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Already exist";
            }
            else if (await areaRepository.IsCodeExist(areaVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Code already in use";
            }
            else
            {
                int? RegionId = await regionRepository.GetIdAsync(areaVM.RegionName);
                if (RegionId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Region";
                }
                else
                {
                    areaVM.RegionId = RegionId.Value;
                    await areaRepository.AddAsync(areaVM);
                    responseModel.Status = "Success";
                    responseModel.Message = "Area added";
                }
            }
            return Json(responseModel);
        }
    }
}