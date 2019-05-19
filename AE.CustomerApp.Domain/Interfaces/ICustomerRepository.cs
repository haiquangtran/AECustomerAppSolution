using AE.CustomerApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(int id);

        IEnumerable<Customer> GetAllCustomers();

        void AddCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void Remove(Customer customer);

        int SaveChanges();
    }
}
