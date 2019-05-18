using System;
using Xunit;

namespace AE.CustomerApp.Core.Test
{
    public class CustomerServiceTest
    {
        [Trait("CustomerService", "Get Customers")]
        [Fact(DisplayName = "Customers list is not null")]
        public void CustomerServiceTest_GetCustomers_ShouldNotBeNull()
        {
            // TODO:
        }

        [Trait("CustomerService", "Add Customer")]
        [Fact(DisplayName = "Valid customer should be added")]
        public void CustomerServiceTest_AddCustomer_ValidCustomer_ShouldAddCustomer()
        {
            // TODO:
        }

        [Trait("CustomerService", "Add Customer")]
        [Fact(DisplayName = "Invalid customer should not be added")]
        public void CustomerServiceTest_AddCustomer_InvalidCustomer_ShouldReturnBadRequest()
        {
            // TODO:
        }

        [Trait("CustomerService", "Delete Customer")]
        [Fact(DisplayName = "Customer should be removed")]
        public void CustomerServiceTest_RemoveCustomer_CustomerExists_ShouldRemoveCustomer()
        {
            // TODO:
        }

        [Trait("CustomerService", "Delete Customer")]
        [Fact(DisplayName = "Customer does not exist - throw exception")]
        public void CustomerServiceTest_RemoveCustomer_CustomerDoesNotExist_ShouldThrowException()
        {
            // TODO:
        }

        [Trait("CustomerService", "Update Customer")]
        [Fact(DisplayName = "Update Customer First Name")]
        public void CustomerServiceTest_UpdateCustomer_ValidCustomer_FirstNameUpdated_ShouldUpdateCustomerFirstName()
        {
            // TODO:
        }

        [Trait("CustomerService", "Update Customer")]
        [Fact(DisplayName = "Update customer fails - throw exception")]
        public void CustomerServiceTest_UpdateCustomer_InvalidCustomer_NoFirstName_ShouldThrowException()
        {
            // TODO:
        }

        [Trait("CustomerService", "FindBy")]
        [Fact(DisplayName = "Find Customer by first name")]
        public void CustomerRepositoryTest_FindBy_FirstName_ShouldReturnCustomerWithFirstName()
        {
            // TODO:
        }

        [Trait("CustomerService", "FindBy")]
        [Fact(DisplayName = "Find Customer by last name")]
        public void CustomerRepositoryTest_FindBy_LastName_ShouldReturnCustomerWithLastName()
        {
            // TODO:
        }

        [Trait("CustomerService", "FindBy")]
        [Fact(DisplayName = "Find Customer by name - No matching customer")]
        public void CustomerRepositoryTest_FindBy_Name_NoMatchingCustomer_ShouldReturnNull()
        {
            // TODO:
        }
    }
}
