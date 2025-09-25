using DocumentFormat.OpenXml.Bibliography;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class CurrencyController(IAccessRepository accessRepository
                                  , ICurrencyRepository currencyRepository
                                  , IAPIHM_Service apiHMService
                                  , IOptions<APIHMSettings> apiHMOptions) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await currencyRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Currency, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Currency)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCurrenciesDT()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Currency, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await currencyRepository.GetCurrenciesDT(form));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateList()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Currency, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var currencies = await apiHMService.GetCurrencies(apiHMOptions.Value.Host, apiHMOptions.Value.User, apiHMOptions.Value.Password);

            if (currencies.Status == "Error")
                return Json(currencies);

            if (currencies.NewRow.data.Count == 0)
                return Json(new ResponseModel { Status = "Error", Message = "Nothing was retreived from HashMicro." });

            var currencyToSave = new List<Currency>();
            foreach( var currency in currencies.NewRow.data)
            {
                currencyToSave.Add(new Currency
                {
                    Name = currency.name,
                    IsActive = true
                });
            }
            var saveCurrency = await currencyRepository.UpdateList(currencyToSave);

            return Json(saveCurrency);
        }
    }
}