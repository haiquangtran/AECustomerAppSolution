using AE.CustomerApp.Domain.Interfaces;
using AE.CustomerApp.Domain.Models;
using AE.CustomerApp.Infra.Data.Context;
using AE.CustomerApp.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AE.CustomerApp.Infra.Data.Test.IntegrationTest
{
    public class CustomerRepositoryTest
    {
        private DbContextOptions<CustomerAppDbContext> _options;
        private ICustomerRepository _customerRepository;

        public CustomerRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<CustomerAppDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            using (var dbContext = new CustomerAppDbContext(_options))
            {
                dbContext.Database.EnsureCreated();
            }
        }

        [Trait("CustomerRepository", "AddCustomer")]
        [Fact(DisplayName = "Adds Customer")]
        public void CustomerRepository_AddCustomer()
        {
            using (var dbContext = new CustomerAppDbContext(_options))
            {
                // Arrange
                _customerRepository = new CustomerRepository(dbContext);
                var fakeId = 1;

                // Act
                _customerRepository.AddCustomer(GetFakeCustomer(fakeId));
                _customerRepository.SaveChanges();

                // Assert
                var mockCustomer = _customerRepository.GetCustomer(fakeId);
                Assert.NotNull(mockCustomer);
                Assert.Equal(fakeId, mockCustomer.Id);
            }
        }

        [Trait("CustomerRepository", "GetCustomers")]
        [Fact(DisplayName = "Gets all Customers")]
        public void CustomerRepository_GetsAllCustomers()
        {
            using (var dbContext = new CustomerAppDbContext(_options))
            {
                // Arrange
                _customerRepository = new CustomerRepository(dbContext);
                var expectedCustomerCount = 10;
                for (int i = 1; i <= expectedCustomerCount; i++)
                {
                    _customerRepository.AddCustomer(GetFakeCustomer(i));
                }
                _customerRepository.SaveChanges();

                // Act
                var test = _customerRepository.GetAllCustomers();

                // Assert
                var mockCustomers = Assert.IsType<List<Customer>>(test.ToList());
                Assert.Equal(expectedCustomerCount, mockCustomers.Count);
            }
        }

        [Trait("CustomerRepository", "UpdateCustomer")]
        [Fact(DisplayName = "Updates Customer")]
        public void CustomerRepository_UpdateCustomer()
        {
            using (var dbContext = new CustomerAppDbContext(_options))
            {
                // Arrange
                _customerRepository = new CustomerRepository(dbContext);
                var fakeId = 1;
                var expectedUpdatedFirstName = "fakeUpdatedFirstName";
                var expectedUpdatedLastName = "fakeUpdatedLastName";
                _customerRepository.AddCustomer(GetFakeCustomer(fakeId));
                _customerRepository.SaveChanges();

                // Act
                var mockCustomer = _customerRepository.GetCustomer(fakeId);
                mockCustomer.FirstName = expectedUpdatedFirstName;
                mockCustomer.LastName = expectedUpdatedLastName;
                _customerRepository.UpdateCustomer(mockCustomer);
                _customerRepository.SaveChanges();

                // Assert
                var mockUpdatedCustomer = _customerRepository.GetCustomer(fakeId);
                Assert.NotNull(mockCustomer);
                Assert.Equal(expectedUpdatedFirstName, mockCustomer.FirstName);
                Assert.Equal(expectedUpdatedLastName, mockCustomer.LastName);
            }
        }

        [Trait("CustomerRepository", "DeleteCustomer")]
        [Fact(DisplayName = "Delete Customer")]
        public void CustomerRepository_DeleteCustomer()
        {
            using (var dbContext = new CustomerAppDbContext(_options))
            {
                // Arrange
                _customerRepository = new CustomerRepository(dbContext);
                var fakeId = 1;
                _customerRepository.AddCustomer(GetFakeCustomer(fakeId));
                _customerRepository.SaveChanges();
                var mockCustomer = _customerRepository.GetCustomer(fakeId);

                // Act
                _customerRepository.Remove(mockCustomer);
                _customerRepository.SaveChanges();

                // Assert
                var mockDeletedCustomer = _customerRepository.GetCustomer(fakeId);
                Assert.Null(mockDeletedCustomer);
            }
        }

        [Trait("CustomerRepository", "FindCustomer")]
        [Fact(DisplayName = "Find Customer by partial name")]
        public void CustomerRepository_FindCustomerByPartialName()
        {
            using (var dbContext = new CustomerAppDbContext(_options))
            {
                // Arrange
                var expectedFoundCustomers = 2;
                var fakePartialNameSearch = "Stel";
                _customerRepository = new CustomerRepository(dbContext);
                var fakeName = "Stella";
                var fakeCustomerOne = GetFakeCustomer(1);
                fakeCustomerOne.FirstName = fakeName;
                var fakeCustomerTwo = GetFakeCustomer(2);
                fakeCustomerTwo.LastName = fakeName;
                var fakeCustomerThree = GetFakeCustomer(3);
                _customerRepository.AddCustomer(fakeCustomerOne);
                _customerRepository.AddCustomer(fakeCustomerTwo);
                _customerRepository.AddCustomer(fakeCustomerThree);
                _customerRepository.SaveChanges();

                // Act
                var test = _customerRepository.FindCustomers(fakePartialNameSearch);

                // Assert
                var mockFoundCustomers = Assert.IsType<List<Customer>>(test);
                Assert.Equal(expectedFoundCustomers, mockFoundCustomers.Count());
                mockFoundCustomers.ForEach(c =>
                {
                    Assert.Contains(fakePartialNameSearch, c.FullName);
                });
            }
        }

        private Customer GetFakeCustomer(int id)
        {
            return new Customer()
            {
                Id = id,
                DateOfBirth = new DateTime(1990, 04, 22),
                FirstName = "fakeFirstName",
                LastName = "fakeLastName"
            };

        }
    }
}
