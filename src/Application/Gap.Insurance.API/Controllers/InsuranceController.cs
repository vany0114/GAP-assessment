using System.Net;
using System.Threading.Tasks;
using Gap.Insurance.API.Application.Exceptions;
using Gap.Insurance.API.Services;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using ViewModel = Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Controllers
{
    [Route("api/v1/[controller]")]
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
        /// <param name="insuranceId"></param>
        /// <returns>Returns an insurance that matches with the specified id</returns>
        /// <response code="200">Returns an Insurance object that matches with the specified id</response>
        [Route("getbyid")]
        [HttpGet]
        [ProducesResponseType(typeof(ViewModel.Insurance), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetInsurance(int insuranceId)
        {
            var insurance = await _insuranceService.GetInsuranceAsync(insuranceId);

            if (insurance == null)
                return NotFound();

            return Ok(insurance);
        }

        /// <summary>
        /// Creates a new insurance.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns the newly created insurance identifier.</returns>
        /// <response code="201">Returns the newly created trip identifier.</response>
        [Route("create")]
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
        [Route("delete")]
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
        [Route("addcoverage")]
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
