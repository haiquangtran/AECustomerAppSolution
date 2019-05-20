using AE.CustomerApp.Api.Controllers;
using AE.CustomerApp.Core;
using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Core.Interfaces;
using AE.CustomerApp.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
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

        [Trait("CustomerController", "GetCustomers")]
        [Fact(DisplayName = "Get Customers Returns 200 Response")]
        public void CustomerControllerTest_GetCustomers_Returns200Response()
        {
            // Arrange
            const int expectedStatusCode = StatusCodes.Status200OK;
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetCustomers();

            // Assert
            var result = Assert.IsType<OkObjectResult>(test);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Trait("CustomerController", "GetCustomers")]
        [Fact(DisplayName = "Get Customers Calls Get From Customer Repository")]
        public void CustomerControllerTest_GetCustomers_CallsGetFromCustomerRepository()
        {
            // Arrange
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetCustomers();

            // Assert
            _customerService.Verify(m => m.GetCustomers(), Times.Once());
        }

        [Trait("CustomerController", "GetCustomers")]
        [Fact(DisplayName = "Get Customers Returns Multiple Customers")]
        public void CustomerControllerTest_GetCustomers_ReturnsMultipleCustomers()
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

        [Trait("CustomerController", "GetCustomerById")]
        [Fact(DisplayName = "No customer found by id returns 204 response")]
        public void CustomerControllerTest_GetCustomerById_NoCustomerFound_Returns204Response()
        {
            // Arrange
            const int expectedStatusCode = StatusCodes.Status204NoContent;
            var id = 1;
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetCustomerById(id);

            // Assert
            var result = Assert.IsType<NoContentResult>(test);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Trait("CustomerController", "GetCustomerById")]
        [Fact(DisplayName = "Customer found by id returns 200 response")]
        public void CustomerControllerTest_GetCustomerById_CustomerFound_Returns200Response()
        {
            // Arrange
            const int expectedStatusCode = StatusCodes.Status200OK;
            var id = 1;
            _customerService.Setup(c => c.GetCustomer(It.IsAny<int>())).Returns(new Customer());
            _mapper.Setup(m => m.Map<Customer, CustomerDto>(It.IsAny<Customer>())).Returns(new CustomerDto());
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetCustomerById(id);

            // Assert
            var result = Assert.IsType<OkObjectResult>(test);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Trait("CustomerController", "searchCustomers")]
        [Fact(DisplayName = "GetCustomerById calls GetCustomer from Customer Repository")]
        public void CustomerControllerTest_GetCustomerById_CallsGetCustomerFromCustomerRepository()
        {
            // Arrange
            var id = 1;
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetCustomerById(id);

            // Assert
            _customerService.Verify(m => m.GetCustomer(It.IsAny<int>()), Times.Once());
        }

        [Trait("CustomerController", "FindCustomersByName")]
        [Fact(DisplayName = "FindCustomersByName returns 200 response when no customers found")]
        public void CustomerControllerTest_SearchCustomers_NoCustomerFound_Returns200Response()
        {
            // Arrange
            const int expectedStatusCode = StatusCodes.Status200OK;
            var searchName = "Bob";
            _mapper.Setup(m => m.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(It.IsAny<IEnumerable<Customer>>()))
             .Returns(new List<CustomerDto>() {});
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.FindCustomersByName(searchName);

            // Assert
            var result = Assert.IsType<OkObjectResult>(test);
            var customers = Assert.IsType<List<CustomerDto>>(result.Value);

            Assert.Equal(expectedStatusCode, result.StatusCode);
            Assert.False(customers.Any());
        }

        [Trait("CustomerController", "FindCustomersByName")]
        [Fact(DisplayName = "FindCustomersByName returns 200 response when customer found")]
        public void CustomerControllerTest_FindCustomersByName_CustomerFound_Returns200Response()
        {
            // Arrange
            const int expectedStatusCode = StatusCodes.Status200OK;
            var searchName = "Bob";
            _customerService.Setup(c => c.FindCustomers(It.IsAny<string>()))
                .Returns(new List<Customer>() {
                    new Customer()
                    {
                        FirstName = searchName
                    }
                });
            _mapper.Setup(m => m.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(It.IsAny<IEnumerable<Customer>>()))
                .Returns(new List<CustomerDto>() {
                    new CustomerDto()
                    {
                        FirstName = searchName
                    }
                });
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.FindCustomersByName(searchName);

            // Assert
            var result = Assert.IsType<OkObjectResult>(test);
            var customers = Assert.IsType<List<CustomerDto>>(result.Value);
            Assert.Equal(expectedStatusCode, result.StatusCode);
            Assert.True(customers.Any());
        }

        [Trait("CustomerController", "FindCustomersByName")]
        [Fact(DisplayName = "FindCustomersByName calls FindCustomers from Customer Repository")]
        public void CustomerControllerTest_FindCustomersByName_CallsFindCustomersFromCustomerRepository()
        {
            // Arrange
            var searchName = "Bob";
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.FindCustomersByName(searchName);

            // Assert
            _customerService.Verify(m => m.FindCustomers(It.IsAny<string>()), Times.Once());
        }
    }
}
