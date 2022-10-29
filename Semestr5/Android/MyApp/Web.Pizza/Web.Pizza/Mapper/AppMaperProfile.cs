using AutoMapper;
using Data.Pizza.Entities;
using Data.Pizza.Entities.Identity;
using Web.Pizza.Models;

namespace Web.Pizza.Mapper
{
    public class AppMaperProfile : Profile
    {
        public AppMaperProfile()
        {
            CreateMap<CategoryEntity, CategoryItemViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => $@"/images/{x.Image}"));

            CreateMap<CategoryCreateItemVM, CategoryEntity>()
                .ForMember(x => x.Image, opt => opt.Ignore());

            CreateMap<RegisterViewModel, AppUser>()
              .ForMember(x => x.Photo, opt => opt.Ignore())
              .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
