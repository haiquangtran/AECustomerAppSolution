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

namespace AE.CustomerApp.Api.Test.Controllers
{
    public class CustomerControllerTest
    {
        private IOptions<AppSettingsConfiguration> _fakeAppSettings;
        private Mock<ICustomerService> _fakeCustomerService;
        private Mock<IMapper> _fakeMapper;
        private CustomerController _customerController;

        public CustomerControllerTest()
        {
            _fakeCustomerService = new Mock<ICustomerService>();
            _fakeMapper = new Mock<IMapper>();
            _fakeAppSettings = Options.Create(new AppSettingsConfiguration());
            _customerController = new CustomerController(_fakeCustomerService.Object, _fakeMapper.Object, _fakeAppSettings);
        }

        #region GetCustomers Tests

        [Trait("CustomerController", "GetCustomers")]
        [Fact(DisplayName = "Get Customers Returns 200 Response")]
        public void CustomerControllerTest_GetCustomers_Returns200Response()
        {
            // Arrange
            const int expectedStatusCode = StatusCodes.Status200OK;

            // Act
            var test = _customerController.GetCustomers();

            // Assert
            var mockResult = Assert.IsType<OkObjectResult>(test);
            Assert.Equal(expectedStatusCode, mockResult.StatusCode);
        }

        [Trait("CustomerController", "GetCustomers")]
        [Fact(DisplayName = "Get Customers Calls Get From Customer Repository")]
        public void CustomerControllerTest_GetCustomers_CallsGetFromCustomerRepository()
        {
            // Act
            var test = _customerController.GetCustomers();

            // Assert
            _fakeCustomerService.Verify(m => m.GetCustomers(), Times.Once());
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
            _fakeMapper.Setup(m => m.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(It.IsAny<IEnumerable<Customer>>()))
                .Returns(expectedResult);

            // Act
            var test = _customerController.GetCustomers();

            // Assert
            var mockResult = test as OkObjectResult;
            var mockCustomers = Assert.IsType<List<CustomerDto>>(mockResult.Value);
            Assert.Equal(expectedResult.Count, mockCustomers.Count);
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

            // Act
            var test = _customerController.GetCustomerById(fakeId);

            // Assert
            var mockResult = Assert.IsType<NoContentResult>(test);
            Assert.Equal(expectedStatusCode, mockResult.StatusCode);
        }

        [Trait("CustomerController", "GetCustomerById")]
        [Fact(DisplayName = "Customer found by id returns 200 response")]
        public void CustomerControllerTest_GetCustomerById_CustomerFound_Returns200Response()
        {
            // Arrange
            const int expectedStatusCode = StatusCodes.Status200OK;
            var fakeId = 1;
            _fakeCustomerService.Setup(c => c.GetCustomer(It.IsAny<int>())).Returns(new Customer());
            _fakeMapper.Setup(m => m.Map<Customer, CustomerDto>(It.IsAny<Customer>())).Returns(new CustomerDto());

            // Act
            var test = _customerController.GetCustomerById(fakeId);

            // Assert
            var mockResult = Assert.IsType<OkObjectResult>(test);
            Assert.Equal(expectedStatusCode, mockResult.StatusCode);
        }

        [Trait("CustomerController", "GetCustomerById")]
        [Fact(DisplayName = "GetCustomerById calls GetCustomer from Customer Repository")]
        public void CustomerControllerTest_GetCustomerById_CallsGetCustomerFromCustomerRepository()
        {
            // Arrange
            var fakeId = 1;

            // Act
            var test = _customerController.GetCustomerById(fakeId);

            // Assert
            _fakeCustomerService.Verify(m => m.GetCustomer(It.IsAny<int>()), Times.Once());
        }

        #endregion GetCustomerById Tests

        #region FindCustomersByName Tests

        [Trait("CustomerController", "FindCustomersByName")]
        [Fact(DisplayName = "FindCustomersByName returns 200 response when no customers found")]
        public void CustomerControllerTest_SearchCustomers_NoCustomerFound_Returns200Response()
        {
            // Arrange
            const int expectedStatusCode = StatusCodes.Status200OK;
            var fakeSearchName = "Bob";
            _fakeMapper.Setup(m => m.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(It.IsAny<IEnumerable<Customer>>()))
             .Returns(new List<CustomerDto>() {});

            // Act
            var test = _customerController.FindCustomersByName(fakeSearchName);

            // Assert
            var mockResult = Assert.IsType<OkObjectResult>(test);
            var mockCustomers = Assert.IsType<List<CustomerDto>>(mockResult.Value);
            Assert.Equal(expectedStatusCode, mockResult.StatusCode);
            Assert.False(mockCustomers.Any());
        }

        [Trait("CustomerController", "FindCustomersByName")]
        [Fact(DisplayName = "FindCustomersByName returns 200 response when customer found")]
        public void CustomerControllerTest_FindCustomersByName_CustomerFound_Returns200Response()
        {
            // Arrange
            const int expectedStatusCode = StatusCodes.Status200OK;
            var fakeName = "Bob";
            _fakeCustomerService.Setup(c => c.FindCustomers(It.IsAny<string>()))
                .Returns(new List<Customer>() {
                    new Customer()
                    {
                        FirstName = fakeName
                    }
                });
            _fakeMapper.Setup(m => m.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(It.IsAny<IEnumerable<Customer>>()))
                .Returns(new List<CustomerDto>() {
                    new CustomerDto()
                    {
                        FirstName = fakeName
                    }
                });

            // Act
            var test = _customerController.FindCustomersByName(fakeName);

            // Assert
            var mockResult = Assert.IsType<OkObjectResult>(test);
            var mockCustomers = Assert.IsType<List<CustomerDto>>(mockResult.Value);
            Assert.Equal(expectedStatusCode, mockResult.StatusCode);
            Assert.True(mockCustomers.Any());
        }

        [Trait("CustomerController", "FindCustomersByName")]
        [Fact(DisplayName = "FindCustomersByName calls FindCustomers from Customer Repository")]
        public void CustomerControllerTest_FindCustomersByName_CallsFindCustomersFromCustomerRepository()
        {
            // Arrange
            var fakeName = "Bob";

            // Act
            var test = _customerController.FindCustomersByName(fakeName);

            // Assert
            _fakeCustomerService.Verify(m => m.FindCustomers(It.IsAny<string>()), Times.Once());
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
            _fakeCustomerService.Setup(c => c.AddCustomer(It.IsAny<CreateCustomerRequestDto>()))
                .Returns(fakeCustomer);
            _fakeMapper.Setup(m => m.Map<Customer, CustomerDto>(It.IsAny<Customer>()))
               .Returns(fakeResponse);

            // Act
            var test = _customerController.CreateCustomer(fakeRequest);

            // Assert
            var mockResult = Assert.IsType<CreatedResult>(test);
            var mockCustomer = Assert.IsType<CustomerDto>(mockResult.Value);
            Assert.Equal(expectedStatusCode, mockResult.StatusCode);
            Assert.NotNull(mockCustomer);
            Assert.Equal(expectedId, mockCustomer.Id);
            Assert.Equal(expectedRoute, mockResult.Location);
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
            _fakeCustomerService.Setup(c => c.AddCustomer(It.IsAny<CreateCustomerRequestDto>()))
               .Returns(new Customer());

            // Act
            var test = _customerController.CreateCustomer(fakeRequest);

            // Assert
            _fakeCustomerService.Verify(m => m.AddCustomer(It.IsAny<CreateCustomerRequestDto>()), Times.Once());
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
            _fakeCustomerService.Setup(c => c.GetCustomer(It.IsAny<int>()))
                .Returns((Customer)null);

            // Act
            var test = _customerController.UpdateCustomer(expectedId, fakeRequest);

            // Assert
            var mockResult = Assert.IsType<NotFoundResult>(test);
            Assert.Equal(expectedStatusCode, mockResult.StatusCode);
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
            _fakeCustomerService.Setup(c => c.GetCustomer(It.IsAny<int>()))
                .Returns(fakeCustomer);
            _fakeCustomerService.Setup(c => c.UpdateCustomer(It.IsAny<Customer>(), It.IsAny<UpdateCustomerRequestDto>()))
                .Returns(fakeCustomer);
            _fakeMapper.Setup(m => m.Map<Customer, CustomerDto>(It.IsAny<Customer>()))
               .Returns(fakeResponse);

            // Act
            var test = _customerController.UpdateCustomer(expectedId, fakeRequest);

            // Assert
            var mockResult = Assert.IsType<OkObjectResult>(test);
            var mockCustomer = Assert.IsType<CustomerDto>(mockResult.Value);
            Assert.Equal(expectedStatusCode, mockResult.StatusCode);
            Assert.NotNull(mockCustomer);
            Assert.Equal(expectedId, mockCustomer.Id);
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
            _fakeCustomerService.Setup(c => c.GetCustomer(It.IsAny<int>()))
                .Returns(fakeCustomer);
            _fakeCustomerService.Setup(c => c.UpdateCustomer(It.IsAny<Customer>(), It.IsAny<UpdateCustomerRequestDto>()))
               .Returns(fakeCustomer);

            // Act
            var test = _customerController.UpdateCustomer(fakeId, fakeUpdateRequest);

            // Assert
            _fakeCustomerService.Verify(m => m.UpdateCustomer(It.IsAny<Customer>(), It.IsAny<UpdateCustomerRequestDto>()), Times.Once());
        }

        #endregion

        #region DeleteCustomer Tests

        [Trait("CustomerController", "DeleteCustomer")]
        [Fact(DisplayName = "DeleteCustomer cant find customer and returns not found 404 response")]
        public void CustomerControllerTest_DeleteCustomer_NoFoundCustomer_Returns404Response()
        {
            // Arrange
            var expectedStatusCode = StatusCodes.Status404NotFound;
            var fakeId = 1;
            _fakeCustomerService.Setup(c => c.GetCustomer(It.IsAny<int>()))
                .Returns((Customer)null);

            // Act
            var test = _customerController.DeleteCustomer(fakeId);

            // Assert
            var mockResult = Assert.IsType<NotFoundResult>(test);
            Assert.Equal(expectedStatusCode, mockResult.StatusCode);
        }

        [Trait("CustomerController", "DeleteCustomer")]
        [Fact(DisplayName = "DeleteCustomer succeeded returns no content 204 Response")]
        public void CustomerControllerTest_DeleteCustomer_ValidDeletedCustomer_ReturnsNoContent204Response()
        {
            // Arrange
            var expectedStatusCode = StatusCodes.Status204NoContent;
            var fakeId = 1;
            _fakeCustomerService.Setup(c => c.GetCustomer(It.IsAny<int>()))
                .Returns(new Customer());

            // Act
            var test = _customerController.DeleteCustomer(fakeId);

            // Assert
            var mockResult = Assert.IsType<NoContentResult>(test);
            Assert.Equal(expectedStatusCode, mockResult.StatusCode);
        }

        [Trait("CustomerController", "DeleteCustomer")]
        [Fact(DisplayName = "DeleteCustomer calls RemoveCustomer from Customer Repository")]
        public void CustomerControllerTest_DeleteCustomer_ValidDeletedCustomer_CallsRemoveCustomerFromCustomerRepository()
        {
            // Arrange
            var fakeId = 1;
            _fakeCustomerService.Setup(c => c.GetCustomer(It.IsAny<int>()))
               .Returns(new Customer() { Id = fakeId });

            // Act
            var test = _customerController.DeleteCustomer(fakeId);

            // Assert
            _fakeCustomerService.Verify(m => m.RemoveCustomer(It.IsAny<Customer>()), Times.Once());
        }

        #endregion
    }
}
