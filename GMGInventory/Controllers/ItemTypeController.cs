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
    public class ItemTypeController(IAccessRepository accessRepository
                            , IItemTypeRepository itemTypeRepository
                            , ICategoryRepository categoryRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? CategoryId, int? IncludeId)
        {
            return Json(await itemTypeRepository.GetOptions(CategoryId, IncludeId));
        }

        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await itemTypeRepository.GetOptions2(search, page));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ItemTypes)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await itemTypeRepository.GetListDTAsync(dtForm));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(ItemTypeVM itemTypeVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await itemTypeRepository.IsCodeExist(itemTypeVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Item type code already in use";
            }
            else
            {
                await itemTypeRepository.AddAsync(itemTypeVM);
                responseModel.Status = "Success";
                responseModel.Message = "Item type successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadJson(ItemTypeFileVM itemTypeVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            return Json(new ResponseModel()
            {
                Status = "Success",
                Message = await itemTypeRepository.SaveItemFromFile(itemTypeVM)
            });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileJson(IFormFile file)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var response = await itemTypeRepository.UploadFile(file);

            Response.Headers["X-Status"] = response.Status;
            Response.Headers["X-Message"] = response.Message;
            return File(response.Data.Content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        response.Data.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> EditJson(ItemTypeVM itemTypeVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            try
            {
                await itemTypeRepository.UpdateAsync(itemTypeVM);
                responseModel.Status = "Success";
                responseModel.Message = "Item type successfully updated";
            }
            catch (Exception ex)
            {
                if (!await itemTypeRepository.Exists(itemTypeVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Item type cannot be found";
                }
                else
                {
                    responseModel.Status = "Error";
                    responseModel.Message = SystemFormat.GetInnermostExceptionMessage(ex);
                }
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> DownloadFile()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new DataTable("Item Types");
            dt.Columns.AddRange([
                new DataColumn("Code"),
                new DataColumn("Name")
            ]);
            var itemTypes = (await itemTypeRepository.GetListAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var itemType in itemTypes)
            {
                dt.Rows.Add(itemType.Code, itemType.Name);
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Item Types.xlsx");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            if (await itemTypeRepository.IsItemTypeIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await itemTypeRepository.IsItemTypeInUse(id))
            {
                await itemTypeRepository.DeactivateItemTypeAsync(id);
                TempData["WarningMessage"] = "Item type has been deactivated";
            }
            else
            {
                await itemTypeRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Item type successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            if (await itemTypeRepository.IsItemTypeIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await itemTypeRepository.ReactivateItemTypeAsync(id);
            TempData["SuccessMessage"] = "Item type reactivated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}