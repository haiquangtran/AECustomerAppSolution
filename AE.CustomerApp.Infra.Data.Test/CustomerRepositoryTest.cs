using System;
using Xunit;

namespace AE.CustomerApp.Infra.Data.Test
{
    public class CustomerRepositoryTest
    {
        [Trait("CustomerRepository", "FindBy")]
        [Fact(DisplayName = "Find Customer by first name")]
        public void CustomerRepositoryTest_FindBy_FirstName_ShouldReturnCustomerWithFirstName()
        {
            // TODO:
        }

        [Trait("CustomerRepository", "FindBy")]
        [Fact(DisplayName = "Find Customer by last name")]
        public void CustomerRepositoryTest_FindBy_LastName_ShouldReturnCustomerWithLastName()
        {
            // TODO:
        }

        [Trait("CustomerRepository", "FindBy")]
        [Fact(DisplayName = "Find Customer by name - No matching customer")]
        public void CustomerRepositoryTest_FindBy_Name_NoMatchingCustomer_ShouldReturnNull()
        {
            // TODO:
        }
    }
}
