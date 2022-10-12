using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using ClosedXML.Excel;

namespace GMGInventory.Controllers
{
    //[Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector)]
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentRepository agentRepository;
        private readonly IAreaRepository areaRepository;
        private readonly IRegionRepository regionRepository;

        public AgentController(IAgentRepository agentRepository
            , IAreaRepository areaRepository, IRegionRepository regionRepository)
        {
            this.agentRepository = agentRepository;
            this.areaRepository = areaRepository;
            this.regionRepository = regionRepository;
        }

        public async Task<IActionResult> GetOptions2(string search, int page)
        {
            return Json(await agentRepository.GetOptions2(search, page));
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await agentRepository.GetOptions(IncludeId));
        }
        public async Task<ActionResult> Index()
        {
            AgentFilterVM agentFilterVM = new AgentFilterVM();
            agentFilterVM.Regions = new SelectList(await regionRepository.GetAllActiveRegionAsync(-1), "Id", "Name");
            ViewData["AgentFilter"] = agentFilterVM;
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetListJson(AgentFilterVM filter)
        {
            DataTablesRequestForm form = new DataTablesRequestForm(Request.Form);
            return Json(await agentRepository.GetAllForDataTablesAsync(form, filter));
        }

        //[HttpPost]
        //public async Task<JsonResult> GetListJson()
        //{
        //    DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
        //    return Json(await agentRepository.GetListDTAsync(dtForm));
        //}

        [HttpPost]
        public async Task<JsonResult> CreateJson(AgentVM agentVM)
        {
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
        public async Task<JsonResult> UploadJson(AgentVM agentVM)
        {
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
                int? AreaId = await agentRepository.GetIdAsync(agentVM.Area);
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
                    responseModel.Message = "Area added";
                }
            }
            return Json(responseModel);
        }

        [HttpPost]
        public async Task<JsonResult> EditJson(AgentVM agentVM)
        {
            ResponseModel responseModel = new ResponseModel();
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
            DataTable dt = new DataTable("Agent");
            dt.Columns.AddRange(new DataColumn[3] {
                new DataColumn("Code"),
                new DataColumn("Full Name"),
                new DataColumn("Area")
            });
            var agent = (await agentRepository.GetAllAsync()).Where(s => s.IsActive == true).ToList();
            foreach (var agents in agent)
            {
                dt.Rows.Add(agents.Code, agents.FullName, agents.AreaId);
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        DateTime.Now.ToString("yyyyMMdd") + " Agents.xlsx");
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await agentRepository.IsAgentIdExistAsync(id) == false)
            {
                return NotFound();
            }
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
            if (await agentRepository.IsAgentIdExistAsync(id) == false)
            {
                return NotFound();
            }
            await agentRepository.ReactivateAgentAsync(id);
            TempData["SuccessMessage"] = "Agent reactivated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
