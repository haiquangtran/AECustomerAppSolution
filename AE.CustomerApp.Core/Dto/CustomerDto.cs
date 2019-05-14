using AE.CustomerApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Core.Dto
{
    public class CustomerDto
    {
        public IEnumerable<Customer> Customers { get; set; }
    }
}
