using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
   [Authorize]
    public class KYCController(IAccessRepository accessRepository
                             , IKYCRepository kycRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetKYCsDT()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await kycRepository.GetKYCsDT(form));
        }

        public async Task<IActionResult> Customer(int? Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.KYC)).Convert();

            var details = Id == null ? new KYCVM() : await kycRepository.GetDetails(Id.Value);

            return View(details);
        }

        [HttpPost]
        public async Task<IActionResult> SaveDetails(KYCParam param)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await kycRepository.SaveDetails(param));
        }

        [HttpPost]
        public async Task<IActionResult> ApproveDetails(int CustomerId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;

            return Json(await kycRepository.ApproveDetails(CustomerId));
        }

        [HttpPost]
        public async Task<IActionResult> DisapproveDetails(int CustomerId, string Reason)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.ApproveDisapprove);
            if (redirectResult != null) return redirectResult;

            return Json(await kycRepository.DisapproveDetails(CustomerId, Reason));
        }

        [HttpPost]
        public async Task<IActionResult> EditDetails(int CustomerId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await kycRepository.EditDetails(CustomerId));
        }

        [HttpPost]
        public async Task<IActionResult> GetKYCSignatoriesDT(int CustomerId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await kycRepository.GetKYCSignatoriesDT(form, CustomerId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveSignatory(KYCSignatoryModel param)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            var responseModel = await kycRepository.SaveSignatory(param);
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSignatory(int SignatoryId, int KYCId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var responseModel = await kycRepository.DeleteSignatory(SignatoryId, KYCId);
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetKYCClientsDT(int CustomerId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await kycRepository.GetKYCClientsDT(form, CustomerId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveClient(KYCClientModel param)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            var responseModel = await kycRepository.SaveClient(param);
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClient(int ClientId, int KYCId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var responseModel = await kycRepository.DeleteClient(ClientId, KYCId);
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetKYCBanksDT(int CustomerId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new(Request.Form);
            return Json(await kycRepository.GetKYCBanksDT(form, CustomerId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveBank(KYCBankModel param)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            var responseModel = await kycRepository.SaveBank(param);
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBank(int BankId, int KYCId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.KYC, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            var responseModel = await kycRepository.DeleteBank(BankId, KYCId);
            return Json(responseModel);
        }
    }
}