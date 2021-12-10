using AutoMapper;
using EventBusRabbitMQ;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.mapping
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckOutEvent, CheckoutOrderCommand>();
        }
    }
}
