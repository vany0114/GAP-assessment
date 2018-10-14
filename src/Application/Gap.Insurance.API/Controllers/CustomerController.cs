using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Gap.Domain.Customer.Repository;
using Gap.Insurance.API.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ViewModel = Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _mapper = mapper ?? throw new CustomerApplicationArgumentNullException(nameof(mapper));
            _customerRepository = customerRepository ?? throw new CustomerApplicationArgumentNullException(nameof(customerRepository));
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
            var customers = await _customerRepository.GetCustomersAsync();
            var customersViewModel = _mapper.Map<IEnumerable<ViewModel.Customer>>(customers);

            if (customersViewModel == null)
                return NotFound();

            return Ok(customersViewModel);
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
            var customer = await _customerRepository.GetCustomerAsync(customerId);
            var customersViewModel = _mapper.Map<ViewModel.Customer>(customer);

            if (customersViewModel == null)
                return NotFound();

            return Ok(customersViewModel);
        }
    }
}
