using AutoMapper;
using Library.Data.Models;
using LibraryAPI.Dtos.Author;

namespace LibraryAPI.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ReverseMap();
            CreateMap<CreateAuthorDto, Author>()
                .ForMember(a => a.Name, opt => opt.MapFrom(a => a.Name))
                .ReverseMap();
        }
    }
}
