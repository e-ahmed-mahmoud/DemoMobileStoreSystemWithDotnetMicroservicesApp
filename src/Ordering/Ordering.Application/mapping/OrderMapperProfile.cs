using AutoMapper;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;

namespace Ordering.Application.mapping
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<Order, OrderResponse>().ReverseMap();

            CreateMap<CheckoutOrderCommand, Order>().ReverseMap();
        }
    }
}