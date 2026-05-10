using AutoMapper;
using LibraryAPI.DTOs.Book;
using LibraryAPI.Entities;

namespace LibraryAPI.Utils.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<CreateBookDTO, BookDTO>();
        }
    }
}
