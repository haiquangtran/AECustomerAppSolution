using AE.CustomerApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
    }
}
