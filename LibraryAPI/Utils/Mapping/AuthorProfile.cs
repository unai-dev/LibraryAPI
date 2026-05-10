using AutoMapper;
using LibraryAPI.DTOs.Author;
using LibraryAPI.Entities;

namespace LibraryAPI.Utils.Mapping
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDTO>();
            CreateMap<CreateAuthorDTO, Author>();
        }
    }
}
