using System.Linq;
using System.Threading.Tasks;
using Gap.Insurance.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gap.Insurance.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IInsuranceService _insuranceService;

        public CustomerController(ICustomerService customerService, IInsuranceService insuranceService)
        {
            _customerService = customerService;
            _insuranceService = insuranceService;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetCustomersAsync();
            return View(customers.ToList());
        }

        public async Task<IActionResult> Assignment(int id)
        {
            var customer = await _customerService.GetCustomerAsync(id);
            var insurances = await _insuranceService.GetInsurancesAsync();
            var model = new ViewModels.Assignment
            {
                Customer = customer,
                Insurances = insurances.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Cancellation(int id)
        {
            var customer = await _customerService.GetCustomerAsync(id);
            var insurances = await _insuranceService.GetInsurancesAsync();
            var model = new ViewModels.Assignment
            {
                Customer = customer,
                Insurances = insurances.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(ViewModels.Assignment assignment)
        {
            if (!ModelState.IsValid)
                return View("Assignment", assignment);

            await _customerService.AssignInsuranceAsync(assignment);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(ViewModels.Assignment assignment)
        {
            if (!ModelState.IsValid)
                return View("Assignment", assignment);

            await _customerService.CancelInsuranceAsync(assignment);
            return RedirectToAction("Index");
        }
    }
}
