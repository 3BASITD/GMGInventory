using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
   [Authorize]
    public class ItemTypeController(IAccessRepository accessRepository
                            , IItemTypeRepository itemTypeRepository
                            , ICategoryRepository categoryRepository
                            , UserManager<User> userManager) : BaseController(accessRepository, userManager)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await itemTypeRepository.GetOptions(IncludeId));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ItemTypes)).Convert();

            return View();
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await itemTypeRepository.GetListDTAsync(dtForm));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
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

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        public async Task<IActionResult> UploadJson(ItemTypeVM itemTypeVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await itemTypeRepository.UploadCombinationExistAsync(itemTypeVM))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Already exist";
            }
            else if (await itemTypeRepository.IsCodeExist(itemTypeVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Code already in use";
            }
            else
            {
                int? CategoryId = await categoryRepository.GetIdAsync(itemTypeVM.Category);
                if (CategoryId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Category";
                }
                else
                {
                    itemTypeVM.CategoryId = CategoryId.Value;
                    await itemTypeRepository.AddAsync(itemTypeVM);
                    responseModel.Status = "Success";
                    responseModel.Message = "Item type added";
                }
            }
            return Json(responseModel);
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
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
                    responseModel.Message = ex.Message;
                }
            }
            return Json(responseModel);
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        public async Task<IActionResult> DownloadFile()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemTypes, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new DataTable("Item Types");
            dt.Columns.AddRange(new DataColumn[3] {
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Category")
            });
            var itemTypes = (await itemTypeRepository.GetListAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var itemType in itemTypes)
            {
                dt.Rows.Add(itemType.Code, itemType.Name, itemType.Category);
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

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
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

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
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