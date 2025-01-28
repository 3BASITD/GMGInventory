using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class ChequeController(IAccessRepository accessRepository, 
                                 IChequeRepository chequeRepository,
                                 IChequeStatusRepository chequeStatusRepository,
                                 ICustomerRepository customerRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Cheque, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Cheque)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(ListFilterChequeModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Cheque, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await chequeRepository.GetAllForDataTablesAsync(form, filter));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(ChequeVM chequeVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Cheque, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();

            await chequeRepository.AddAsync(chequeVM);
            responseModel.Status = "Success";
            responseModel.Message = "Cheque successfully added";
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditJson(ChequeVM chequeVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Cheque, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            try
            {
                await chequeRepository.UpdateAsync(chequeVM);
                responseModel.Status = "Success";
                responseModel.Message = "Cheque successfully updated";
            }
            catch (Exception ex)
            {
                if (!await chequeRepository.Exists(chequeVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Cheque cannot be found";
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
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Cheque, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var reason = await chequeRepository.GetAsync(id);
            if (reason == null)
            {
                return NotFound();
            }
            
            await chequeRepository.DeleteAsync(id);
            TempData["SuccessMessage"] = "Cheque successfully deleted";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadFile(ListFilterChequeModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Cheque, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("Cheque");
            dt.Columns.AddRange([
                new DataColumn("CollectDate"),
                new DataColumn("RefNo"),
                new DataColumn("CustomerCode"),
                new DataColumn("CheckDate"),
                new DataColumn("Check"),
                new DataColumn("Branch"),
                new DataColumn("CheckNo"),
                new DataColumn("Amount"),
                new DataColumn("Status")

            ]);
            var cheques = (await chequeRepository.GetAllChequeAsync(filter)).ToList();
            foreach (var cheque in cheques)
            {
                dt.Rows.Add(cheque.CollectDate,
                    cheque.RefNo,
                    cheque.CustomerCode,
                    cheque.CheckDate,
                    cheque.Check,
                    cheque.Branch,
                    cheque.CheckNo,
                    cheque.Amount,
                    cheque.StatusCode);
            }
            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            DateTime.Now.ToString("yyyyMMdd") + " Cheques.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> UploadJson(ChequeVM chequeVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Cheque, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new();
            if (await chequeRepository.UploadCombinationExistAsync(chequeVM))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Already exist";
            }
            else if (await chequeRepository.IsRefNoExistAsync(chequeVM.RefNo))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Ref No already in use";
            }
            else
            {
                int? CustomerId = await customerRepository.GetIdByCodeAsync(chequeVM.CustomerCode);
                int? StatusId = await chequeStatusRepository.GetIdByCodeAsync(chequeVM.StatusCode);

                if (CustomerId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Customer";
                }
                else
                {
                    chequeVM.CustomerId = CustomerId.Value;
                    chequeVM.StatusId = StatusId.Value;

                    await chequeRepository.AddAsync(chequeVM);
                    responseModel.Status = "Success";
                    responseModel.Message = "Cheque added";
                }
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> PrintCheques(ListFilterChequeModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Cheque, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            return View(await chequeRepository.PrintCheque(filter));
        }
    }
}
