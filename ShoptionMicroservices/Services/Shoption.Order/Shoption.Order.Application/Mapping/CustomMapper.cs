using System;
using AutoMapper;
using Shoption.Order.Application.Dtos;
using Shoption.Services.Order.Domain;

namespace Shoption.Order.Application.Mapping
{
    public class CustomMapper:Profile
    {
        public CustomMapper()
        {
            CreateMap<Services.Order.Domain.Order, OrderDto>().ReverseMap();
            CreateMap<OrderItemDto, OrderItemDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
