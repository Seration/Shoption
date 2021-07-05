using System;
using AutoMapper;
using Shoption.Order.Application.Dtos;
using Shoption.Order.Domain.OrderAggregate;

namespace Shoption.Order.Application.Mapping
{
    public class CustomMapper:Profile
    {
        public CustomMapper()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<OrderItemDto, OrderItemDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
