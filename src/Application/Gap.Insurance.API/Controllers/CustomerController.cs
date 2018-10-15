using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Gap.Insurance.API.Application.Exceptions;
using Gap.Insurance.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel = Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new CustomerApplicationArgumentNullException(nameof(customerService));
        }

        /// <summary>
        /// Returns all of the customers
        /// </summary>
        /// <returns>Returns all of the customers</returns>
        /// <response code="200">Returns a list of Customer object.</response>
        [Route("get")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ViewModel.Customer>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomersAsync();

            if (customers == null)
                return NotFound();

            return Ok(customers);
        }

        /// <summary>
        /// Returns a customer that matches with the specified id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Returns a customer that matches with the specified id</returns>
        /// <response code="200">Returns a Customer object that matches with the specified id</response>
        [Route("getbyid")]
        [HttpGet]
        [ProducesResponseType(typeof(ViewModel.Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            var customer = await _customerService.GetCustomerAsync(customerId);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        /// <summary>
        /// Add an insurance to the given customer.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        [Route("assigninsurance")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AssignInsurance([FromBody]ViewModel.AssignCancelInsuranceRequest request)
        {
            await _customerService.AssignInsurance(request);
            return Ok();
        }

        /// <summary>
        /// Cancel an insurance to the given customer.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        [Route("cancelinsurance")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CancelInsurance([FromBody]ViewModel.AssignCancelInsuranceRequest request)
        {
            await _customerService.CancelInsurance(request);
            return Ok();
        }
    }
}
