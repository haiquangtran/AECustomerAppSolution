using AE.CustomerApp.Api.Controllers;
using AE.CustomerApp.Core;
using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Core.Interfaces;
using AE.CustomerApp.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AE.CustomerApp.Api.Test
{
    public class CustomerControllerTest
    {
        private IOptions<AppSettingsConfiguration> _appSettings;
        private Mock<ICustomerService> _customerService;
        private Mock<IMapper> _mapper;

        public CustomerControllerTest()
        {
            _customerService = new Mock<ICustomerService>();
            _mapper = new Mock<IMapper>();
            _appSettings = Options.Create(new AppSettingsConfiguration());
        }

        [Trait("CustomerController", "Get All Customers")]
        [Fact(DisplayName = "Get All Customers Returns 200 Response")]
        public void CustomerControllerTest_GetCustomers_ShouldReturn200Response()
        {
            // Arrange
            _mapper.Setup(x => x.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(It.IsAny<IEnumerable<Customer>>()))
                .Returns(new List<CustomerDto>());
            _customerService.Setup(x => x.GetCustomers()).Returns(new List<Customer>());
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetAllCustomers();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(test);
        }
    }
}
