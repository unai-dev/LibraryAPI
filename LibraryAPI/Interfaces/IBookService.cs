using LibraryAPI.DTOs.Book;
using LibraryAPI.DTOs.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Interfaces
{
    public interface IBookService
    {
        Task<BookDTO> CreateBookAsync([FromBody] CreateBookDTO bookDTO);
        Task<bool> DeleteBookAsync([FromRoute] int id);
        Task<BookDTO> GetBooByIdAsync([FromRoute] int id);
        Task<IEnumerable<BookDTO>> GetBooksAsync([FromQuery] BookFilterDTO filter);
    }
}