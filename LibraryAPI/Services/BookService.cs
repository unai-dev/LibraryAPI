using AutoMapper;
using LibraryAPI.Config;
using LibraryAPI.DTOs.Book;
using LibraryAPI.Entities;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public BookService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookDTO>> GetBooksAsync()
        {
            var books = await dbContext.Books.Include(a => a.Author).ToListAsync();

            return mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookDTO> GetBooByIdAsync([FromRoute] int id)
        {
            var book = await dbContext.Books
                .Include(a => a.Author)
                .FirstOrDefaultAsync(x => x.Id == id);

            return mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO> CreateBookAsync([FromBody] CreateBookDTO bookDTO)
        {
            var book = mapper.Map<Book>(bookDTO);

            var authorExists = await dbContext.Authors.AnyAsync(x => x.Id == book.AuthorId);

            if (!authorExists)
            {
                return null!;
            }

            dbContext.Add(book);
            await dbContext.SaveChangesAsync();

            return mapper.Map<BookDTO>(book);
        }

        public async Task<bool> DeleteBookAsync([FromRoute] int id)
        {

            var result = await dbContext.Books.Where(x => x.Id == id).ExecuteDeleteAsync();

            return result > 0;

        }


    }
}
