using AutoMapper;
using LibraryAPI.Config;
using LibraryAPI.DTOs.Author;
using LibraryAPI.DTOs.Filter;
using LibraryAPI.Entities;
using LibraryAPI.Exceptions;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public AuthorService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /**
         * Metodo GetAuthors, devuelve todos los autores encontrados en la base de datos
         * A futuro poder implementar un filtro de busqueda
         * return --> authordto
         */
        public async Task<IEnumerable<AuthorDTO>> GetAuthorsAsync([FromQuery] AuthorFilterDTO filter)
        {
            var query = dbContext.Authors.Include(b => b.Books).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(a => a.Name.Contains(filter.Name));
            }

            if (filter.MinBooks.HasValue)
            {
                query = query.Where(a => a.Books.Count >= filter.MinBooks.Value);
            }

            query = filter.OrderBy?.ToLower() switch
            {
                "books" => query.OrderByDescending(a => a.Books.Count),
                "name" or _ => query.OrderBy(a => a.Name)
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
            var authors = await query.ToListAsync();
            return mapper.Map<IEnumerable<AuthorDTO>>(authors);

        }

        /**
         * Metodo GetAuthors, devuelve el autor indicado encontrado en la base de datos
         * Este ID se pasara como parametro en la ruta
         * return --> authordto
         */
        public async Task<AuthorDTO> GetAuthorByIdAsync([FromRoute] int id)
        {
            var author = await dbContext.Authors
                .Include(b => b.Books)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (author is null)
            {
                throw new NotFoundException("Autor no econtrado");
            }

            return mapper.Map<AuthorDTO>(author);
        }

        /**
         * Metodo CreateAuthor, crea un autor en la base de datos y lo devuelve mapeado
         * return --> authordto
         */
        public async Task<AuthorDTO> CreateAuthorAsync([FromBody] CreateAuthorDTO authorDTO)
        {
            var author = mapper.Map<Author>(authorDTO);

            dbContext.Add(author);
            await dbContext.SaveChangesAsync();

            return mapper.Map<AuthorDTO>(author);
        }

        /**
         * Metodo DeleteAuthor, elimina un autor de la base de datos
         * return --> bool indicando si es que hay registros eliminados
         */
        public async Task<bool> DeleteAuthorAsync([FromRoute] int id)
        {
            var result = await dbContext.Authors.Where(x => x.Id == id).ExecuteDeleteAsync();

            return result > 0;

        }


    }
}
