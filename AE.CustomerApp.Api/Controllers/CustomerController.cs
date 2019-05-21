using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using AE.CustomerApp.Core;
using AE.CustomerApp.Core.Constants;
using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Core.Interfaces;
using AE.CustomerApp.Domain.Models;
using AE.CustomerApp.Infra.IoC.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace AE.CustomerApp.Api.Controllers
{
    [Route(ApiConstants.ApiBaseRoute)]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService,
            IMapper mapper, IOptions<AppSettingsConfiguration> appSettings) : base(mapper, appSettings)
        {
            _customerService = customerService;
        }

        // GET: api/v1/customers
        [HttpGet("customers")]
        [ValidateModelState]
        [SwaggerOperation(
            Summary = "Get all customers",
            Description = "",
            OperationId = "GetAllCustomers",
            Tags = new[] { "Customers" })]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns all customers", typeof(IEnumerable<CustomerDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server error - cannot get customers")]
        public IActionResult GetCustomers()
        {
            var customers = _customerService.GetCustomers();

            var result = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
            return Ok(result);
        }

        // GET: api/v1/customer/searchCustomers?{name}
        [HttpGet("customer/searchCustomers")]
        [ValidateModelState]
        [SwaggerOperation(
            Summary = "Search customers by partial name match (first or last name)",
            Description = "",
            OperationId = "SearchCustomersByName",
            Tags = new[] { "Customers" })]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns customers with the matched name", typeof(CustomerDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server error - cannot search customers by name")]
        public IActionResult FindCustomersByName([FromQuery, Required, SwaggerParameter("Name of customer")] string name)
        {
            var customers = _customerService.FindCustomers(name);

            var result = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
            return Ok(result);
        }

        // GET: api/v1/customer/5
        [HttpGet("customer/{id}")]
        [ValidateModelState]
        [SwaggerOperation(
            Summary = "Get customer by id",
            OperationId = "GetCustomerById",
            Tags = new[] { "Customers" })]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns customer with the matching id", typeof(CustomerDto))]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Cannot find customer")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server error - cannot get customer")]
        public IActionResult GetCustomerById(
            [SwaggerParameter("Customer id", Required = true)] int id
            )
        {
            var customer = _customerService.GetCustomer(id);

            if (customer == null)
                return NoContent();

            var result = _mapper.Map<Customer, CustomerDto>(customer);
            return Ok(result);
        }

        // POST: api/v1/customer
        [HttpPost("customer")]
        [ValidateModelState]
        [SwaggerOperation(
            Summary = "Create a new customer",
            OperationId = "CreateCustomer",
            Tags = new[] { "Customers" })]
        [SwaggerResponse((int)HttpStatusCode.Created, "Customer created", typeof(CustomerDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid customer model", typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server error - cannot create customer")]
        public IActionResult CreateCustomer(
            [FromBody, Required, SwaggerParameter("Details of new customer")] CreateCustomerRequestDto request
            )
        {
            var customer = _customerService.AddCustomer(request);
            var customerApiRoute = $"{ApiConstants.ApiBaseRoute}/customer/{customer.Id}";

            var result = _mapper.Map<Customer, CustomerDto>(customer);
            return Created(customerApiRoute, result);
        }

        // PUT: api/v1/customer/5
        [HttpPut("customer/{id}")]
        [ValidateModelState]
        [SwaggerOperation(
            Summary = "Update customer",
            OperationId = "UpdateCustomer",
            Tags = new[] { "Customers" })]
        [SwaggerResponse((int)HttpStatusCode.OK, "Customer successfully updated", typeof(CustomerDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid customer model", typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Customer not found")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server error - cannot update customer")]
        public IActionResult UpdateCustomer(
            [Required, SwaggerParameter("customer id")] int id,
            [FromBody, Required, SwaggerParameter("Updated details of customer")] UpdateCustomerRequestDto request
            )
        {
            var customer = _customerService.GetCustomer(id);

            if (customer == null)
                return NotFound();

            var updatedCustomer = _customerService.UpdateCustomer(customer, request);

            var result = _mapper.Map<Customer, CustomerDto>(updatedCustomer);
            return Ok(result);
        }

        // DELETE: api/v1/customer/5
        [HttpDelete("customer/{id}")]
        [ValidateModelState]
        [SwaggerOperation(
            Summary = "Delete customer",
            OperationId = "DeleteCustomer",
            Tags = new[] { "Customers" })]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Customer successfully deleted", typeof(CustomerDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Customer not found", typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server error - cannot delete customer")]
        public IActionResult DeleteCustomer([Required, SwaggerParameter("customer id")] int id)
        {
            var customer = _customerService.GetCustomer(id);

            if (customer == null)
                return NotFound();

            _customerService.RemoveCustomer(customer);

            return NoContent();
        }
    }
}
