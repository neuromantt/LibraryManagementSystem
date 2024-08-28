using AutoMapper;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.DTOs.UsersBook;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookMainInfoDto>().ReverseMap();
            CreateMap<Book, BookInfoDto>().ReverseMap();

            CreateMap<UsersBook, UsersBookInfoDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.BookId, src => src.MapFrom(x => x.Book.Id))
                .ForMember(dest => dest.BookTitle, src => src.MapFrom(x => x.Book.Title))
                .ForMember(dest => dest.BookAuthor, src => src.MapFrom(x => x.Book.Author))
                .ForMember(dest => dest.BookImage, src => src.MapFrom(x => x.Book.Image));
        }
    }
}
