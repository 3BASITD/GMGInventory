using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackController(IPackRepository packRepository) : ControllerBase
    {
        //[Authorize]
        //[HttpPost("List")]
        //public async Task<IEnumerable<PackVM>> GetList([FromBody] ListFilterCommonModel filter)
        //{
        //    return await packRepository.GetListAsync(filter);
        //}

        //[Authorize]
        //[HttpPost("Details")]
        //public async Task<PackDetailsVM?> GetDetails([FromBody] int Id)
        //{
        //    return await packRepository.GetDetailsAsync(Id);
        //}

        [Authorize]
        [HttpPost("Add")]
        public async Task<ResponseModelData<int>> AddPack([FromBody] PackFormVM form)
        {
            return await packRepository.AddAPI(form);
        }
    }
}
