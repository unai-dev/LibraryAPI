using AutoMapper;
using LibraryAPI.Config;
using LibraryAPI.DTOs.Book;
using LibraryAPI.DTOs.Filter;
using LibraryAPI.Entities;
using LibraryAPI.Exceptions;
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

        public async Task<IEnumerable<BookDTO>> GetBooksAsync([FromQuery] BookFilterDTO filter)
        {
            var query = dbContext.Books.Include(a => a.Author).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(b => b.Title.Contains(filter.Title.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.AuthorName))
            {
                query = query.Where(b => b.Author!.Name.Contains(filter.AuthorName.ToLower()));
            }

            query = filter.OrderBy?.ToLower() switch
            {
                "authors" => query.OrderBy(b => b.Author!.Name),
                "title" or _ => query.OrderBy(b => b.Title)
            };

            if (filter.Page.HasValue && filter.PageSize.HasValue)
            {
                int skip = (filter.Page.Value - 1) * filter.PageSize.Value;
                query = query.Skip(skip).Take(filter.PageSize.Value);
            }

            if (filter.Limit.HasValue)
            {
                query = query.Take(filter.Limit.Value);
            }

            var books = await query.ToListAsync();
            return mapper.Map<IEnumerable<BookDTO>>(books);

        }

        public async Task<BookDTO> GetBooByIdAsync([FromRoute] int id)
        {
            var book = await dbContext.Books
                .Include(a => a.Author)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (book is null)
            {
                throw new NotFoundException("Libro no encontrado");
            }

            return mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO> CreateBookAsync([FromBody] CreateBookDTO bookDTO)
        {
            var book = mapper.Map<Book>(bookDTO);

            var authorExists = await dbContext.Authors.AnyAsync(x => x.Id == book.AuthorId);

            if (!authorExists)
            {
                throw new BadRequestException("El autor no existe");
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
