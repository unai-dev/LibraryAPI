using AutoMapper;
using LibraryAPI.Config;
using LibraryAPI.DTOs.Author;
using LibraryAPI.Entities;
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
        public async Task<IEnumerable<AuthorDTO>> GetAuthorsAsync()
        {
            var authors = await dbContext.Authors.ToListAsync();

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

        public async Task<bool> DeleteAuthorAsync([FromRoute] int id)
        {
            var result = await dbContext.Authors.Where(x => x.Id == id).ExecuteDeleteAsync();

            return result > 0;

        }


    }
}
