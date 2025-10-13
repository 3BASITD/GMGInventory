using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Humanizer.On;

namespace GMGInventory.Controllers
{

    [Authorize]
    public class ServiceLevelAgreementController(IAccessRepository accessRepository
                                 , IServiceLevelAgreementRepository serviceLevelAgreementRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await serviceLevelAgreementRepository.GetOptions2(search, page));
        }
        
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ServiceLevelAgreement, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.ServiceLevelAgreement)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetServiceLevelAgreementsDT()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ServiceLevelAgreement, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await serviceLevelAgreementRepository.GetServiceLevelAgreementsDT(form));
        }

        [HttpPost]
        public async Task<IActionResult> AddServiceLevelAgreement(ServiceLevelAgreementVM serviceLevelAgreement)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ServiceLevelAgreement, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await serviceLevelAgreementRepository.AddServiceLevelAgreement(serviceLevelAgreement));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateServiceLevelAgreement(ServiceLevelAgreementVM serviceLevelAgreement)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ServiceLevelAgreement, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await serviceLevelAgreementRepository.UpdateServiceLevelAgreement(serviceLevelAgreement));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrDeactivateServiceLevelAgreement(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.ServiceLevelAgreement, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            return Json(await serviceLevelAgreementRepository.DeleteOrDeactivateServiceLevelAgreement(Id));
        }

        [HttpPost]
        public async Task<IActionResult> ReactivateServiceLevelAgreement(int? Id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.ServiceLevelAgreement, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.ServiceLevelAgreement, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            return Json(await serviceLevelAgreementRepository.ReactivateServiceLevelAgreement(Id));
        }
    }
}