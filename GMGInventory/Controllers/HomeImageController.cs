using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class HomeImageController(IAccessRepository accessRepository
                                   , IHomeImageRepository homeImageRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.HomeImage, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.HomeImage)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListDTJson()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.HomeImage, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await homeImageRepository.GetListDTAsync(form));
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(HomeImageFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.HomeImage, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await homeImageRepository.SaveAsync(form));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateImage(HomeImageFormVM form)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.HomeImage, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await homeImageRepository.SaveAsync(form));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int ImageId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.HomeImage, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            return Json(await homeImageRepository.DeleteAsync(ImageId));
        }

        [HttpPost]
        public async Task<IActionResult> MoveUpImage(int ImageId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.HomeImage, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await homeImageRepository.MoveUpAsync(ImageId));
        }

        [HttpPost]
        public async Task<IActionResult> MoveDownImage(int ImageId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.HomeImage, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await homeImageRepository.MoveDownAsync(ImageId));
        }
    }
}