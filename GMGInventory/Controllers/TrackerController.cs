using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{

    [Authorize]
    public class TrackerController(IAccessRepository accessRepository
                                 , ITrackerRepository trackerRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.TrackerTask)).Convert();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetTasksDT(TaskFilter filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await trackerRepository.GetTasksDT(form, filter));
        }

        public async Task<IActionResult> Details(Guid Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["TaskUserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.TrackerTask)).Convert();
            ViewData["CommentUserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.TrackerComment)).Convert();
            ViewData["IssueUserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.TrackerIssue)).Convert();

            var details = await trackerRepository.GetTask(Id);
            if (details == null) return NotFound();

            return View(details);
        }

        public async Task<IActionResult> DetailsJson(Guid Id)
        {
            var details = await trackerRepository.GetTaskDetails(Id);
            if (details == null) return NotFound();

            return Json(details);
        }

        [HttpPost]
        public async Task<IActionResult> GetHistories(Guid TaskId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.GetHistories(TaskId));
        }

        [HttpPost]
        public async Task<IActionResult> GetComments(Guid TaskId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.GetComments(TaskId));
        }

        [HttpPost]
        public async Task<IActionResult> GetIssues(Guid TaskId)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.GetIssues(TaskId));
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TaskParameterVM task)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.AddTask(task));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(TaskParameterVM task)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.UpdateTask(task));
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseTaskPriority(Guid Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.IncreaseTaskPriority(Id));
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseTaskPriority(Guid Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.DecreaseTaskPriority(Id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(Guid Id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerTask, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.DeleteTask(Id));
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentParameterVM comment)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerComment, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.AddComment(comment));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComment(CommentParameterVM comment)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerComment, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.UpdateComment(comment));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(CommentParameterVM comment)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerComment, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.DeleteComment(comment));
        }

        [HttpPost]
        public async Task<IActionResult> AddIssue(IssueParameterVM issue)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerIssue, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.AddIssue(issue));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIssue(IssueParameterVM issue)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerIssue, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.UpdateIssue(issue));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIssue(IssueParameterVM issue)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.TrackerIssue, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            return Json(await trackerRepository.DeleteIssue(issue));
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile File, string Entity)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(Entity.ToLower() == "issue" ? PageNames.TrackerIssue
                                                                 : Entity.ToLower() == "comment" ? PageNames.TrackerComment
                                                                 : PageNames.TrackerTask, AccessTypes.UploadDownload);

            //return Json(await trackerRepository.UploadImage(File));
            return Json(new { success = true, fileUrl = "/images/DevTask/" + trackerRepository.UploadImage(File) });

        }
    }
}