using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AE.CustomerApp.Core;
using AE.CustomerApp.Core.Constants;
using AE.CustomerApp.Core.Dto;
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
        public CustomerController(IMapper mapper, IOptions<AppSettingsConfiguration> appSettings) : base(mapper, appSettings)
        {
        }

        // GET: api/{version}/Customers
        [HttpGet("Customers")]
        [SwaggerOperation(
            Summary = "Get all customers", 
            Description = "",
            OperationId = "GetAllCustomers", 
            Tags = new[] { "Customers" })]
        [SwaggerResponse((int)HttpStatusCode.OK, "Returns all customers", typeof(IEnumerable<CustomerDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server error - cannot get customers")]
        public IEnumerable<string> GetAllCustomers()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Customer/5
        [HttpGet("Customers/{id}")]
        [SwaggerOperation(OperationId = "GetCustomer", Summary = "Get all customers")]
        public string GetCustomer(int id)
        {
            return "value";
        }

        // POST: api/Customer
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
