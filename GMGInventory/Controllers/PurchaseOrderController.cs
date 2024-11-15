using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{

    [Authorize]
    public class PurchaseOrderController(IAccessRepository accessRepository
                                       , IPurchaseOrderNewRepository orderRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetTypeOptions(int? SelectedId)
        {
            return Json(await orderRepository.GetTypeOptions(SelectedId));
        }

        public async Task<IActionResult> GetDeliveryModeOptions(int? SelectedId)
        {
            return Json(await orderRepository.GetDeliveryModeOptions(SelectedId));
        }

        public async Task<IActionResult> GetPurchaseRequestOptions(int RequestTypeId, int? SelectedId)
        {
            return Json(await orderRepository.GetPurchaseRequestOptions(RequestTypeId, SelectedId));
        }

        public async Task<IActionResult> GetPurchaseRequestItemOptions(int RequestId, int? SelectedId)
        {
            return Json(await orderRepository.GetPurchaseRequestItemOptions(RequestId, SelectedId));
        }

        public async Task<IActionResult> GetNewDocumentNumber(int? TypeId)
        {
            return Json(await orderRepository.GetNewDocumentNumber(TypeId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.PurchaseOrder)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDTJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await orderRepository.GetListDTAsync(form));
        }

        public async Task<IActionResult> Create()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.PurchaseOrder, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return View();
        }
    }
}