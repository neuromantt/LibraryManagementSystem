using AutoMapper;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookMainInfoDto>().ReverseMap();
            CreateMap<Book, BookInfoDto>().ReverseMap();
        }
    }
}
