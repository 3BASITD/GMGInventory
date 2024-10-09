using ClosedXML.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace GMGInventory.Controllers
{
    [Authorize]
    public class AgentController(IAccessRepository accessRepository
                               , IAgentRepository agentRepository
                               , IAreaRepository areaRepository
                               , IRegionRepository regionRepository) : BaseController(accessRepository)
    {
        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await agentRepository.GetOptions2(search, page));
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await agentRepository.GetOptions(IncludeId));
        }

        public async Task<IActionResult> GetOptionsWithOrder(string search, int page, int? CustomerId, DateTime? DateFrom, DateTime? DateTo)
        {
            return Json(await agentRepository.GetOptionsWithOrder(search, page, CustomerId, DateFrom, DateTo));
        }

        public async Task<IActionResult> Index()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Agents, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            ViewData["UserAccess"] = new ViewsAccessData(await GetAccessAsync(PageNames.Agents)).Convert();

            var agentFilterVM = new AgentFilterVM()
            {
                Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name")
            };
            ViewData["AgentFilter"] = agentFilterVM;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetListJson(AgentFilterVM filter)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Agents, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var form = new DataTablesRequestForm(Request.Form);
            return Json(await agentRepository.GetAllForDataTablesAsync(form, filter));
        }

        public async Task<IActionResult> Details(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Agents, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            var agent = agentRepository.GetAgentDtlVM(id);
            if (agent == null)
                return NotFound();

            return View(agent);
        }

        [HttpPost]
        public async Task<IActionResult> GetDetailsJson(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Agents, AccessTypes.View);
            if (redirectResult != null) return redirectResult;

            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await agentRepository.GetAgentDTAsync(form, id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJson(AgentVM agentVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Agents, AccessTypes.Add);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await agentRepository.IsCodeExist(agentVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Agent code already in use";
            }
            else
            {
                await agentRepository.AddAsync(agentVM);
                responseModel.Status = "Success";
                responseModel.Message = "Agent successfully added";
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadJson(AgentVM agentVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Agents, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            ResponseModel responseModel = new ResponseModel();
            if (await agentRepository.UploadCombinationExistAsync(agentVM))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Already exist";
            }
            else if (await agentRepository.IsCodeExist(agentVM.Code))
            {
                responseModel.Status = "Error";
                responseModel.Message = "Code already in use";
            }
            else
            {
                int? AreaId = await areaRepository.GetIdAsync(agentVM.Area);
                if (AreaId == null)
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Unknown Area";
                }
                else
                {
                    agentVM.AreaId = AreaId.Value;
                    await agentRepository.AddAsync(agentVM);
                    responseModel.Status = "Success";
                    responseModel.Message = "Agent added";
                }
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditJson(AgentVM agentVM)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Agents, AccessTypes.Edit);
            if (redirectResult != null) return redirectResult;

            var responseModel = new ResponseModel();
            try
            {
                await agentRepository.UpdateAsync(agentVM);
                responseModel.Status = "Success";
                responseModel.Message = "Agent successfully updated";
            }
            catch (Exception ex)
            {
                if (!await agentRepository.Exists(agentVM.Id))
                {
                    responseModel.Status = "Error";
                    responseModel.Message = "Agent cannot be found";
                }
                else
                {
                    responseModel.Status = "Error";
                    responseModel.Message = ex.Message;
                }
            }
            return Json(responseModel);
        }

        public async Task<IActionResult> DownloadFile()
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Agents, AccessTypes.UploadDownload);
            if (redirectResult != null) return redirectResult;

            var dt = new DataTable("Agent");
            dt.Columns.AddRange([
                new DataColumn("Code"),
                new DataColumn("First Name"),
                new DataColumn("Middle Name"),
                new DataColumn("Last Name"),
                new DataColumn("Area")
            ]);

            var agent = (await agentRepository.GetAllAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var agents in agent)
            {
                dt.Rows.Add(agents.Code, agents.FirstName, agents.MiddleName == null ? "" : " " + agents.MiddleName, agents.LastName, agents.AreaId);
            }
            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Agents.xlsx");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var redirectResult = await CheckAccessAndRedirectAsync(PageNames.Agents, AccessTypes.Delete);
            if (redirectResult != null) return redirectResult;

            if (await agentRepository.IsAgentIdExistAsync(id) == false)
                return NotFound();

            if (await agentRepository.IsAgentInUse(id))
            {
                await agentRepository.DeactivateAgentAsync(id);
                TempData["WarningMessage"] = "Agent has been deactivated";
            }
            else
            {
                await agentRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Agent successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            var redirectResultAdd = await CheckAccessAndRedirectAsync(PageNames.Agents, AccessTypes.Add);
            var redirectResultEdit = await CheckAccessAndRedirectAsync(PageNames.Agents, AccessTypes.Edit);
            if (redirectResultAdd != null && redirectResultEdit != null) return redirectResultAdd;

            if (await agentRepository.IsAgentIdExistAsync(id) == false)
                return NotFound();

            await agentRepository.ReactivateAgentAsync(id);
            TempData["SuccessMessage"] = "Agent reactivated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}