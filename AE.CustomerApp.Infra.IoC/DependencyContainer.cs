using AE.CustomerApp.Core.Interfaces;
using AE.CustomerApp.Core.Services;
using AE.CustomerApp.Domain.Interfaces;
using AE.CustomerApp.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application layer
            services.AddScoped<ICustomerService, CustomerService>();

            // Infra.Data layer
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
