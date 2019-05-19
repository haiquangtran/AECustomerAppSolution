using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Core.Mappings
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            // Map domain models to DTOs and map DTOs back to domain models
            CreateMap<Customer, CustomerReponseDto>().ReverseMap();

            // Map DTOs to domain models
            CreateMap<CreateCustomerRequestDto, Customer>();
            CreateMap<UpdateCustomerRequestDto, Customer>();
        }
    }
}
