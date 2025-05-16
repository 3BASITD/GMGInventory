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
    public class TransferController(IAccessRepository accessRepository
                                  , IItemTransferRepository itemTransferRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.WarehouseTransfer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.WarehouseTransfer)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWarehouseTransferJson(int? WarehouseId, ListFilterCommonModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.WarehouseTransfer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            if (WarehouseId == null)
                return Json(await itemTransferRepository.GetWarehouseTransfersDTAsync(form, filter));
            else
                return Json(await itemTransferRepository.GetWarehouseTransfersDTAsync(form, WarehouseId.Value, filter));
        }

        public async Task<IActionResult> Details(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.WarehouseTransfer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;
           
            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.WarehouseTransfer)).Convert();

            var transfer = await itemTransferRepository.GetTransferVMAsync(Id);
            if (transfer == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var dtNow = DateTime.Now;
            var newChecker = dtNow.AddMinutes(-1) <= transfer.TransferDateD && transfer.TransferDateD <= dtNow;
            if (newChecker)
            {
                TempData["SuccessMessage"] = "Transfer successfully added";
            }

            return View(transfer);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemTransferJson(int? TransferId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.WarehouseTransfer, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await itemTransferRepository.GetItemTransfersDTAsync(form, TransferId));
        }

        public async Task<IActionResult> Create()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.WarehouseTransfer, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.WarehouseTransfer)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(TransferFormVM formVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.WarehouseTransfer, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel<int> responseModel = new ResponseModel<int>();
            int transferId = await itemTransferRepository.AddAsync(formVM);
            responseModel.Status = "Success";
            responseModel.Message = "Transfer successfully added";
            responseModel.NewRow = transferId;
            return Json(responseModel);
        }

        public async Task<IActionResult> DownloadFile(ListFilterChequeModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.WarehouseTransfer, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            DataTable dt = new("WarehouseTransfer");
            dt.Columns.AddRange([
                new DataColumn("Transfer Number"),
                new DataColumn("Source Warehouse"),
                new DataColumn("Destination Warehouse"),
                new DataColumn("ItemCount"),
                new DataColumn("Transfer Date"),
                new DataColumn("Remarks"),
                new DataColumn("CreatedBy")

            ]);
            var transfers = (await itemTransferRepository.GetAllTransferAsync(filter)).ToList();
            foreach (var transfer in transfers)
            {
                dt.Rows.Add(
                    transfer.Id,
                    transfer.Source,
                    transfer.Destination,
                    transfer.ItemCount,
                    transfer.TransferDate,
                    transfer.Remarks,
                    transfer.CreatedBy);
            }
            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            DateTime.Now.ToString("yyyyMMdd") + " WarehouseTransfers.xlsx");
        }

    }
}