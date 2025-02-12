using DocumentFormat.OpenXml.Office2010.Excel;
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
    public class ShippingController(IAccessRepository accessRepository,
                                    IShippingRepository shippingRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ShippingList, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ShippingList)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(ListFilterCustomerModel filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ShippingList, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await shippingRepository.GetListDTAsync(form, filter));
        }

        public async Task<IActionResult> Details(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ShippingList, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ShippingList)).Convert();

            var shipping = await shippingRepository.GetDetailsAsync(id);
            if (shipping == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(shipping);
        }

        public async Task<IActionResult> Create()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ShippingList, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ShippingList)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsJson(int salesOrderId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ShippingList, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await shippingRepository.GetItemsAsync(salesOrderId));
        }

        //public async Task<IActionResult> GetInvoicedSalesOrderOptions(string search, int page)
        //{
        //    return Json(await shippingRepository.GetInvoicedSalesOrderOptions(search, page));
        //}

        //public async Task<IActionResult> PrintDetails(int Id, string? Company, string? Desc, string Freight)
        //{
        //    var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ShippingList, AccessTypes.Print);
        //    if (redirectResult != null) return redirectResult;

        //    var shipping = await shippingRepository.PrintDetailsAsync(Id);


        //    //var items = await receiveRepository.GetItemListAsync(Id);

        //    if (shipping == null || Company == null)
        //    {
        //        return NotFound();
        //    }

        //    TempData["CompanyLogo"] = Company;
        //   // ViewData["Items"] = items;
        //    return View(shipping);
        //}

        public async Task<IActionResult> PrintDetails(JsonShippingModel shipping)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ShippingList, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            TempData["CompanyLogo"] = shipping.Company;

            return View(await shippingRepository.PrintDetailsAsync(shipping));
        }
    }
}
