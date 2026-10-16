using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class SalesOrderController(IAccessRepository accessRepository
                                    , ISystemSettingRepository settingRepository
                                    , ISalesOrderRepository salesOrderRepository) : BaseController(accessRepository)

    {
        private readonly ControllerHelper chelper = new();

        public async Task<IActionResult> GetOptions(string search, int page)
        {
            return Json(await salesOrderRepository.GetOptions(search, page));
        }

        public async Task<IActionResult> GetSalesOrderOptions(string search, int page)
        {
            return Json(await salesOrderRepository.GetSalesOrderOptions(search, page));
        }

        public async Task<IActionResult> GetSalesOrderUnservedOnlyOptions(string search, int page, int? regionId)
        {
            return Json(await salesOrderRepository.GetSalesOrderUnservedOnlyOptions(search, page, regionId));
        }

        public async Task<IActionResult> GetSalesOrderInvoicedOptions(string search, int page)
        {
            return Json(await salesOrderRepository.GetSalesOrderInvoicedOptions(search, page));
        }

        public async Task<IActionResult> GetStatusOptions()
        {
            return Json(await salesOrderRepository.GetStatusOptions());
        }

        public async Task<IActionResult> GetStatusOptions2(string search, int page)
        {
            return Json(await salesOrderRepository.GetStatusOptions2(search, page));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesOrder)).Convert();
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDTJson(SalesOrderFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;


            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesOrderRepository.GetListDTAsync(form, filter));
        }

        public async Task<IActionResult> Details(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesOrder)).Convert();
            ViewData["UserAccessSpecial"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesOrderSpecial)).Convert();
         
            var order = await salesOrderRepository.GetDetailsAsync(Id);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["SystemSettings"] = await settingRepository.GetSystemSettings();

            var dtNow = DateTime.Now;
            var newChecker = dtNow.AddMinutes(-1) <= order.OrderDateD && order.OrderDateD <= dtNow;
            if (newChecker)
            {
                TempData["SuccessMessage"] = "Order successfully submitted";
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> DetailsJson(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await salesOrderRepository.GetDetailsAsync(Id));
        }

        public async Task<IActionResult> Print(int? Id, string? Company)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;


            var order = await salesOrderRepository.PrintDetailsAsync(Id);
            if (order == null || Company == null)
            {
                return NotFound();
            }
            TempData["CompanyLogo"] = Company;
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsJson(int salesOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await salesOrderRepository.GetItemsAsync(salesOrderId));
        }

        [HttpPost]
        public async Task<IActionResult> GetBrandDiscount(int customerId, int brandId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await salesOrderRepository.GetBrandDiscountAsync(customerId, brandId));
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsDTJson(int salesOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await salesOrderRepository.GetItemsDTAsync(form, salesOrderId));
        }

        public async Task<IActionResult> Create(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesOrder)).Convert();
            ViewData["UserAccessSpecial"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesOrderSpecial)).Convert();
            ViewData["SystemSettings"] = await settingRepository.GetSystemSettings();

            var order = await salesOrderRepository.GetDetailsAsync(Id);
            if (Id != null && (order == null || order.DisapprovedBy == null || order.DisapprovedDateD == null))
            {
                return RedirectToAction("Details", new { Id = Id });
            }
            TempData["SalesOrderId"] = Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveJson(SalesOrderFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            if (form.Status == "Submitted")
            {
                var cutoffStr = await settingRepository.GetValue("SubmitCutoff");

                if (!TimeSpan.TryParse(cutoffStr, out var cutoff))
                    cutoff = new TimeSpan(14, 0, 0);   // fallback if missing/malformed

                var now = DateTime.Now.TimeOfDay;
                if (now < new TimeSpan(7, 0, 0) || now >= cutoff)
                    return Json(new { status = "Error", message = $"Submission is closed (cutoff {cutoff:hh\\:mm}). It reopens at 07:00." });
            }


            var response = await salesOrderRepository.SaveAsync(form);
            if (response.Status == "Success") TempData["SuccessMessage"] = response.Message;

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveItemsJson(SalesOrderFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;


            ResponseModel<int> responseModel = new ResponseModel<int>();
            if (form.Id != null)
            {
                await salesOrderRepository.SaveItemsAsync(form.Items, (int)form.Id);
                responseModel.Status = "Success";
                responseModel.Message = "Order item(s) successfully saved";
            }
            else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Invalid Sales Order";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDraft(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var response = await salesOrderRepository.DeleteDraft(Id);

            if (response.Status == "Success")
            {
                TempData["SuccessMessage"] = response.Message;
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveJson(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;


            ResponseModel responseModel = new ResponseModel();
            if (await salesOrderRepository.ApproveAsync(Id, ""))
            {
                responseModel.Status = "Success";
                responseModel.Message = "Order successfully approved";
            }
            else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Cannot approve order. Please try again later";
            }
            return Json(responseModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int Id, string? SlaReason)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;

            if (ModelState.IsValid)
            {
                if (await salesOrderRepository.ApproveAsync(Id, SlaReason))
                {
                    TempData["SuccessMessage"] = "Sales order approved successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Approving sales order failed";
                }
            }
            return RedirectToAction(nameof(Details), new { id = Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disapprove(int Id, string? Reason)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;

            if (ModelState.IsValid)
            {
                if (await salesOrderRepository.DisapproveAsync(Id, Reason + ""))
                {
                    TempData["SuccessMessage"] = "Sales order disapproved successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Disapproving sales order failed";
                }
            }
            return RedirectToAction(nameof(Details), new { id = Id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateServedSOItem(int salesOrderLineItemId, int quantity)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await salesOrderRepository.UpdateServedSOItem(salesOrderLineItemId, quantity))
            {
                responseModel.Status = "Success";
                responseModel.Message = "Served Quantity successfully updated";
            }
            else
            {
                responseModel.Status = "Error";
                responseModel.Message = "Cannot update served quantity. Please try again later";
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> DownloadFile(SalesOrderFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Sales Order");
            dt.Columns.AddRange([
                new DataColumn(chelper.DisplayName<SalesOrderVM>("SONumber")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("CustomerCode")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("CustomerName")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("CustomerAddress")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("Area")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("Region")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("OrderDate")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("RequiredDeliveryDate")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("Forwarder")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("TotalAmount")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("Status")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("ServiceLevelAgreement")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("WithLP")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("WithREYCONonDefective")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("WithREYCOJITLPDelivery")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("SpecialInstruction")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("Remarks")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("SA Name")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("SA KPI")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("OPS Name")),
                new DataColumn(chelper.DisplayName<SalesOrderVM>("OPS KPI")),
            ]);
            var sos = await salesOrderRepository.GetListAsync(filter);
            foreach (var so in sos)
            {
                dt.Rows.Add(so.SONumber,
                            so.CustomerCode,
                            so.CustomerName,
                            so.CustomerAddress,
                            so.Area,
                            so.Region,
                            so.OrderDate,
                            so.RequiredDeliveryDate,
                            so.Forwarder,
                            so.TotalAmount,
                            so.Status,
                            so.ServiceLevelAgreement,
                            so.WithLP ? "Yes" : "No",
                            so.WithREYCONonDefective ? "Yes" : "No",
                            so.WithREYCOJITLPDelivery,
                            so.SpecialInstruction,
                            so.Remarks,
                            so.SaName,
                            so.SaKpi,
                            so.OpsName,
                            so.OpsKpi);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Sales Order.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePod(int Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.SalesOrder, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var response = await salesOrderRepository.UpdatePod(Id);

            if (response.Status == "Success")
            {
                TempData["SuccessMessage"] = response.Message;
            }

            return Json(response);
        }

    }
}