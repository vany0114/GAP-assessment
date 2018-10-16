using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Gap.Insurance.API.Application.Exceptions;
using Gap.Insurance.API.Services;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel = Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class InsuranceController : Controller
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService ?? throw new InsuranceApplicationArgumentNullException(nameof(insuranceService));
        }

        /// <summary>
        /// Returns an insurance that matches with the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an insurance that matches with the specified id</returns>
        /// <response code="200">Returns an Insurance object that matches with the specified id</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ViewModel.Insurance), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetInsurance(int id)
        {
            var insurance = await _insuranceService.GetInsuranceAsync(id);

            if (insurance == null)
                return NotFound();

            return Ok(insurance);
        }

        /// <summary>
        /// Returns a list of insurances
        /// </summary>
        /// <returns>Returns a list of insurances</returns>
        /// <response code="200">Returns a list of insurances</response>
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ViewModel.Insurance>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetInsurances()
        {
            var insurances = await _insuranceService.GetInsurancesAsync();

            if (insurances == null || insurances.Count == 0)
                return NotFound();

            return Ok(insurances);
        }

        /// <summary>
        /// Creates a new insurance.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns the newly created insurance identifier.</returns>
        /// <response code="201">Returns the newly created trip identifier.</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateInsurance([FromBody]ViewModel.CreateInsuranceRequest request)
        {
            var insuranceId = await _insuranceService.CreateInsurance(request);
            return Created(HttpContext.Request.GetUri().AbsoluteUri, insuranceId);
        }

        /// <summary>
        /// Deletes a new insurance.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteInsurance([FromBody]ViewModel.DeleteInsuranceRequest request)
        {
            await _insuranceService.DeleteInsurance(request);
            return Ok();
        }

        /// <summary>
        /// Add a coverage to the given insurance.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddCoverageToInsurance([FromBody]ViewModel.AddCoverageRequest request)
        {
            await _insuranceService.AddCoverageToInsurance(request);
            return Ok();
        }
    }
}
