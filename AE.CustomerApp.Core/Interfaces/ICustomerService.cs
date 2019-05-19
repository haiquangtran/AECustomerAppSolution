using AE.CustomerApp.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Core.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerReponseDto> GetCustomers();

        CustomerReponseDto GetCustomer(int id);

        void AddCustomer(CreateCustomerRequestDto customerRequestDto);

        int SaveChanges();
    }
}
