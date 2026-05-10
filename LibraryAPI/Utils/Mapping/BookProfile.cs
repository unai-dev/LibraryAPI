using AutoMapper;
using LibraryAPI.DTOs.Book;
using LibraryAPI.Entities;

namespace LibraryAPI.Utils.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(map => map.AuthorName, cfg => cfg.MapFrom(x => x.Author!.Name));
            CreateMap<CreateBookDTO, Book>();
        }
    }
}
