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
        
        public IEnumerable<CustomerReponseDto> GetCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();
            var customerDtos = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerReponseDto>>(customers);
            return customerDtos;
        }

        public CustomerReponseDto GetCustomer(int id)
        {
            var customer = _customerRepository.GetCustomer(id);
            var customerDto = Mapper.Map<Customer, CustomerReponseDto>(customer);
            return customerDto;
        }

        public void AddCustomer(CreateCustomerRequestDto customerRequest)
        {
            var customer = Mapper.Map<CreateCustomerRequestDto, Customer>(customerRequest);

            _customerRepository.AddCustomer(customer);
        }

        public int SaveChanges()
        {
            return _customerRepository.SaveChanges();
        }
    }
}
