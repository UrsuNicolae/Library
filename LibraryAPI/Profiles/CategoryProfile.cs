using AutoMapper;
using Library.Data.Models;
using LibraryAPI.Dtos.Category;

namespace LibraryAPI.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap();
            CreateMap<CreateCategoryDto, Category>()
                .ForMember(a => a.Name, opt => opt.MapFrom(a => a.Name))
                .ReverseMap();
        }
    }
}
