using AutoMapper;
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
    [Authorize]
    public class AreaController : Controller
    {
        private readonly IAreaRepository areaRepository;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public AreaController(IAreaRepository areaRepository
            , IRegionRepository regionRepository
            , IMapper mapper)
        {
            this.areaRepository = areaRepository;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await areaRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await areaRepository.GetOptions2(search, page));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        public async Task<JsonResult> CreateJson(AreaVM areaVM)
        {
            ResponseModel responseModel = new ResponseModel();
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


        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        public async Task<JsonResult> GetListJson()
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await areaRepository.GetAllForDataTablesAsync(form));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        public async Task<JsonResult> EditJson(AreaVM areaVM)
        {
            ResponseModel responseModel = new ResponseModel();
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

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await areaRepository.IsAreaIdExistAsync(id) == false)
            {
                return NotFound();
            }
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

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            if (await areaRepository.IsAreaIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await areaRepository.ReactivateAreaAsync(id);
            TempData["SuccessMessage"] = "Area reactivated successfully";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        public async Task<IActionResult> DownloadFile()
        {
            DataTable dt = new DataTable("Area");
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Commission Rate"),
                new DataColumn("Region")

            });
            var area = (await areaRepository.GetAllAreaAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var areas in area)
            {
                dt.Rows.Add(areas.Code, areas.Name, areas.CommissionRate, areas.RegionName);
            }
            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    DateTime.Now.ToString("yyyyMMdd") + " Areas.xlsx");
                }
            }

        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        public async Task<JsonResult> UploadJson(AreaVM areaVM)
        {
            ResponseModel responseModel = new ResponseModel();
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