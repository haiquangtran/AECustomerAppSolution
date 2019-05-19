using AE.CustomerApp.Domain.Interfaces;
using AE.CustomerApp.Domain.Models;
using AE.CustomerApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE.CustomerApp.Infra.Data.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private CustomerAppDbContext _customerAppDbContext => (CustomerAppDbContext)_dbContext;

        public CustomerRepository(CustomerAppDbContext dbContext) : base(dbContext)
        {
        }

        public Customer GetCustomer(int id)
        {
            return Get(id);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return GetAll().ToList();
        }

        public void AddCustomer(Customer customer)
        {
            // Set timestamps
            customer.SetCreatedDate();
            customer.SetUpdatedDate();
            // Add
            Add(customer);
        }

        public int SaveChanges()
        {
            return Save();
        }
    }
}
