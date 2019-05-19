using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Core.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();

        IEnumerable<Customer> FindCustomers(string name);

        Customer GetCustomer(int id);

        Customer AddCustomer(CreateCustomerRequestDto customerRequestDto);

        Customer UpdateCustomer(Customer customer, UpdateCustomerRequestDto customerRequestDto);

        void RemoveCustomer(Customer customer);
    }
}
