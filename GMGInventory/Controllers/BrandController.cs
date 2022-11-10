using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    //[Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector)]
    [Authorize]
    public class BrandController : Controller
    {
        private readonly IItemTypeRepository itemTypeRepository;
        private readonly IBrandRepository brandRepository;

        public BrandController(IBrandRepository brandRepository
            , IItemTypeRepository itemTypeRepository)
        {
            this.itemTypeRepository = itemTypeRepository;
            this.brandRepository = brandRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await brandRepository.GetOptions(IncludeId));
        }

        [HttpPost]
        public async Task<JsonResult> GetListJson()
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await brandRepository.GetListDTAsync(dtForm));
        }

        [HttpPost]
        public async Task<JsonResult> CreateJson(BrandVM brandVM)
        {
            ResponseModel responseModel = new ResponseModel();
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
        public async Task<JsonResult> UploadJson(BrandVM brandVM)
        {
            ResponseModel responseModel = new ResponseModel();
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
        public async Task<JsonResult> EditJson(BrandVM brandVM)
        {
            ResponseModel responseModel = new ResponseModel();
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
            DataTable dt = new DataTable("Item Types");
            dt.Columns.AddRange(new DataColumn[3] {
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Item Type")
            });
            var brands = (await brandRepository.GetListAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var brand in brands)
            {
                dt.Rows.Add(brand.Code, brand.Name, brand.ItemType);
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Brands.xlsx");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
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