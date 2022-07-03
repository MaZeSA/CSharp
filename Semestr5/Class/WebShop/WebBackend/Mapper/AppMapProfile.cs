using AutoMapper;
using WebBackend.Data.Entities;
using WebBackend.Model;

namespace WebBackend.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<CategoryCreateVM, CategoryEntity>()
                .ForMember(x => x.Image, opt => opt.Ignore()); 
            CreateMap<CategoryEntity, CategoryItemVM>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x=> $"/images/{x.Image}"));
        }
    }
}
