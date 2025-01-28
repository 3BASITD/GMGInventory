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
    public class ReceptacleController(IAccessRepository accessRepository
                                    , IPackRepository packRepository
                                    , IReceptacleRepository receptacleRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Receptacle, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDTJson(ReceptacleFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Receptacle, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await receptacleRepository.GetListDTAsync(form, filter));
        }

        public async Task<IActionResult> Details(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Receptacle, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Receptacle)).Convert();

            var pack = await receptacleRepository.GetDetailsAsync(Id ?? -1);
            if (pack == null)
            {
                return NotFound();
            }
            return View(pack);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsJson(int packId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Receptacle, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return Json(await packRepository.GetItemsAsync(packId));
        }

        public async Task<IActionResult> Print(int Id, int BoxNumber, string? Company)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Receptacle, AccessTypes.Print);
            if (redirectResult != null) return redirectResult;

            var details = await receptacleRepository.GetPrintDetailsAsync(Id, BoxNumber);

            if (details == null || Company == null)
            {
                return NotFound();
            }

            await receptacleRepository.SaveReceptacleHistory(details.Id, details.BoxNumber, details.DestinationId, Company);

            TempData["CompanyLogo"] = Company;
            return View(details);
        }

        [HttpPost]
        public async Task<IActionResult> GetReceptaclePrintHistoriesDTJson(int packId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Receptacle, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await receptacleRepository.GetReceptaclePrintHistoriesDTAsync(form, packId));
        }
    }
}