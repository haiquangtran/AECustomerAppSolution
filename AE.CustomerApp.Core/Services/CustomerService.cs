using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Core.Interfaces;
using AE.CustomerApp.Domain.Interfaces;
using AE.CustomerApp.Domain.Models;
using AutoMapper;
using System.Collections.Generic;

namespace AE.CustomerApp.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
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
            var customer = _mapper.Map<CreateCustomerRequestDto, Customer>(customerRequest);

            _customerRepository.AddCustomer(customer);
            SaveChanges();

            return customer;
        }
        
        public Customer UpdateCustomer(Customer customer, UpdateCustomerRequestDto customerDto)
        {
            // Update fields
            var updatedCustomer = _mapper.Map(customerDto, customer);

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

        public IEnumerable<Customer> FindCustomers(string name)
        {
            return _customerRepository.FindCustomers(name);
        }

        #endregion

    }
}
