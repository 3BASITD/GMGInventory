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
    public class CategoryController(IAccessRepository accessRepository
                                  , ICategoryRepository categoryRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await categoryRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Categories, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Categories)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Categories, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await categoryRepository.GetAllForDataTablesAsync(form));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(CategoryVM categoryVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Categories, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
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

        [HttpPost]
        public async Task<IActionResult> EditJson(CategoryVM categoryVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Categories, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
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

        public async Task<IActionResult> Details(int? id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Categories, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Categories)).Convert();

            var categoryVM = await categoryRepository.GetGenericVMAsync(id);
            if (categoryVM == null)
            {
                return NotFound();
            }
            return View(categoryVM);
        }

        public async Task<IActionResult> Create()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Categories, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Categories, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Categories, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Categories, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

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

        public async Task<IActionResult> DownloadFile()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Categories, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var dt = new DataTable("Category");
            dt.Columns.AddRange([
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Is Active")

            ]);
            var category = (await categoryRepository.GetAllGenericVMAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var categories in category)
            {
                dt.Rows.Add(categories.Code, categories.Name, categories.IsActive);
            }
            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            DateTime.Now.ToString("yyyyMMdd") + " Category.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> UploadJson(CategoryVM categoryVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Categories, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
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