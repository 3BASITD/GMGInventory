using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Data;
using GMGInventory.Common.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using ClosedXML.Excel;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class ItemTypeController : Controller
    {
        private readonly IItemTypeRepository itemTypeRepository;
        private readonly ICategoryRepository categoryRepository;

        public ItemTypeController(IItemTypeRepository itemTypeRepository
            , ICategoryRepository categoryRepository)
        {
            this.itemTypeRepository = itemTypeRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> GetOptions(int CategoryId, int? IncludeId)
        {
            return Json(await itemTypeRepository.GetOptions(CategoryId, IncludeId));
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetListJson()
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(itemTypeRepository.GetListDataTables(dtForm));
        }

        [HttpPost]
        public async Task<JsonResult> CreateJson(ItemTypeVM itemTypeVM)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await itemTypeRepository.IsItemTypeCodeExist(itemTypeVM.Code))
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
        public async Task<JsonResult> UploadJson(ItemTypeVM itemTypeVM)
        {
            ResponseModel responseModel = new ResponseModel();
            if(await itemTypeRepository.UploadCombinationExistAsync(itemTypeVM))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Already exist";
            }
            else if (await itemTypeRepository.IsItemTypeCodeExist(itemTypeVM.Code))
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
                    responseModel.Message = "Item type successfully added";
                }
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> EditJson(ItemTypeVM itemTypeVM)
        {
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

        public IActionResult DownloadFile()
        {
            DataTable dt = new DataTable("Category");
            dt.Columns.AddRange(new DataColumn[3] {
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Category")
            });
            var itemTypes = itemTypeRepository.GetAllItemTypeVM().Where(s => s.IsActive == true).ToList();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
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
