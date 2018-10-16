using System;
using System.Linq;
using System.Threading.Tasks;
using Gap.Insurance.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gap.Insurance.Web.Controllers
{
    [Authorize]
    public class InsuranceController : Controller
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        public async Task<IActionResult> Index()
        {
            var insurances = await _insuranceService.GetInsurancesAsync();
            return View(insurances?.ToList());
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _insuranceService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult New()
        {
            return View("Create", new ViewModels.Insurance { StartDate = DateTime.UtcNow.AddDays(1) });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ViewModels.Insurance insurance)
        {
            if (!ModelState.IsValid)
                return View("Create", insurance);

            await _insuranceService.CreateAsync(insurance);
            return RedirectToAction("Index");
        }
    }
}
