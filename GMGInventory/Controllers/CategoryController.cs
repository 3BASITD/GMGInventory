using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await categoryRepository.GetOptions(IncludeId));
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
        public async Task<JsonResult> GetListJson()
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await categoryRepository.GetAllForDataTablesAsync(form));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        public async Task<JsonResult> CreateJson(CategoryVM categoryVM)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await categoryRepository.IsCategoryCodeExist(categoryVM.CategoryCode))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Category code already in use";
            }
            else
            {
                await categoryRepository.AddAsync(categoryVM);
                responseModel.Status = "Success";
                responseModel.Message = "Category successfully added";
            }
            return Json(responseModel);
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        public async Task<JsonResult> EditJson(CategoryVM categoryVM)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                await categoryRepository.UpdateAsync(categoryVM);
                responseModel.Status = "Success";
                responseModel.Message = "Area successfully updated";
            }
            catch (Exception ex)
            {
                if (!await categoryRepository.Exists(categoryVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Category cannot be found";
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
        public async Task<IActionResult> Details(int? id)
        {
            var categoryVM = await categoryRepository.GetGenericVMAsync(id);
            if (categoryVM == null)
            {
                return NotFound();
            }
            return View(categoryVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            if (await categoryRepository.IsCategoryInUse(id))
            {
                category.IsActive = false;
                await categoryRepository.UpdateAsync(category);
                TempData["WarningMessage"] = "Category has been deactivated";
            }
            else
            {
                await categoryRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Category successfully deleted";
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
            var category = await categoryRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            category.IsActive = true;
            await categoryRepository.UpdateAsync(category);
            TempData["SuccessMessage"] = "Category successfully reactivated";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        public async Task<IActionResult> DownloadFile()
        {
            DataTable dt = new DataTable("Category");
            dt.Columns.AddRange(new DataColumn[3] {
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Is Active")

            });
            var category = (await categoryRepository.GetAllGenericVMAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var categories in category)
            {
                dt.Rows.Add(categories.Code, categories.Name, categories.IsActive);
            }
            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    DateTime.Now.ToString("yyyyMMdd") + " Category.xlsx");
                }
            }
        }

        [Authorize(Roles = Roles.InventoryManager + ","
                         + Roles.SalesDirector + ","
                         + Roles.SystemAdmin)]
        [HttpPost]
        public async Task<JsonResult> UploadJson(CategoryVM categoryVM)
        {
            ResponseModel responseModel = new ResponseModel();
            if (await categoryRepository.UploadCombinationExistAsync(categoryVM))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Already exist";
            }
            else if (await categoryRepository.IsCodeExist(categoryVM.CategoryCode))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Code already in use";
            }
            else
            {
                    await categoryRepository.AddAsync(categoryVM);
                    responseModel.Status = "Success";
                    responseModel.Message = "Category added";
                
            }
            return Json(responseModel);
        }
    }
}