using AutoMapper;
using Database;
using WebApi.DTOs.Customers;

namespace WebApi.DTOs.AutoMapping
{
    /// <summary>
    /// AutoMapper class which is used to map Dtos to their model equivalent. 
    /// </summary>
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}