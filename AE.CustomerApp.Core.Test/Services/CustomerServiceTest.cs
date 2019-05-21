using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Core.Services;
using AE.CustomerApp.Domain.Interfaces;
using AE.CustomerApp.Domain.Models;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AE.CustomerApp.Core.Test.Services
{
    public class CustomerServiceTest
    {
        private Mock<IMapper> _fakeMapper;
        private Mock<ICustomerRepository> _fakeCustomerRepository;
        private CustomerService _customerService;

        public CustomerServiceTest()
        {
            _fakeMapper = new Mock<IMapper>();
            _fakeCustomerRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_fakeCustomerRepository.Object, _fakeMapper.Object);
        }

        [Trait("CustomerService", "Get Customers")]
        [Fact(DisplayName = "Customers list has customers")]
        public void CustomerServiceTest_GetCustomers_ReturnsCustomers()
        {
            // Arrange
            var expectedCustomers = new List<Customer>() { new Customer(), new Customer() };
            _fakeCustomerRepository.Setup(c => c.GetAllCustomers())
                .Returns(expectedCustomers);

            // Act
            var test = _customerService.GetCustomers();

            // Assert
            var mockCustomers = Assert.IsType<List<Customer>>(test);
            Assert.NotNull(mockCustomers);
            Assert.Equal(expectedCustomers.Count, mockCustomers.Count);
        }

        [Trait("CustomerService", "Add Customer")]
        [Fact(DisplayName = "Valid customer should be added")]
        public void CustomerServiceTest_AddCustomer_ValidCustomer_ShouldAddCustomer()
        {
            // Arrange
            var expectedCustomer = new Customer() { Id = 1 };
            var fakeCreateCustomerRequest = new CreateCustomerRequestDto()
            {
                FirstName = "fakeFirstName",
                LastName = "fakeLastName",
                DateOfBirth = new DateTime(1990, 07, 26),
            };
            _fakeCustomerRepository.Setup(c => c.AddCustomer(It.IsAny<Customer>()));
            _fakeMapper.Setup(m => m.Map<CreateCustomerRequestDto, Customer>(It.IsAny<CreateCustomerRequestDto>()))
                .Returns(expectedCustomer);

            // Act
            var test = _customerService.AddCustomer(fakeCreateCustomerRequest);

            // Assert
            var mockCustomer = Assert.IsType<Customer>(test);
            Assert.NotNull(mockCustomer);
            Assert.Equal(expectedCustomer, mockCustomer);
            _fakeCustomerRepository.Verify(c => c.AddCustomer(It.IsAny<Customer>()), Times.Once);
            _fakeCustomerRepository.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Trait("CustomerService", "Delete Customer")]
        [Fact(DisplayName = "Customer should be removed")]
        public void CustomerServiceTest_RemoveCustomer_CustomerExists_ShouldRemoveCustomer()
        {
            // Arrange
            var fakeCustomer = new Customer() { Id = 1 };

            // Act
            _customerService.RemoveCustomer(fakeCustomer);

            // Assert
            _fakeCustomerRepository.Verify(c => c.Remove(It.IsAny<Customer>()), Times.Once);
            _fakeCustomerRepository.Verify(c => c.SaveChanges(), Times.Once);
        }
        
        [Trait("CustomerService", "Update Customer")]
        [Fact(DisplayName = "Update Customer First Name")]
        public void CustomerServiceTest_UpdateCustomer_ValidCustomer_FirstNameUpdated_ShouldUpdateCustomerFirstName()
        {
            // Arrange
            var fakeCustomer = new Customer() { Id = 1 };
            var fakeUpdateCustomerRequest = new UpdateCustomerRequestDto()
            {
                FirstName = "fakeFirstName",
                LastName = "fakeLastName",
                DateOfBirth = new DateTime(1999, 07, 26)
            };
            _fakeCustomerRepository.Setup(c => c.UpdateCustomer(fakeCustomer));
            _fakeMapper.Setup(m => m.Map(It.IsAny<UpdateCustomerRequestDto>(), It.IsAny<Customer>()))
                .Returns(fakeCustomer);

            // Act
            var test = _customerService.UpdateCustomer(fakeCustomer, fakeUpdateCustomerRequest);

            // Assert
            var mockCustomer = Assert.IsType<Customer>(test);
            Assert.NotNull(mockCustomer);
            _fakeCustomerRepository.Verify(c => c.UpdateCustomer(It.IsAny<Customer>()), Times.Once);
            _fakeCustomerRepository.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Trait("CustomerService", "FindBy")]
        [Fact(DisplayName = "Find Customer by first name")]
        public void CustomerRepositoryTest_FindBy_FirstName_ShouldCallFindCustomersFromCustomerRepository()
        {
            // Arrange
            var fakeSearchName = "Bo";

            // Act
            var test = _customerService.FindCustomers(fakeSearchName);

            // Assert
            _fakeCustomerRepository.Verify(c => c.FindCustomers(It.IsAny<string>()), Times.Once);
        }
        
    }
}
