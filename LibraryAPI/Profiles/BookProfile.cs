using AutoMapper;
using Library.Data.Models;
using LibraryAPI.Dtos.Book;

namespace LibraryAPI.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, CreateBookDto>()
                .ReverseMap();
            CreateMap<Book, BookDto>()
                .ReverseMap();
        }
    }
}
