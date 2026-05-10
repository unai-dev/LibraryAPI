using LibraryAPI.DTOs.Author;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDTO> CreateAuthorAsync([FromBody] CreateAuthorDTO authorDTO);
        Task<bool> DeleteAuthorAsync([FromRoute] int id);
        Task<AuthorDTO> GetAuthorByIdAsync([FromRoute] int id);
        Task<IEnumerable<AuthorDTO>> GetAuthorsAsync();
    }
}