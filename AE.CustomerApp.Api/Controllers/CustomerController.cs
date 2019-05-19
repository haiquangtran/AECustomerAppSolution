using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AE.CustomerApp.Core;
using AE.CustomerApp.Core.Constants;
using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Core.Interfaces;
using AE.CustomerApp.Infra.IoC.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        // GET: api/{version}/Customers
        [HttpGet("customers")]
        [ValidateModelState]
        [SwaggerOperation(
            Summary = "Get all customers", 
            Description = "",
            OperationId = "GetAllCustomers", 
            Tags = new[] { "Customers" })]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns all customers", typeof(IEnumerable<CustomerReponseDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server error - cannot get customers")]
        public IActionResult GetAllCustomers()
        {
            // TODO: error handling

            var customers = _customerService.GetCustomers();

            return Ok(customers);
        }

        // GET: api/customer/5
        [HttpGet("customer/{id}")]
        [ValidateModelState]
        [SwaggerOperation(OperationId = "GetCustomer", Summary = "Get all customers")]
        public IActionResult GetCustomer(int id)
        {
            var customers = _customerService.GetCustomers();

            return Ok(customers);
        }

        // POST: api/customer
        [HttpPost("customer")]
        [ValidateModelState]
        public IActionResult CreateCustomer([FromBody, Required] CreateCustomerRequestDto request)
        {
            // TODO: error handling
            _customerService.AddCustomer(request);

            var result = _customerService.SaveChanges();
           
            return Ok(request);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        [ValidateModelState]
        public void UpdateCustomer(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ValidateModelState]
        public void Delete(int id)
        {
        }
    }
}
