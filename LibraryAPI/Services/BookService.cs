using AutoMapper;
using LibraryAPI.Config;
using LibraryAPI.DTOs.Book;
using LibraryAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class BookService
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
            var books = await dbContext.Books.ToListAsync();

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

            var author = dbContext.Authors.FirstOrDefaultAsync(x => x.Id == book.AuthorId);

            book.Author.Id = author.Id;
        }


    }
}
