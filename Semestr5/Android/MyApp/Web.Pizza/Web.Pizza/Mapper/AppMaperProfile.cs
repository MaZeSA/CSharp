using AutoMapper;
using Data.Pizza.Entities;
using Web.Pizza.Models;

namespace Web.Pizza.Mapper
{
    public class AppMaperProfile : Profile
    {
        public AppMaperProfile()
        {
            CreateMap<CategoryEntity, CategoryItemViewModel>()
                .ForMember(x=> x.Image, opt=> opt.MapFrom(x=> $@"/images/{x.Image}"));
        }
    }
}
