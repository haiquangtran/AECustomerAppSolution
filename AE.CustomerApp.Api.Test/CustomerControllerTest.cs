using AE.CustomerApp.Api.Controllers;
using AE.CustomerApp.Core;
using AE.CustomerApp.Core.Constants;
using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Core.Interfaces;
using AE.CustomerApp.Domain.Models;
using AE.CustomerApp.Infra.IoC.Filters;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
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

        #region GetCustomers Tests

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

        #endregion GetCustomers Tests

        #region GetCustomerById Tests

        [Trait("CustomerController", "GetCustomerById")]
        [Fact(DisplayName = "No customer found by id returns 204 response")]
        public void CustomerControllerTest_GetCustomerById_NoCustomerFound_Returns204Response()
        {
            // Arrange
            const int expectedStatusCode = StatusCodes.Status204NoContent;
            var fakeId = 1;
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetCustomerById(fakeId);

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
            var fakeId = 1;
            _customerService.Setup(c => c.GetCustomer(It.IsAny<int>())).Returns(new Customer());
            _mapper.Setup(m => m.Map<Customer, CustomerDto>(It.IsAny<Customer>())).Returns(new CustomerDto());
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetCustomerById(fakeId);

            // Assert
            var result = Assert.IsType<OkObjectResult>(test);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Trait("CustomerController", "GetCustomerById")]
        [Fact(DisplayName = "GetCustomerById calls GetCustomer from Customer Repository")]
        public void CustomerControllerTest_GetCustomerById_CallsGetCustomerFromCustomerRepository()
        {
            // Arrange
            var fakeId = 1;
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.GetCustomerById(fakeId);

            // Assert
            _customerService.Verify(m => m.GetCustomer(It.IsAny<int>()), Times.Once());
        }

        #endregion GetCustomerById Tests

        #region FindCustomersByName Tests

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
            var fakeName = "Bob";
            _customerService.Setup(c => c.FindCustomers(It.IsAny<string>()))
                .Returns(new List<Customer>() {
                    new Customer()
                    {
                        FirstName = fakeName
                    }
                });
            _mapper.Setup(m => m.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(It.IsAny<IEnumerable<Customer>>()))
                .Returns(new List<CustomerDto>() {
                    new CustomerDto()
                    {
                        FirstName = fakeName
                    }
                });
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.FindCustomersByName(fakeName);

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
            var fakeName = "Bob";
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.FindCustomersByName(fakeName);

            // Assert
            _customerService.Verify(m => m.FindCustomers(It.IsAny<string>()), Times.Once());
        }

        #endregion FindCustomersByName Tests

        #region CreateCustomer Tests

        [Trait("CustomerController", "CreateCustomer")]
        [Fact(DisplayName = "Customer created returns added customer and 201 response")]
        public void CustomerControllerTest_CreateCustomer_ValidCustomerCreated_ReturnsAddedCustomerAnd201Response()
        {
            // Arrange
            var expectedStatusCode = StatusCodes.Status201Created;
            var expectedId = 1;
            var expectedRoute = $"{ApiConstants.ApiBaseRoute}/customer/{expectedId}";
            var fakeRequest = new CreateCustomerRequestDto()
            {
                DateOfBirth = new DateTime(2000, 05, 22),
                FirstName = "fakeFirstName",
                LastName = "fakeLastName"
            };
            var fakeResponse = new CustomerDto() { Id = expectedId };
            var fakeCustomer = new Customer() { Id = expectedId };
            _customerService.Setup(c => c.AddCustomer(It.IsAny<CreateCustomerRequestDto>()))
                .Returns(fakeCustomer);
            _mapper.Setup(m => m.Map<Customer, CustomerDto>(It.IsAny<Customer>()))
               .Returns(fakeResponse);
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.CreateCustomer(fakeRequest);

            // Assert
            var result = Assert.IsType<CreatedResult>(test);
            var customer = Assert.IsType<CustomerDto>(result.Value);
            Assert.Equal(expectedStatusCode, result.StatusCode);
            Assert.NotNull(customer);
            Assert.Equal(expectedId, customer.Id);
            Assert.Equal(expectedRoute, result.Location);
        }
        
        [Trait("CustomerController", "CreateCustomer")]
        [Fact(DisplayName = "CreateCustomer is decorated with ValidateModelStateAttribute")]
        public void CustomerControllerTest_CreateCustomer_IsDecoratedWithValidateModelStateAttribute()
        {
            // Act
            var test = typeof(CustomerController).GetMethod(nameof(CustomerController.CreateCustomer));

            // Assert
            test.Should().BeDecoratedWith<ValidateModelStateAttribute>();
        }

        [Trait("CustomerController", "CreateCustomer")]
        [Fact(DisplayName = "CreateCustomer calls AddCustomer from Customer Repository")]
        public void CustomerControllerTest_CreateCustomer_ValidCustomer_CallsAddCustomerFromCustomerRepository()
        {
            // Arrange
            var fakeRequest = new CreateCustomerRequestDto()
            {
                DateOfBirth = new DateTime(2000, 05, 22),
                FirstName = "fakeFirstName",
                LastName = "fakeLastName"
            };
            _customerService.Setup(c => c.AddCustomer(It.IsAny<CreateCustomerRequestDto>()))
               .Returns(new Customer());
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.CreateCustomer(fakeRequest);

            // Assert
            _customerService.Verify(m => m.AddCustomer(It.IsAny<CreateCustomerRequestDto>()), Times.Once());
        }

        #endregion CreateCustomer Tests

        #region UpdateCustomer Tests

        [Trait("CustomerController", "UpdateCustomer")]
        [Fact(DisplayName = "Update customer cant find customer and returns not found 404 response")]
        public void CustomerControllerTest_UpdateCustomer_NoFoundCustomer_Returns404Response()
        {
            // Arrange
            var expectedStatusCode = StatusCodes.Status404NotFound;
            var expectedId = 1;
            var fakeRequest = new UpdateCustomerRequestDto()
            {
                DateOfBirth = new DateTime(2000, 05, 22),
                FirstName = "fakeFirstName",
                LastName = "fakeLastName"
            };
            _customerService.Setup(c => c.GetCustomer(It.IsAny<int>()))
                .Returns((Customer)null);
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.UpdateCustomer(expectedId, fakeRequest);

            // Assert
            var result = Assert.IsType<NotFoundResult>(test);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Trait("CustomerController", "UpdateCustomer")]
        [Fact(DisplayName = "Updated customer returns updated customer and 200 response")]
        public void CustomerControllerTest_UpdateCustomer_ValidCustomerUpdated_ReturnsUpdatedCustomerAnd200Response()
        {
            // Arrange
            var expectedStatusCode = StatusCodes.Status200OK;
            var expectedId = 1;
            var fakeRequest = new UpdateCustomerRequestDto()
            {
                DateOfBirth = new DateTime(2000, 05, 22),
                FirstName = "fakeFirstName",
                LastName = "fakeLastName"
            };
            var fakeResponse = new CustomerDto() { Id = expectedId };
            var fakeCustomer = new Customer() { Id = expectedId };
            _customerService.Setup(c => c.GetCustomer(It.IsAny<int>()))
                .Returns(fakeCustomer);
            _customerService.Setup(c => c.UpdateCustomer(It.IsAny<Customer>(), It.IsAny<UpdateCustomerRequestDto>()))
                .Returns(fakeCustomer);
            _mapper.Setup(m => m.Map<Customer, CustomerDto>(It.IsAny<Customer>()))
               .Returns(fakeResponse);
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.UpdateCustomer(expectedId, fakeRequest);

            // Assert
            var result = Assert.IsType<OkObjectResult>(test);
            var customer = Assert.IsType<CustomerDto>(result.Value);
            Assert.Equal(expectedStatusCode, result.StatusCode);
            Assert.NotNull(customer);
            Assert.Equal(expectedId, customer.Id);
        }
        
        [Trait("CustomerController", "UpdateCustomer")]
        [Fact(DisplayName = "UpdateCustomer is decorated with ValidateModelStateAttribute")]
        public void CustomerControllerTest_UpdateCustomer_IsDecoratedWithValidateModelStateAttribute()
        {
            // Act
            var test = typeof(CustomerController).GetMethod(nameof(CustomerController.UpdateCustomer));

            // Assert
            test.Should().BeDecoratedWith<ValidateModelStateAttribute>();
        }

        [Trait("CustomerController", "UpdateCustomer")]
        [Fact(DisplayName = "UpdateCustomer calls UpdateCustomer from Customer Repository")]
        public void CustomerControllerTest_UpdateCustomer_ValidCustomer_CallsUpdateCustomerFromCustomerRepository()
        {
            // Arrange
            var fakeId = 1;
            var fakeUpdateRequest = new UpdateCustomerRequestDto()
            {
                DateOfBirth = new DateTime(2000, 05, 22),
                FirstName = "fakeFirstName",
                LastName = "fakeLastName"
            };
            var fakeCustomer = new Customer();
            _customerService.Setup(c => c.GetCustomer(It.IsAny<int>()))
                .Returns(fakeCustomer);
            _customerService.Setup(c => c.UpdateCustomer(It.IsAny<Customer>(), It.IsAny<UpdateCustomerRequestDto>()))
               .Returns(fakeCustomer);
            var customerController = new CustomerController(_customerService.Object, _mapper.Object, _appSettings);

            // Act
            var test = customerController.UpdateCustomer(fakeId, fakeUpdateRequest);

            // Assert
            _customerService.Verify(m => m.UpdateCustomer(It.IsAny<Customer>(), It.IsAny<UpdateCustomerRequestDto>()), Times.Once());
        }

        #endregion


    }
}
