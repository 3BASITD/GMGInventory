using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    public class BrandController(IAccessRepository accessRepository
                               , IItemTypeRepository itemTypeRepository
                               , IBrandRepository brandRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await brandRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Brands, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Brands)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Brands, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await brandRepository.GetListDTAsync(dtForm));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(BrandVM brandVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Brands, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            if (await brandRepository.IsBrandCodeExist(brandVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Brand code already in use";
            }
            else
            {
                await brandRepository.AddAsync(brandVM);
                responseModel.Status = "Success";
                responseModel.Message = "Brand successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadJson(BrandVM brandVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Brands, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            if (await brandRepository.UploadCombinationExistAsync(brandVM))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Already exist";
            }
            else if (await brandRepository.IsBrandCodeExist(brandVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Code already in use";
            }
            else
            {
                int? ItemTypeId = await itemTypeRepository.GetIdAsync(brandVM.ItemType);
                if (ItemTypeId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Item Type";
                }
                else
                {
                    brandVM.ItemTypeId = ItemTypeId.Value;
                    await brandRepository.AddAsync(brandVM);
                    responseModel.Status = "Success";
                    responseModel.Message = "Brand successfully added";
                }
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditJson(BrandVM brandVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Brands, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            try
            {
                await brandRepository.UpdateAsync(brandVM);
                responseModel.Status = "Success";
                responseModel.Message = "Brand successfully updated";
            }
            catch (Exception ex)
            {
                if (!await brandRepository.Exists(brandVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Brand cannot be found";
                }
                else
                {
                    responseModel.Status = "Error";
                    responseModel.Message = ex.Message;
                }
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> DownloadFile()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Brands, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var dt = new DataTable("Item Types");
            dt.Columns.AddRange([
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Item Type")
            ]);
            var brands = (await brandRepository.GetListAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var brand in brands)
            {
                dt.Rows.Add(brand.Code, brand.Name, brand.ItemType);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                DateTime.Now.ToString("yyyyMMdd") + " Brands.xlsx");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Brands, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            if (await brandRepository.IsBrandIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await brandRepository.IsBrandInUse(id))
            {
                await brandRepository.DeactivateBrandAsync(id);
                TempData["WarningMessage"] = "Brand has been deactivated";
            }
            else
            {
                await brandRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Brand successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Brands, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Brands, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            if (await brandRepository.IsBrandIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await brandRepository.ReactivateBrandAsync(id);
            TempData["SuccessMessage"] = "Brand reactivated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}