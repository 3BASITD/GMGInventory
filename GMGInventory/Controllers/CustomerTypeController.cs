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
    public class CustomerTypeController : Controller
    {
        private readonly ICustomerTypeRepository customertypeRepository;
        public CustomerTypeController(ICustomerTypeRepository customertypeRepository)
        {
            this.customertypeRepository = customertypeRepository;
        }
        public async Task<IActionResult> GetOptions(int? IncludeId)
        {

            return Json(await customertypeRepository.GetOptions(IncludeId));
        }

    }
}
