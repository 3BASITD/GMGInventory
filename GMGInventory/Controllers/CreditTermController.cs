using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GMGInventory.Data;
using GMGInventory.Common.Models;
using GMGInventory.Common.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMGInventory.Controllers
{
    //[Authorize(Roles = Roles.InventoryManager + "," + Roles.SalesDirector)]
    [Authorize]
    public class CreditTermController : Controller
    {
        private readonly ICreditTermRepository creditTermRepository;
        public CreditTermController(ICreditTermRepository creditTermRepository)
        {
            this.creditTermRepository = creditTermRepository;
        }
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {

            return Json(await creditTermRepository.GetOptions(IncludeId));
        }

    }
}
