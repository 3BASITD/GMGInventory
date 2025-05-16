using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SupplierController(IAccessRepository accessRepository
                             , ISupplierRepository supplierRepository
                             , ICurrencyRepository currencyRepository
                             , IRegionRepository regionRepository
                             , IBusinessStyleRepository businessStyleRepository
                             , ICreditTermRepository creditTermRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId, bool? IsLocal, bool? IsForeign)
        {
            return Json(await supplierRepository.GetOptions(IncludeId, IsLocal, IsForeign));
        }

        public async Task<IActionResult> GetOptions2(string search, int page, int? includeId, bool? isLocal, bool? isForeign)
        {
            return Json(await supplierRepository.GetOptions2(search, page, includeId, isLocal, isForeign));
        }

        public async Task<IActionResult> GetOptionsByName2(string search, int page, int? includeId, bool? isLocal, bool? isForeign)
        {
            return Json(await supplierRepository.GetOptionsByName2(search, page, includeId, isLocal, isForeign));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Suppliers)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(SupplierVM supplierVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await supplierRepository.IsSupplierCodeExist(supplierVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Supplier code already in use";
            }
            else
            {
                await supplierRepository.AddAsync(supplierVM);
                responseModel.Status = "Success";
                responseModel.Message = "Supplier successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await supplierRepository.GetAllForDataTablesAsync(form));
        }

        public async Task<IActionResult> Details(int? id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var supplierVM =  supplierRepository.GetSupplierViewVM(id);
            if (supplierVM == null)
            {
                return NotFound();
            }
            return View(supplierVM);
        }

        public async Task<IActionResult> Create()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var supplierVM = new SupplierVM
            {
                Currencies = new SelectList(await currencyRepository.GetAllActiveCurrencyAsync(-1), "Id", "Name"),
                Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name")
            };
            return View(supplierVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierVM supplierVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            if (ModelState.IsValid)
            {
                if (await supplierRepository.IsSupplierCodeExist(supplierVM.Code))
                {
                    ModelState.AddModelError(string.Empty, "Supplier code already in use.");
                    TempData["ErrorMessage"] = "Supplier code already in use";
                }
                else
                {
                    await supplierRepository.AddAsync(supplierVM);
                    TempData["SuccessMessage"] = "Supplier successfully added";
                    return RedirectToAction(nameof(Index));
                }
            }
           // supplierVM.Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(supplierVM.AreaId), "Id", "Name", supplierVM.AreaId);
            supplierVM.Currencies = new SelectList(await currencyRepository.GetAllActiveCurrencyAsync(supplierVM.CurrencyId), "Id", "Name", supplierVM.CurrencyId);
            supplierVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(supplierVM.RegionId), "Id", "Name", supplierVM.RegionId);
            return View(supplierVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var supplierVM = await supplierRepository.GetSupplierVMAsync(id);
            if (supplierVM == null)
            {
                return NotFound();
            }
         
            supplierVM.Currencies = new SelectList(await currencyRepository.GetAllActiveCurrencyAsync(-1), "Id", "Name");
            supplierVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name");
            supplierVM.BusinessStyles = new SelectList(await businessStyleRepository.GetAllBusinessStyleAsync(-1), "Id", "Name");

            return View(supplierVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditJson(SupplierVM supplierVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            try
            {
                await supplierRepository.UpdateAsync(supplierVM);
                responseModel.Status = "Success";
                responseModel.Message = "Supplier successfully updated";
            }
            catch (Exception ex)
            {
                if (!await supplierRepository.Exists(supplierVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Supplier cannot be found";
                }
                else
                {
                    responseModel.Status = "Error";
                    responseModel.Message = SystemFormat.GetInnermostExceptionMessage(ex);
                }
            }
            return Json(responseModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            if (await supplierRepository.IsSupplierIdExistAsync(id) == false)
            {
                return NotFound();
            }
            if (await supplierRepository.IsSupplierInUse(id))
            {
                await supplierRepository.DeactivateSupplierAsync(id);
                TempData["WarningMessage"] = "Supplier has been deactivated";
            }
            else
            {
                await supplierRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Supplier successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            if (await supplierRepository.IsSupplierIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await supplierRepository.ReactivateSupplierAsync(id);
            TempData["SuccessMessage"] = "Supplier reactivated successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadFile()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new DataTable("Supplier");
            dt.Columns.AddRange(new DataColumn[14] {
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Address Line 1"),
                new DataColumn("Address Line 2"),
                new DataColumn("Region"),
                new DataColumn("Contact Person"),
                new DataColumn("Office Tel No"),
                new DataColumn("Mobile No"),
                new DataColumn("Term"),
                new DataColumn("Currency"),
                new DataColumn("BusinessStyle"),
                new DataColumn("VAT"),
                new DataColumn("TIN"),
                new DataColumn("Is Active")
            });
            var supplier = (await supplierRepository.GetAllSupplierAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var suppliers in supplier)
            {
                dt.Rows.Add(
                    suppliers.Code,
                    suppliers.Name,
                    suppliers.AddressLine1,
                    suppliers.AddressLine2,
                    suppliers.RegionName,
                    suppliers.ContactPerson,
                    suppliers.OfficeTelNo,
                    suppliers.MobileNumber,
                    suppliers.Term,
                    suppliers.CurrencyName,
                    suppliers.BusinessStyleName,
                    suppliers.VAT,
                    suppliers.TIN,
                    suppliers.IsActive
                );
            }
            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    DateTime.Now.ToString("yyyyMMdd") + " Suppliers.xlsx");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadJson(SupplierVM supplierVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Suppliers, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await supplierRepository.UploadCombinationExistAsync(supplierVM))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Already exist";
            }
            else if (await supplierRepository.IsCodeExist(supplierVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Code already in use";
            }
            else
            {
                int? RegionId = await regionRepository.GetIdAsync(supplierVM.RegionName);
                int? CurrencyId = await currencyRepository.GetIdAsync(supplierVM.CurrencyName);
                int? BusinessStyleId = await businessStyleRepository.GetIdAsync(supplierVM.BusinessStyleName);
                int? TermId = await creditTermRepository.GetIdAsync(supplierVM.Term);

                if (RegionId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Area";
                }
                else if (CurrencyId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Currency";
                }
                else
                {
                    supplierVM.RegionId = RegionId.Value;
                    supplierVM.CurrencyId = CurrencyId.Value;
                    supplierVM.BusinessStyleId = BusinessStyleId.Value;
                    supplierVM.TermId = TermId.Value;

                    await supplierRepository.AddAsync(supplierVM);
                    responseModel.Status = "Success";
                    responseModel.Message = "Supplier added";
                }
            }
            return Json(responseModel);
        }



        [HttpPost]
        public async Task<IActionResult> GetPurchasesJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Item, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await supplierRepository.GetSupplierPurchaseDTAsync(form, id));
        }

    }
}