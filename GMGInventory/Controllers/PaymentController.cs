using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Mvc;


namespace GMGInventory.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly ICustomerRepository customerRepository;

        public PaymentController(IPaymentRepository paymentRepository,
            ICustomerRepository customerRepository)
        {
            this.paymentRepository = paymentRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<ActionResult> Index()
        {
            var payments = await paymentRepository.GetAllAsync();
            return View(payments);
        }

        public async Task<ActionResult> Details(int id)
        {
            var payment = await paymentRepository.GetAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            var paymentFilter = new PaymentFilterVM() { PaymentId = payment.Id };
            return View(payment);
        }

        [HttpPost]
        public JsonResult GetListJson()
        {
            DataTablesRequestForm dtForm = new DataTablesRequestForm(Request.Form);
            return Json(paymentRepository.GetListDT(dtForm));
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentFormVM paymentVM)
        {
            if (ModelState.IsValid)
            {
                if (await paymentRepository.IsCodeExists(paymentVM.CRNumber))
                {
                    TempData["ErrorMessage"] = "CR Number already in use";
                }
                else
                {
                    await paymentRepository.AddAsync(paymentVM);
                    TempData["SuccessMessage"] = "Payment successfully created";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(paymentVM);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var payment = await paymentRepository.GetToEditAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PaymentFormVM paymentVM)
        {
            if (ModelState.IsValid)
            {
                await paymentRepository.UpdateAsync(paymentVM);
                TempData["SuccessMessage"] = "Payment successfully updated";
                return RedirectToAction(nameof(Index));
            }
            return View(paymentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await paymentRepository.IsIdExists(id) != true)
            {
                return NotFound();
            }
            if (await paymentRepository.IsInUse(id))
            {
                await paymentRepository.DeactivateAsync(id);
                TempData["WarningMessage"] = "Payment has been deactivated";
            }
            else
            {
                await paymentRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Payment successfully deleted";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivate(int id)
        {
            if (await paymentRepository.IsIdExists(id) != true)
            {
                return NotFound();
            }
            await paymentRepository.ReactivateAsync(id);
            TempData["SuccessMessage"] = "Payment successfully reactivated";
            return RedirectToAction(nameof(Index));
        }
    }
}
