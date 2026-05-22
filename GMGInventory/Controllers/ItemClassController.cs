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
    public class ItemClassController(IAccessRepository accessRepository
                                   , IItemClassRepository itemClassRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await itemClassRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await itemClassRepository.GetOptions2(search, page));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemClass, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ItemClass)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDT()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemClass, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await itemClassRepository.GetListDTAsync(form));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(GenericAuditVM iclass)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemClass, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await itemClassRepository.AddAsync(iclass));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(GenericAuditVM iclass)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemClass, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await itemClassRepository.UpdateAsync(iclass));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrDeactivateAsync(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ItemClass, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            return Json(await itemClassRepository.DeleteOrDeactivateAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> ReactivateAsync(int? Id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.ItemClass, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.ItemClass, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            return Json(await itemClassRepository.ReactivateAsync(Id));
        }
    }
}