using AE.CustomerApp.Domain.Interfaces;
using AE.CustomerApp.Domain.Models;
using AE.CustomerApp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE.CustomerApp.Infra.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerAppDbContext _customerAppDbContext;

        public CustomerRepository(CustomerAppDbContext customerAppDbContext)
        {
            _customerAppDbContext = customerAppDbContext;
        }
        
        public IEnumerable<Customer> GetCustomers()
        {
            return _customerAppDbContext.Customers.ToList();
        }
    }
}
