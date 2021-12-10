using AutoMapper;
using BasketAPI.Entities;
using EventBusRabbitMQ;

namespace BasketAPI.Mapping
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<BasketCheckOut, BasketCheckOutEvent>().ReverseMap();
        }
    }
}
