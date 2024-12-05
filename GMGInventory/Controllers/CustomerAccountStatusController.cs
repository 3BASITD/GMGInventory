using GMGInventory.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GMGInventory.Controllers
{
    public class CustomerAccountStatusController : Controller
    {
        private readonly ICustomerAccountStatusRepository customerAccountStatusRepository;
        public CustomerAccountStatusController(ICustomerAccountStatusRepository customerAccountStatusRepository)
        {
            this.customerAccountStatusRepository = customerAccountStatusRepository;
        }

        public async Task<IActionResult> GetOptions(int? IncludeId)
        {
            return Json(await customerAccountStatusRepository.GetOptions(IncludeId));
        }
    }
}
