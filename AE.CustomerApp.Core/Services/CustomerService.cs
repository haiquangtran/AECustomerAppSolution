using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Core.Interfaces;
using AE.CustomerApp.Domain.Interfaces;
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
        
        public IEnumerable<CustomerDto> GetCustomers()
        {

            return _customerRepository.GetCustomers();
        }
    }
}
