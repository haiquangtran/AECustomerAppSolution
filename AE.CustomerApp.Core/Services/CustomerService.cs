using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Core.Interfaces;
using AE.CustomerApp.Domain.Interfaces;
using AE.CustomerApp.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public Customer GetCustomer(int id)
        {
            return _customerRepository.GetCustomer(id);
        }

        public Customer AddCustomer(CreateCustomerRequestDto customerRequest)
        {
            var customer = Mapper.Map<CreateCustomerRequestDto, Customer>(customerRequest);

            _customerRepository.AddCustomer(customer);
            SaveChanges();

            return customer;
        }
        
        public Customer UpdateCustomer(Customer customer, UpdateCustomerRequestDto customerDto)
        {
            // Update fields
            var updatedCustomer = Mapper.Map(customerDto, customer);

            _customerRepository.UpdateCustomer(updatedCustomer);
            SaveChanges();

            return updatedCustomer;
        }

        public void RemoveCustomer(Customer customer)
        {
            _customerRepository.Remove(customer);

            SaveChanges();
        }

        #region Private methods

        private int SaveChanges()
        {
            return _customerRepository.SaveChanges();
        }

        #endregion

    }
}
