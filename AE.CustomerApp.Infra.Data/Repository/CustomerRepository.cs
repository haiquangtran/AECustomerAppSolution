using AE.CustomerApp.Domain.Interfaces;
using AE.CustomerApp.Domain.Models;
using AE.CustomerApp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Customer> FindCustomers(string name)
        {
            return Find(x => x.FullName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public void AddCustomer(Customer customer)
        {
            // Set timestamps
            customer.SetCreatedDate();
            customer.SetUpdatedDate();
            // Add
            Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            // Update date
            customer.SetUpdatedDate();
            // Add
            Update(customer);
        }

        // TODO: move to unit of work
        public int SaveChanges()
        {
            return Save();
        }
    }
}
