using AutoMapper;
using Core.Models.Entities;
using Core.ViewModels;

namespace Core.Models
{
    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id));

            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(o => o.Id, ex => ex.MapFrom(o => o.Id));

        }
    }
}
