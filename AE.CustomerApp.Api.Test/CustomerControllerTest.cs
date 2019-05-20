using AE.CustomerApp.Api.Controllers;
using AE.CustomerApp.Core;
using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Core.Interfaces;
using AE.CustomerApp.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
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

        [Trait("CustomerController", "Get Customers")]
        [Fact(DisplayName = "Get Customers Returns 200 Response")]
        public void CustomerControllerTest_GetCustomers_ShouldReturn200Response()
        {
            // Arrange
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetCustomers();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(test);
        }

        [Trait("CustomerController", "Get Customers")]
        [Fact(DisplayName = "Get Customers Calls Get From Customer Repository")]
        public void CustomerControllerTest_GetCustomers_ShouldCallGetFromCustomerRepository()
        {
            // Arrange
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetCustomers();

            // Assert
            _customerService.Verify(m => m.GetCustomers(), Times.Once());
        }

        [Trait("CustomerController", "Get Customers")]
        [Fact(DisplayName = "Get Customers Returns Multiple Customers")]
        public void CustomerControllerTest_GetCustomers_ShouldReturnMultipleCustomers()
        {
            // Arrange
            var expectedResult = new List<CustomerDto>() {
                new CustomerDto() { Id = 1 },
                new CustomerDto() { Id = 2 },
            };
            _mapper.Setup(m => m.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(It.IsAny<IEnumerable<Customer>>()))
                .Returns(expectedResult);
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetCustomers();
            
            // Assert
            var result = test as OkObjectResult;
            var customers = Assert.IsType<List<CustomerDto>>(result.Value);
            Assert.Equal(expectedResult.Count, customers.Count);
        }


    }
}
