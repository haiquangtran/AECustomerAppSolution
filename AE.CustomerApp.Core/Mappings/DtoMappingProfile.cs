using AE.CustomerApp.Core.Dto;
using AE.CustomerApp.Domain.Models;
using AutoMapper;

namespace AE.CustomerApp.Core.Mappings
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            // Map domain models to DTOs and map DTOs back to domain models
            CreateMap<Customer, CustomerDto>().ReverseMap();

            // Map DTOs to domain models
            CreateMap<CreateCustomerRequestDto, Customer>();
            CreateMap<UpdateCustomerRequestDto, Customer>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
