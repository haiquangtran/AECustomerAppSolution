using AE.CustomerApp.Core;
using AE.CustomerApp.Core.Interfaces;
using AE.CustomerApp.Core.Mappings;
using AE.CustomerApp.Core.Services;
using AE.CustomerApp.Domain.Interfaces;
using AE.CustomerApp.Infra.Data.Repository;
using AutoMapper;
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
            // Application Core layer
            services.AddScoped<ICustomerService, CustomerService>();

            // Infra.Data layer
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }

        public static void RegisterMappingProfiles(IServiceCollection services)
        {
            // Setup Mapping profiles
            Mapper.Initialize(config => config.AddProfile<DtoMappingProfile>());

            services.AddAutoMapper(typeof(DtoMappingProfile));
        }
    }
}
