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
    [Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector + "," + Roles.SystemAdmin)]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly IAreaRepository areaRepository;
        private readonly ICurrencyRepository currencyRepository;
        private readonly IRegionRepository regionRepository;

        public SupplierController(ISupplierRepository supplierRepository
            , IAreaRepository areaRepository
            , ICurrencyRepository currencyRepository
            , IRegionRepository regionRepository)
        {
            this.supplierRepository = supplierRepository;
            this.areaRepository = areaRepository;
            this.currencyRepository = currencyRepository;
            this.regionRepository = regionRepository;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await supplierRepository.GetOptions(IncludeId));
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> CreateJson(SupplierVM supplierVM)
        {
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
        public async Task<JsonResult> GetListJson()
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await supplierRepository.GetAllForDataTablesAsync(form));
        }

        public async Task<IActionResult> Details(int? id)
        {
            var supplierVM = await supplierRepository.GetSupplierViewVMAsync(id);
            if (supplierVM == null)
            {
                return NotFound();
            }
            return View(supplierVM);
        }

        public async Task<IActionResult> Create()
        {
            var supplierVM = new SupplierVM
            {
                Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(-1), "Id", "Name"),
                Currencies = new SelectList(await currencyRepository.GetAllActiveCurrencyAsync(-1), "Id", "Name"),
                Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name")
            };
            return View(supplierVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierVM supplierVM)
        {
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
            supplierVM.Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(supplierVM.AreaId), "Id", "Name", supplierVM.AreaId);
            supplierVM.Currencies = new SelectList(await currencyRepository.GetAllActiveCurrencyAsync(supplierVM.CurrencyId), "Id", "Name", supplierVM.CurrencyId);
            supplierVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(supplierVM.RegionId), "Id", "Name", supplierVM.RegionId);
            return View(supplierVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var supplierVM = await supplierRepository.GetSupplierVMAsync(id);
            if (supplierVM == null)
            {
                return NotFound();
            }
            supplierVM.Areas = new SelectList(await areaRepository.GetAllActiveAreaAsync(-1), "Id", "Name");

            supplierVM.Currencies = new SelectList(await currencyRepository.GetAllActiveCurrencyAsync(-1), "Id", "Name");
            supplierVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name");

            return View(supplierVM);
        }

        [HttpPost]
        public async Task<JsonResult> EditJson(SupplierVM supplierVM)
        {
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
                    responseModel.Message = ex.Message;
                }
            }
            return Json(responseModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
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
            DataTable dt = new DataTable("Supplier");
            dt.Columns.AddRange(new DataColumn[15] {
                new DataColumn("Code"),
                new DataColumn("Name"),
                new DataColumn("Contact Person"),
                new DataColumn("Contact Number"),
                new DataColumn("Email"),
                new DataColumn("Address Line 1"),
                new DataColumn("Address Line 2"),
                new DataColumn("Barangay"),
                new DataColumn("City/ Municipality"),
                new DataColumn("Area"),
                new DataColumn("Zip Code"),
                new DataColumn("Credit Limit"),
                new DataColumn("Currency"),
                new DataColumn("Is Foreign"),
                new DataColumn("Is Active")

            });
            var supplier = (await supplierRepository.GetAllAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var suppliers in supplier)
            {
                dt.Rows.Add(suppliers.Code, suppliers.Name, suppliers.ContactPerson, suppliers.ContactNumber, suppliers.EmailAddress,
                    suppliers.AddressLine1, suppliers.AddressLine2, suppliers.Barangay, suppliers.City, suppliers.AreaName,
                    suppliers.ZipCode, suppliers.CreditLimit, suppliers.CurrencyName, suppliers.IsForeign, suppliers.IsActive);
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
        public async Task<JsonResult> UploadJson(SupplierVM supplierVM)
        {
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
                int? AreaId = await areaRepository.GetIdAsync(supplierVM.AreaName);
                int? CurrencyId = await currencyRepository.GetIdAsync(supplierVM.CurrencyName);
                if (AreaId == null)
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
                    supplierVM.AreaId = AreaId.Value;
                    supplierVM.CurrencyId = CurrencyId.Value;
                    await supplierRepository.AddAsync(supplierVM);
                    responseModel.Status = "Success";
                    responseModel.Message = "Area added";
                }
            }
            return Json(responseModel);
        }



    }
}