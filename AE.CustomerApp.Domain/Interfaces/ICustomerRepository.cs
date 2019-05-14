using AE.CustomerApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Domain.Interfaces
{
    interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
    }
}
