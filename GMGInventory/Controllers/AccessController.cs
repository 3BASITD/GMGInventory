using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAccessRepository accessRepository;


        public AccessController(IAccessRepository accessRepository)
        {
            this.accessRepository = accessRepository;
        }

        [HttpPost]
        public async Task<JsonResult> GetUserAccessesDT(string userId)
        {
            var form = new DataTablesRequestForm(Request.Form);
            return Json(await accessRepository.GetUserAccessesDTAsync(form, userId));
        }
    }
}