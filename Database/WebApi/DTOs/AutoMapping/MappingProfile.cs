using AutoMapper;
using Database;
using WebApi.DTOs.BarEvent;
using WebApi.DTOs.BarRepresentative;
using WebApi.DTOs.Customers;
using WebApi.DTOs.Drinks;


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

            CreateMap<Database.BarRepresentative, BarRepresentativeDto>();
            CreateMap<BarRepresentativeDto, Database.BarRepresentative>();

            CreateMap<Drink, DrinkDto>();
            CreateMap<DrinkDto, Drink>();

            CreateMap<Database.BarEvent, BarEventDto>();
            CreateMap<BarEventDto, Database.BarEvent>();

            CreateMap<Review, ReviewDto.ReviewDto>();
            CreateMap<ReviewDto.ReviewDto, Review>();
        }
    }
}