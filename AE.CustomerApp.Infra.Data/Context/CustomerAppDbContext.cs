using AE.CustomerApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AE.CustomerApp.Infra.Data.Context
{
    public class CustomerAppDbContext : DbContext
    {
        public CustomerAppDbContext(DbContextOptions<CustomerAppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Customer> Customers { get; set; }
    }
}
