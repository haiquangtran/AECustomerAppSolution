using AE.CustomerApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Infra.Data.Context
{
    public class CustomerAppDbContext : DbContext
    {
        public CustomerAppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Customer> Customers { get; set; }
    }
}
