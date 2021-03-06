﻿using AutoMapper;
using Database;
using Database.Entities;
using WebApi.DTOs.BarEvent;
using WebApi.DTOs.BarRepresentative;
using WebApi.DTOs.Bars;
using WebApi.DTOs.Coupon;
using WebApi.DTOs.Customers;
using WebApi.DTOs.Drinks;


namespace WebApi.DTOs.AutoMapping
{
    /// <summary>
    /// AutoMapper class which is used to map Dtos to their model equivalent. 
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Setup of mappings, some of these are not used, e.g. BarDto to Bar. 
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Bar, BarDto>();
            CreateMap<BarDto, Bar>();

            CreateMap<Bar, BarSimpleDto>();
            CreateMap<BarSimpleDto, Bar>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<Database.Entities.BarRepresentative, BarRepresentativeDto>();
            CreateMap<BarRepresentativeDto, Database.Entities.BarRepresentative>();

            CreateMap<Drink, DrinkDto>();
            CreateMap<DrinkDto, Drink>();

            CreateMap<Database.Entities.BarEvent, BarEventDto>();
            CreateMap<BarEventDto, Database.Entities.BarEvent>();

            CreateMap<Review, ReviewDto.ReviewDto>();
            CreateMap<ReviewDto.ReviewDto, Review>();

            CreateMap<Database.Entities.Coupon, CouponDto>();
            CreateMap<CouponDto, Database.Entities.Coupon>();
        }
    }
}