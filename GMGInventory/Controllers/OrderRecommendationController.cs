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
    public class OrderRecommendationController(IAccessRepository accessRepository
                                             , IOrderRecommendationRepository orderRepository) : BaseController(accessRepository)
    {

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.OrderRecommendation, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.SalesInvoice)).Convert();
            ViewData["PurchaseHistory"] = await orderRepository.GetPurchaseHistories();
            ViewData["IssuanceHistory"] = await orderRepository.GetIssuanceHistories();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetOrderRecommendationDT(OrderRecommendationFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.OrderRecommendation, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(await orderRepository.GetOrderRecommendationsDT(dtForm, filter));
        }

        [HttpPost]
        public async Task<IActionResult> AddRequestAsync(IList<OrderRecommendationNewItemsVM> recos)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.OrderRecommendation, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await orderRepository.AddRequestAsync(recos));
        }
    }
}